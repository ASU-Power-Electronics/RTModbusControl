Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Timers

' TODO: Fix clock after leaving FrmControls
' TODO: https://www.dreamincode.net/forums/topic/395260-idustrial-process-regulation-using-a-vbnet-pid-controller/
' TODO: https://stackoverflow.com/questions/3641147/object-disposed-exception-and-multi-thread-application
' TODO: https://stackoverflow.com/questions/1731384/how-to-stop-backgroundworker-on-forms-closing-event/1732361#1732361
' TODO: https://stackoverflow.com/questions/19571739/system-timers-timer-net-really-slow
'       From the above, it's probably best to switch to either a Threading.Timer or a Windows.Forms.Timer
Module ControlLoop
    Private Const KPw As Double = 0.034         ' proportional constant, omega
    Private Const KIw As Double = 2             ' integral constant, omega
    Private Const KPe As Double = 0.0047 / 6.68 ' proportional constant, voltage
    Private Const KIe As Double = 3.6145 / 6.68 ' integral constant, voltage
    Private Const Ts As Double = 0.1            ' sampling time in [s]
    Private ReadOnly WorkToken As New Object    ' Used to restrict access to Modbus communication
    Private _clockPulse As Timer                ' clock pulse simulation
    Private _deltaW As Double = 0               ' controller output, omega
    Private _deltaWold As Double = 0            ' controller output, omega, previous
    Private _deltaE As Double = 0               ' controller output, voltage
    Private _deltaEold As Double = 0            ' controller output, voltage, previous
    Private _eW As Double = 0                   ' error signal, omega
    Private _eWold As Double = 0                ' error signal, omega, previous
    Private _eE As Double = 0                   ' error signal, voltage
    Private _eEold As Double = 0                ' error signal, voltage, previous
    Private _omegaStar As Double = 2 * Math.PI * 60
    Private _averageStoredEnergy As Double = 0.0

    Private WithEvents _loopWorker As BackgroundWorker

    ' Instantiates and runs timer with period Ts
    Public Sub RunLoop(ByRef bgw As BackgroundWorker)
        If IsNothing(_clockPulse) Then
            _clockPulse = New Timer(Ts * 1000.0) With {
                .Enabled = True,
                .AutoReset = True}
            AddHandler _clockPulse.Elapsed, AddressOf OnPulse
        End If
        If IsNothing(_loopWorker)
            _loopWorker = bgw
        End If
    End Sub

    ' Stops and disposes of timer and cancels the cellWorkers' async operation for graceful exit.
    Public Sub StopLoop()
        Dim cellIsWorking As Boolean

        If Not IsNothing(_clockPulse) Then
            _clockPulse.Stop()
            _clockPulse.Enabled = False

            Do
                cellIsWorking = Not EnergyCells.All.TrueForAll(Function(p) Not p.Working)
            Loop While cellIsWorking

            'Parallel.ForEach(EnergyCells.All, Sub(ec)
            '    ec.CellWorker.CancelAsync()
            'End Sub)
            _loopWorker.CancelAsync()
        End If
    End Sub

    ' Implements control logic to fire on every clock pulse (clockPulse.Elapsed)
    ' Updates all controller DeltaOmega values, and uses first controller's value as reference
    ' Calculates average energy stored across all EnergyCells and sets value from previous pulse
    Private Sub OnPulse(source As Object, e As ElapsedEventArgs)
        Dim energySum = 0.0

        ' Calculate new average energy storage value
        For Each c In EnergyCells.All
            energySum = energySum + c.EnergyStorage
        Next
        _averageStoredEnergy = energySum / EnergyCells.All.Count

        If UpstreamControllers.All.Count > 0 Then
            For Each c In UpstreamControllers.All
                c.ReadDeltaOmega()
            Next
            _omegaStar = 2 * Math.PI * 60 + UpstreamControllers.All(0).DeltaOmega
        Else
            _omegaStar = 2 * Math.PI * 60
        End If

        ' Run individual EnergyCell calculations in parallel
        Parallel.ForEach(EnergyCells.All, Sub(c)
            c.EnergyStorageAverage = _averageStoredEnergy

            If c.Status = "Remote" Then
                Select Case c.Mode
                    Case "Manual"
                        c.DeltaW = _deltaW
                        c.DeltaE = _deltaE

                        ' Allow only one connection at a time
                        SyncLock WorkToken
                            SafeCall(c, "ReadMeasurements")
                        End SyncLock

                        SyncLock WorkToken
                            SafeCall(c, "WriteCommands")
                        End SyncLock
            
                            If Equals(EnergyCells.All.FindIndex(Function(p) Equals(p.Name, c.Name)), 0) Then
                                Try
                                    'If Not (c.CellWorker.CancellationPending Or IsNothing(c.CellWorker)) Then
                                    '    c.CellWorker.ReportProgress(0) ' 0% complete forever
                                    'End If
                                    If Not (_loopWorker.CancellationPending Or IsNothing(_loopWorker)) Then
                                        _loopWorker.ReportProgress(0)
                                    End If
                                Catch ex As InvalidOperationException
                                    Console.WriteLine($"Display:  {ex.Message}")
                                End Try
                            End If
                    Case "Automated"
                        ' Use the first EnergyCell as the reference for controller
                        ' TODO: Check if it's connected, then switch to another if it's not; back when it reconnects
                        If EnergyCells.All.FindIndex(Function(p) p.Name.Equals(c.Name)).Equals(0) Then
                            _eW = _omegaStar - c.Speed
                            _eE = c.VoltageSetpoint - c.VoltageMagnitudeA
                            _deltaW = _deltaWold + (KPw + KIw * Ts / 2) * _eW - (KPw - KIw * Ts / 2) * _eWold

                            ' Saturation at Int16 max
                            If _deltaW > Int16.MaxValue / 10
                                _deltaW = (Int16.MaxValue - 1) / 10
                            End If

                            _deltaE = _deltaEold + (KPe + KIe * Ts / 2) * _eE - (KPe - KIe * Ts / 2) * _eEold

                            ' Saturation at Int16 max
                            If _deltaE > Int16.MaxValue / 10
                                _deltaE = (Int16.MaxValue - 1) / 10
                            End If

                            _eWold = _eW
                            _eEold = _eE
                            _deltaWold = _deltaW
                            _deltaEold = _deltaE
                        End If

                        c.DeltaW = _deltaW
                        c.DeltaE = _deltaE
                        c.RealPowerSetpoint = c.PvGeneration
                        c.ReactivePowerSetpoint = 0

                        ' Allow only one connection at a time
                        SyncLock WorkToken
                            SafeCall(c, "ReadMeasurements")
                        End SyncLock

                        SyncLock WorkToken
                            SafeCall(c, "WriteCommands")
                        End SyncLock
            
                        If Equals(EnergyCells.All.FindIndex(Function(p) Equals(p.Name, c.Name)), 0) Then
                            Try
                                'If Not (c.CellWorker.CancellationPending Or IsNothing(c.CellWorker)) Then
                                '    c.CellWorker.ReportProgress(0) ' 0% complete forever
                                'End If
                                If Not (_loopWorker.CancellationPending Or IsNothing(_loopWorker)) Then
                                    _loopWorker.ReportProgress(0)
                                End If
                            Catch ex As InvalidOperationException
                                Console.WriteLine($"Display:  {ex.Message}")
                            End Try
                        End If
                End Select
            Else ' Local
                ' Allow only one connection at a time
                SyncLock WorkToken
                    SafeCall(c, "ReadMeasurements")
                End SyncLock

                SyncLock WorkToken
                    SafeCall(c, "ReadCommands")
                End SyncLock
                
                ' Send the Local/Remote switch
                SyncLock WorkToken
                    c.CellConnection.Client.WriteSingleRegister(0, 0)
                End SyncLock
            
                If Equals(EnergyCells.All.FindIndex(Function(p) Equals(p.Name, c.Name)), 0) Then
                    Try
                        'If Not (c.CellWorker.CancellationPending Or IsNothing(c.CellWorker)) Then
                        '    c.CellWorker.ReportProgress(0) ' 0% complete forever
                        'End If
                        If Not (_loopWorker.CancellationPending Or IsNothing(_loopWorker)) Then
                            _loopWorker.ReportProgress(0)
                        End If
                    Catch ex As InvalidOperationException
                        Console.WriteLine($"Display:  {ex.Message}")
                    End Try
                End If
            End If
        End Sub)
    End Sub

    ' An attempt to not overwork the Modbus communication
    Private Async Sub SafeCall(c As EnergyCell, operation As String)
        Dim cellIsWorking As Boolean

        Select Case operation
            Case "ReadMeasurements"
                Await c.ReadMeasurementsAsync()
            Case "WriteCommands"
                Await c.WriteCommandsAsync()
            Case "ReadCommands"
                Await c.ReadCommandsAsync()
        End Select

        Do
            cellIsWorking = Not EnergyCells.All.TrueForAll(Function(p) Not p.Working)
        Loop While cellIsWorking
    End Sub
End Module
