Option Strict On
Option Explicit On

' Implements the Opal-RT Modbus slave mapping
' TODO: Generalize class to allow different devices
Public Class EnergyCell
    ' Private fields for connection integrity checks
    Private WithEvents _cellConnection As Connection

    ' Administrative properties
    Public Property Name As String
    Public Property Status As String = "Local"
    Public Property Mode As String = "Automated"
    Public Property Working As Boolean = False

    Public Property CellConnection As Connection
        Get
            Return _cellConnection
        End Get

        Set
            _cellConnection = Value
        End Set
    End Property

    ' Electrical and mechanical measurement properties
    Public Property RealPower As Double = 0.0
    Public Property ReactivePower As Double = 0.0
    Public Property VoltageMagnitudeA As Double = 0.0
    Public Property VoltageAngleA As Double = 0.0
    Public Property CurrentMagnitudeA As Double = 0.0
    Public Property CurrentAngleA As Double = 0.0
    Public Property Speed As Double = 0.0
    Public Property PvGeneration As Double = 0.0
    Public Property EnergyStorage As Double = 0.0
    Public Property EnergyStorageAverage As Double = 0.0

    ' Electrical and computational control properties
    Public Property RealPowerSetpoint As Double = 0.0
    Public Property ReactivePowerSetpoint As Double = 0.0
    Public Property VoltageSetpoint As Double = 4160/Math.Sqrt(3)
    Public Property DeltaW As Double = 0.0
    Public Property DeltaE As Double = 0.0

    ' Scaling constants
    Private Const VoltageScale As Double = 1
    Private Const CurrentScale As Double = 1
    Private Const AngleScale As Double = 1/100
    Private Const PowerScale As Double = 1000
    Private Const SpeedScale As Double = 1/1000
    Private Const EnergyStorageScale As Double = 10

    Private Sub OnDisconnect() Handles _cellConnection.Disconnected
        Connected.Remove(Me)
    End Sub

    Private Sub OnConnect() Handles _cellConnection.Connected
        Connected.Add(Me)
    End Sub

    ' Reads and computes all measurements from device input registers
    ' Specific to OpalRT registers:
    ' 0 - |Va|
    ' 1 - 1 + sgn(∠Va)
    ' 2 - |∠Va| * 100
    ' 3 - |Ia|
    ' 4 = 1 + sgn(∠Ia)
    ' 5 - |∠Ia| * 100
    ' 6 - 1 + sgn(P)
    ' 7 - |P| / 1000
    ' 8 - 1 + sgn(Q)
    ' 9 - |Q| / 1000
    ' 10 - 1 + sgn(ω0 - ω)
    ' 11 - |ω0 - ω| * 1000
    ' 12 - P_PV / 1000
    ' 13 - E_s / 10
    ' TODO: Generalize to array input for register mapping (for different devices)
    Public Async Function ReadMeasurementsAsync() As Task(Of Boolean)
        Dim registerAddresses As Integer() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14}
        Dim wordResult As Integer()
        Dim dataIsValid As Boolean

        Working = True

        ' Bail if not connected
        If Not CellConnection.Client.Connected Then
            Console.WriteLine($"{Now.Hour}:{Now.Minute}:{Now.Second}.{Now.Millisecond} - {Name} is disconnected.  Measurements dropped.")
            Await Task.Delay(TimeSpan.FromMilliseconds(1))
            Return False
        End If

        Try
            wordResult = CellConnection.Client.ReadInputRegisters(registerAddresses(0) - 1, registerAddresses.Length)
            Console.WriteLine(
                $"{Now.Hour}:{Now.Minute}:{Now.Second}.{Now.Millisecond} - {Name} Meas. Read:  ({ _
                                 String.Join(", ", Array.ConvertAll(wordResult, Function(x) x.ToString()))})")
        Catch ex As Exception
            Console.WriteLine($"{Now.Hour}:{Now.Minute}:{Now.Second}.{Now.Millisecond} - EnergyCell {Name} ReadMeasurements:  {ex.Message}")
            'wordResult = {CType(VoltageMagnitudeA/VoltageScale, Integer),
            '              Math.Sign(VoltageAngleA) + 1,
            '              CType(VoltageAngleA/AngleScale, Integer),
            '              CType(CurrentMagnitudeA/CurrentScale, Integer),
            '              Math.Sign(CurrentAngleA) + 1,
            '              CType(CurrentAngleA/AngleScale, Integer),
            '              Math.Sign(RealPower) + 1,
            '              CType(Math.Abs(RealPower)/PowerScale, Integer),
            '              Math.Sign(ReactivePower) + 1,
            '              CType(Math.Abs(ReactivePower)/PowerScale, Integer),
            '              Math.Sign(Speed) + 1,
            '              CType(Speed/SpeedScale, Integer),
            '              CType(PvGeneration/PowerScale, Integer),
            '              CType(EnergyStorage/EnergyStorageScale, Integer)}
            Return False
        End Try

        dataIsValid = ValidateData(wordResult, "Measurement")

        ' Assign Opal RT register values
        If dataIsValid Then
            VoltageMagnitudeA = wordResult(0)*VoltageScale
            VoltageAngleA = (wordResult(1) - 1)*wordResult(2)*AngleScale
            CurrentMagnitudeA = wordResult(3)*CurrentScale
            CurrentAngleA = (wordResult(4) - 1)*wordResult(5)*AngleScale
            RealPower = (wordResult(6) - 1)*wordResult(7)*PowerScale
            ReactivePower = (wordResult(8) - 1)*wordResult(9)*PowerScale
            Speed = 2*Math.PI*60 - (wordResult(10) - 1)*wordResult(11)*SpeedScale
            PvGeneration = wordResult(12)*PowerScale
            EnergyStorage = wordResult(13)*EnergyStorageScale
        Else
            Console.WriteLine($"{Now.Hour}:{Now.Minute}:{Now.Second}.{Now.Millisecond} - {Name} Meas. Data Invalid:  ({ _
                                 String.Join(", ", Array.ConvertAll(wordResult, Function(x) x.ToString()))})")
            Return False
        End If

        Working = False
        Return True
    End Function

    ' Reads commands from device holding registers in local control mode
    ' Specific to OpalRT registers:
    ' 8 - Islanded/Grid-Connected (0 - Grid-Connected, 1 - Islanded)
    ' 9 - P0 / 1000
    ' 10 - Q0 / 1000
    ' 11 - sgn(δω) + 1
    ' 12 - |δω|*1000
    ' 13 - sgn(δE) + 1
    ' 14 - |δE|
    ' 15 - E_s,avg / 10
    ' TODO: Generalize to array input for register mapping (for different devices)
    Public Async Function ReadCommandsAsync() As Task(Of Boolean)
        Dim registerAddresses As Integer() = {9, 10, 11, 12, 13, 14, 15, 16}
        Dim wordResult As Integer()
        Dim dataIsValid As Boolean

        ' Bail if not connected
        If Not CellConnection.Client.Connected Then
            Console.WriteLine($"{Now.Hour}:{Now.Minute}:{Now.Second}.{Now.Millisecond} - {Name} is disconnected.  Read commands dropped.")
            Await Task.Delay(TimeSpan.FromMilliseconds(1))
            Return False
        End If

        Working = True

        Try
            wordResult = CellConnection.Client.ReadHoldingRegisters(registerAddresses(0) - 1, registerAddresses.Length)
            Console.WriteLine(
                $"{Now.Hour}:{Now.Minute}:{Now.Second}.{Now.Millisecond} - {Name} Cont. Read:  ({ _
                                 String.Join(", ", Array.ConvertAll(wordResult, Function(x) x.ToString()))})")
        Catch ex As Exception
            Console.WriteLine($"{Now.Hour}:{Now.Minute}:{Now.Second}.{Now.Millisecond} - Ex - {Name} ReadControls:  {ex.Message}")
            'wordResult = {GetStatusRegister(Status),
            '              CType(RealPowerSetpoint, Integer),
            '              CType(ReactivePowerSetpoint, Integer),
            '              Math.Sign(DeltaW) + 1,
            '              CType(Math.Abs(DeltaW), Integer),
            '              Math.Sign(DeltaE) + 1,
            '              CType(Math.Abs(DeltaE), Integer),
            '              CType(EnergyStorageAverage, Integer)}
            Return False
        End Try

        dataIsValid = ValidateData(wordResult, "Controls")

        wordResult = AvoidOverflow(wordResult)

        ' Assign Opal RT register values
        If dataIsValid Then
            RealPowerSetpoint = wordResult(1)*PowerScale
            ReactivePowerSetpoint = wordResult(2)*PowerScale
            DeltaW = (wordResult(3) - 1)*wordResult(4)*SpeedScale
            DeltaE = (wordResult(5) - 1)*wordResult(6)*VoltageScale
            EnergyStorageAverage = wordResult(7)*EnergyStorageScale
        Else
            Console.WriteLine($"{Now.Hour}:{Now.Minute}:{Now.Second}.{Now.Millisecond} - {Name} Cont. Data Invalid:  ({ _
                                 String.Join(", ", Array.ConvertAll(wordResult, Function(x) x.ToString()))})")
            Return False
        End If

        Working = False
        Return True
    End Function

    ' Writes all commands to device holding registers
    ' Specific to OpalRT registers:
    ' 0 - Remote/Local control (0 - local, 1 - remote)
    ' 1 - |P0| / 1000
    ' 2 - |Q0| / 1000
    ' 3 - 1 + sgn(δω)
    ' 4 - |δω| / 1000
    ' 5 - 1 + sgn(δE)
    ' 6 - δE
    ' 7 - E_s,avg / 10
    ' TODO: Generalize to array input for register mapping (for different devices)
    Public Async Function WriteCommandsAsync() As Task(Of Boolean)
        Dim registerAddresses As Integer() = {1, 2, 3, 4, 5, 6, 7, 8}
        Dim registerValues As Integer() = {GetStatusRegister(Status),
                                           CType(Math.Round(Math.Abs(RealPowerSetpoint / PowerScale)), Integer),
                                           CType(Math.Round(Math.Abs(ReactivePowerSetpoint / PowerScale)), Integer),
                                           If(DeltaW > 0, 2, 0),
                                           CType(Math.Round(Math.Abs(DeltaW) / SpeedScale), Integer),
                                           If(DeltaE > 0, 2, 0),
                                           CType(Math.Round(Math.Abs(DeltaE) / VoltageScale), Integer),
                                           CType(Math.Round(EnergyStorageAverage / EnergyStorageScale), Integer)}

        ' Bail if not connected
        If Not CellConnection.Client.Connected Then
            Console.WriteLine($"{Now.Hour}:{Now.Minute}:{Now.Second}.{Now.Millisecond} - {Name} is disconnected.  Commands dropped.")
            Await Task.Delay(TimeSpan.FromMilliseconds(1))
            Return False
        End If

        Working = True

        registerValues = AvoidOverflow(registerValues)

        Try
            CellConnection.Client.WriteMultipleRegisters(registerAddresses(0) - 1, registerValues)
            Console.WriteLine(
                $"{Now.Hour}:{Now.Minute}:{Now.Second}.{Now.Millisecond} - {Name} Cont. Wrote:  ({ _
                                 String.Join(", ", Array.ConvertAll(registerValues, Function(x) x.ToString()))})")
        Catch ex As Exception
            Console.WriteLine($"{Now.Hour}:{Now.Minute}:{Now.Second}.{Now.Millisecond} - Ex - {Name} WriteControls:  {ex.Message}")
            Return False
        End Try

        Working = False
        Return True
    End Function

    ' Sends local control command
    Public Async Function SendLocalControlCommandAsync() As Task(Of Boolean)
        ' Bail if not connected
        If Not CellConnection.Client.Connected Then
            Console.WriteLine($"{Now.Hour}:{Now.Minute}:{Now.Second}.{Now.Millisecond} - {Name} is disconnected.  Commands dropped.")
            Await Task.Delay(TimeSpan.FromMilliseconds(1))
            Return False
        End If

        Try
            CellConnection.Client.WriteSingleRegister(0, 0)
        Catch ex As Exception
            Console.WriteLine($"{Now.Hour}:{Now.Minute}:{Now.Second}.{Now.Millisecond} - Ex - {Name} WriteControls:  {ex.Message}")
            Return False
        End Try

        Return True
    End Function

    ' Validates data for Opal RT Modbus function by checking sign values (does not prevent all zeros)
    Private Function ValidateData(data() As Integer, v As String) As Boolean
        If Equals(v, "Measurement")
            Dim twoOkay As Boolean = Equals(data(1), 0) Or Equals(data(1), 2)
            Dim fiveOkay As Boolean = Equals(data(4), 0) Or Equals(data(4), 2)
            Dim sevenOkay As Boolean = Equals(data(6), 0) Or Equals(data(6), 2)
            Dim nineOkay As Boolean = Equals(data(8), 0) Or Equals(data(8), 2)
            Dim elevenOkay As Boolean = Equals(data(10), 0) Or Equals(data(10), 2)

            If twoOkay And fiveOkay And sevenOkay And nineOkay And elevenOkay Then
                Return True
            Else
                Return False
            End If
        ElseIf Equals(v, "Controls")
            Dim fourOkay As Boolean = Equals(data(3), 0) Or Equals(data(3), 2)
            Dim sixOkay As Boolean = Equals(data(5), 0) Or Equals(data(5), 2)

            If fourOkay And sixOkay Then
                Return True
            Else
                Return False
            End If
        End If

        Return False
    End Function

    ' Checks control signals for UInt16 bounds to avoid overflow by integer division
    Private Function AvoidOverflow(registerValues() As Integer) As Integer()
        Parallel.ForEach(registerValues, Sub(v)
                                             If v > UInt16.MaxValue
                                                 Dim vIdx = Array.FindIndex(registerValues, Function(p) p = v)
                                                 registerValues(vIdx) = CType((UInt16.MaxValue - 1) * (registerValues(vIdx) / registerValues(vIdx)), Integer)
                                                 Console.WriteLine($"{Now.Hour}:{Now.Minute}:{Now.Second}.{Now.Millisecond} - {Name} Overflow:  Value {v} at index {vIdx} too large, capped at {UInt16.MaxValue - 1}")
                                             End If
                                         End Sub)
        Return registerValues
    End Function

    ' Converts Status to integer value for transmission to Modbus slave
    Private Shared Function GetStatusRegister(modeVal As String) As Integer
        If modeVal.Equals("Local") Then
            Return 0
        ElseIf modeVal.Equals("Remote") Then
            Return 1
        Else ' Default value in case of wierdness
            Return 0
        End If
    End Function

    'Private Shared Function ArraysAreEqual (Of T)(a As T(), b As T()) As Boolean
    '    If a Is Nothing AndAlso b Is Nothing Then Return True
    '    If a Is Nothing Or b Is Nothing Then Return False
    '    If a.Length <> b.Length Then Return False

    '    For i = 0 To a.GetUpperBound(0)
    '        If Not a(i).Equals(b(i)) Then Return False
    '    Next

    '    Return True
    'End Function
End Class