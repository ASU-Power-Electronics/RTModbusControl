Option Explicit On
Option Strict On

Imports System.Threading
' Implements the necessary control logic and timing for a pair of PI controllers, specific to the Opal RT
Public Class RTControlLoop
    ' EnergyCell control variables
    Private Const KPw As Double = 0.034             ' proportional constant, omega
    Private Const KIw As Double = 2                 ' integral constant, omega
    Private Const OmegaLimit As Integer = 5         ' saturation limit for omega
    Private Const KPe As Double = 0.0047/6.68       ' proportional constant, voltage
    Private Const KIe As Double = 3.6145/6.68       ' integral constant, voltage
    Private Const VoltageLimit As Integer = 250     ' saturation limit for voltage
    Private Const Ts As Double = 0.2                ' sampling time in [s]
    Private Const W0 As Double = 2*Math.PI*60       ' constant ideal grid frequency in [rad/s]

    ' UpstreamController and general secondary control variables
    Private _omegaStar As Double = W0               ' synchronous speed reference
    Private _averageStoredEnergy As Double = 0.0    ' average stored energy of all EnergyCells

    ' Administrative variables
    Private _stateObject As StateObjClass           ' timer state information
    Private Shared _isRunning As Boolean            ' local variable for public function
    Private _controlVarList As List(Of LoopVars)    ' list of control variable objects

    ' Public events
    Public Event Update(sender As Object, e As EventArgs)
    

    ' Keyhole function for private field.
    Public Shared Function GetIsRunning() As Boolean
        Return _isRunning
    End Function

    ' Instantiates and runs timer with period Ts, and creates control variables objects.
    Public Sub RunLoop()
        If IsNothing(_stateObject) Then
            _stateObject = New StateObjClass With {
                .TimerCanceled = False,
                .UpdateCount = 0}
        End If

        If IsNothing(_controlVarList) Then
            _controlVarList = New List(Of LoopVars) With {.Capacity = EnergyCells.All.Count}

            For idx = 1 To _controlVarList.Capacity
                _controlVarList.Add(New LoopVars() With {
                                        .DeltaE = 0.0,
                                        .DeltaW = 0.0,
                                        .Ee = 0.0,
                                        .Ew = 0.0,
                                        .DeltaEold = 0.0,
                                        .DeltaWold = 0.0,
                                        .EEold = 0.0,
                                        .EWold = 0.0})
            Next
        End If

        Dim timerDelegate As New TimerCallback(AddressOf OnPulse)
        Dim loopTimer As New Timer(timerDelegate, _stateObject, 0, CInt(Ts*1000))

        _stateObject.TimerReference = loopTimer

        _isRunning = True
    End Sub

    ' Cancels timer and waits for cells to finish operation for graceful exit.
    Public Sub StopLoop()
        Dim cellIsWorking As Boolean

        If Not IsNothing(_stateObject) Then
            _stateObject.TimerCanceled = True

            Do
                cellIsWorking = Not EnergyCells.All.TrueForAll(Function(p) Not p.Working)
            Loop While cellIsWorking

            ' Set all controls to local for disconnect
            Parallel.ForEach(EnergyCells.All, Async Sub(c)
                Await c.SendLocalControlCommandAsync()
            End Sub)
        End If

        _isRunning = False
    End Sub

    ' Implements control logic to fire on every clock pulse.
    ' Updates all controller DeltaOmega values, and uses first controller's value as reference.
    ' Calculates average energy stored across all EnergyCells.
    Private Sub OnPulse(stateObj As Object)
        Dim state = DirectCast(stateObj, StateObjClass) ' loop state variable
        Dim energySum = 0.0                             ' average stored energy

        ' Communicate in parallel with all energy cells
        Parallel.ForEach(EnergyCells.All, Async Sub(c)
            c.EnergyStorageAverage = _averageStoredEnergy
            Dim opComplete As Boolean = True
            Dim cellIndex = EnergyCells.All.FindIndex(Function(p) Equals(p, c))

            If Connected.Contains(c) Then
                Try
                    If c.Status = "Remote" Then
                        opComplete = Await c.ReadMeasurementsAsync()

                        c.DeltaW = _controlVarList(cellIndex).DeltaW
                        c.DeltaE = _controlVarList(cellIndex).DeltaE

                        opComplete = Await c.WriteCommandsAsync()
                    Else ' Local
                        opComplete = Await c.ReadMeasurementsAsync()
                        opComplete = Await c.ReadCommandsAsync()
                        opComplete = Await c.SendLocalControlCommandAsync()

                        _controlVarList(cellIndex).DeltaW = c.DeltaW
                        _controlVarList(cellIndex).DeltaE = c.DeltaE
                    End If
                Catch ex As Exception
                    Console.WriteLine($"{Now.Hour}:{Now.Minute}:{Now.Second}.{Now.Millisecond} - Ex - Parallel Read/Write:  {ex.Message}")
                End Try
            End If

            ' Accumulate energy storage value contribution from EnergyCell c
            energySum = energySum + c.EnergyStorage

            ' If for any reason an operation failed, remake connection (via events in Connection class)
            If Not opComplete Then
                c.CellConnection.Client.Disconnect()
            Else
                Try
                    _controlVarList(cellIndex).EW = _omegaStar - c.Speed
                    _controlVarList(cellIndex).Ee = c.VoltageSetpoint - c.VoltageMagnitudeA
                Catch ex As Exception
                    If Equals(Connected.Count, 0)
                        Console.WriteLine($"{Now.Hour}:{Now.Minute}:{Now.Second}.{Now.Millisecond} - OnPulse:  No connected devices. Control frozen.")
                    Else
                        Console.WriteLine($"{Now.Hour}:{Now.Minute}:{Now.Second}.{Now.Millisecond} - Ex - OnPulse:  {ex.Message}")
                    End If

                    _controlVarList(cellIndex).Ew = _controlVarList(cellIndex).EWold
                    _controlVarList(cellIndex).Ee = _controlVarList(cellIndex).EEold
                End Try

                _controlVarList(cellIndex).DeltaW = Saturate(_controlVarList(cellIndex).DeltaWold + (KPw + KIw * Ts / 2) * _controlVarList(cellIndex).Ew - (KPw - KIw * Ts / 2) * _controlVarList(cellIndex).EWold, OmegaLimit)
                _controlVarList(cellIndex).DeltaE = Saturate(_controlVarList(cellIndex).DeltaEold + (KPe + KIe * Ts / 2) * _controlVarList(cellIndex).Ee - (KPe - KIe * Ts / 2) * _controlVarList(cellIndex).EEold, VoltageLimit)

                _controlVarList(cellIndex).EWold = _controlVarList(cellIndex).Ew
                _controlVarList(cellIndex).EEold = _controlVarList(cellIndex).Ee
                _controlVarList(cellIndex).DeltaWold = _controlVarList(cellIndex).DeltaW
                _controlVarList(cellIndex).DeltaEold = _controlVarList(cellIndex).DeltaE
            End If

            End Sub)

        ' Communicate with any tertiary controllers and set frequency deviation
        ' TODO: This should also find the appropriate connected controller and use its value for the appropriate group
        '       (assuming multiple systems)
        If UpstreamControllers.All.Count > 0 Then
            For Each controller In UpstreamControllers.All
                controller.ReadDeltaOmega()
            Next

            _omegaStar = W0 + UpstreamControllers.All(0).DeltaOmega
        Else
            _omegaStar = W0
        End If

        ' Calculate new average energy storage value
        _averageStoredEnergy = energySum / EnergyCells.All.Count

        ' Handles updates to the UI
        Try
            If Not state.TimerCanceled Then
                If state.UpdateCount >= 2 Then
                    RaiseEvent Update(Nothing, Nothing)
                    Interlocked.Exchange(state.UpdateCount, 0)
                Else
                    Interlocked.Increment(state.UpdateCount)
                End If
            Else
                state.TimerReference.Dispose()
            End If
        Catch ex As Exception
            Console.WriteLine($"{Now.Hour}:{Now.Minute}:{Now.Second}.{Now.Millisecond} - Ex - Display:  {ex.Message}")
        End Try
    End Sub

    ' Enforces a maximum absolute value limit
    Private Shared Function Saturate(value As Double, limit As Double) As Double
        If Math.Abs(value) > limit
            If value > 0
                Return limit
            Else
                Return -limit
            End If
        End If

        Return value
    End Function

    ' Stores information about the state of the active thread
    Private Class StateObjClass
        Public UpdateCount As Integer
        Public TimerReference As Timer
        Public TimerCanceled As Boolean
    End Class

    ' Stores the state of the EnergyCell control variables
    Private Class LoopVars
        Public DeltaW As Double     ' controller output, omega
        Public DeltaE As Double     ' controller output, voltage
        Public Ew As Double         ' error signal, omega
        Public Ee As Double         ' error signal, voltage
        Public DeltaWold As Double  ' controller output, omega, previous
        Public DeltaEold As Double  ' controller output, voltage, previous
        Public EWold As Double      ' error signal, omega, previous
        Public EEold As Double      ' error signal, voltage, previous
    End Class
End Class
