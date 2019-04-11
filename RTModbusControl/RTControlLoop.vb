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
    Private _primaryEC As Integer = 0               ' index of primary EnergyCell
    Private _deltaW As Double = 0                   ' controller output, omega
    Private _deltaWold As Double = 0                ' controller output, omega, previous
    Private _deltaE As Double = 0                   ' controller output, voltage
    Private _deltaEold As Double = 0                ' controller output, voltage, previous
    Private _eW As Double = 0                       ' error signal, omega
    Private _eWold As Double = 0                    ' error signal, omega, previous
    Private _eE As Double = 0                       ' error signal, voltage
    Private _eEold As Double = 0                    ' error signal, voltage, previous

    ' UpstreamController and general secondary control variables
    Private _omegaStar As Double = W0               ' synchronous speed reference
    Private _averageStoredEnergy As Double = 0.0    ' average stored energy of all EnergyCells

    ' Administrative variables
    Private _stateObject As StateObjClass           ' timer state information
    Private Shared _isRunning As Boolean            ' local variable for public function

    ' Public events
    Public Event Update(sender As Object, e As EventArgs)

    ' Keyhole function for private field.
    Public Shared Function GetIsRunning() As Boolean
        Return _isRunning
    End Function

    ' Instantiates and runs timer with period Ts.
    Public Sub RunLoop()
        If IsNothing(_stateObject) Then
            _stateObject = New StateObjClass With {
                .TimerCanceled = False,
                .UpdateCount = 0}
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
        Dim state = DirectCast(stateObj, StateObjClass)
        Dim energySum = 0.0

        ' Communicate in parallel with all energy cells
        Parallel.ForEach(EnergyCells.All, Async Sub(c)
            c.EnergyStorageAverage = _averageStoredEnergy
            Dim opComplete As Boolean = True

            If Connected.Contains(c) Then
                Try
                    If c.Status = "Remote" Then
                        opComplete = Await c.ReadMeasurementsAsync()

                        c.DeltaW = _deltaW
                        c.DeltaE = _deltaE

                        opComplete = Await c.WriteCommandsAsync()
                    Else ' Local
                        opComplete = Await c.ReadMeasurementsAsync()
                        opComplete = Await c.ReadCommandsAsync()
                        opComplete = Await c.SendLocalControlCommandAsync()

                        _deltaW = c.DeltaW
                        _deltaE = c.DeltaE
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

        SetPrimaryEC()

        Try
            _eW = _omegaStar - EnergyCells.All(_primaryEC).Speed
            _eE = EnergyCells.All(_primaryEC).VoltageSetpoint - EnergyCells.All(_primaryEC).VoltageMagnitudeA
        Catch ex As Exception
            If Equals(Connected.Count, 0) Or Equals(_primaryEC, -1)
                Console.WriteLine($"{Now.Hour}:{Now.Minute}:{Now.Second}.{Now.Millisecond} - OnPulse:  No connected devices. Control frozen.")
            Else
                Console.WriteLine($"{Now.Hour}:{Now.Minute}:{Now.Second}.{Now.Millisecond} - Ex - OnPulse:  {ex.Message}")
            End If

            _eW = _eWold
            _eE = _eEold
        End Try

        _deltaW = Saturate(_deltaWold + (KPw + KIw * Ts / 2) * _eW - (KPw - KIw * Ts / 2) * _eWold, OmegaLimit)
        _deltaE = Saturate(_deltaEold + (KPe + KIe * Ts / 2) * _eE - (KPe - KIe * Ts / 2) * _eEold, VoltageLimit)

        _eWold = _eW
        _eEold = _eE
        _deltaWold = _deltaW
        _deltaEold = _deltaE

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
    
    ' Use the first connected EnergyCell as the reference for controller and compute control signals
    Private Sub SetPrimaryEC()
        If Not Connected.Count.Equals(0) Then
            If EnergyCells.All(0).CellConnection.Client.Connected Then
                If Not _primaryEC.Equals(0) Then
                    _primaryEC = 0
                End If
            Else
                _primaryEC = EnergyCells.All.FindIndex(1, Function(p) p.CellConnection.Client.Connected)
            End If
        Else 
            _primaryEC = -1
        End If
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
End Class
