Option Strict On
Option Explicit On

' Implements a tertiary controller, which provides the system frequency offset
' Specific to Opal-RT:
' 0 - sgn(Δω) + 1
' 1 - |Δω| * 1000
Public Class UpstreamController
    Public Property Name As String
    Public Property DeltaOmega As Double
    Public Property ControllerConnection As Connection

    Public Sub ReadDeltaOmega()
        Dim result As Integer() = {0, 0}

        Try
            result = ControllerConnection.Client.ReadInputRegisters(0, 2)
            Console.WriteLine(
                $"{Now.Hour}:{Now.Minute}:{Now.Second}.{Now.Millisecond} - Controller {Name} Meas. Read:  ({ _
                                 String.Join(", ", Array.ConvertAll(result, Function(x) x.ToString()))})")
        Catch ex As Exception
            Console.WriteLine($"{Now.Hour}:{Now.Minute}:{Now.Second}.{Now.Millisecond} - Ex - Controller {Name}:  {ex.Message}")
        End Try

        DeltaOmega = (result(0) - 1)*result(1)/1000
    End Sub
End Class