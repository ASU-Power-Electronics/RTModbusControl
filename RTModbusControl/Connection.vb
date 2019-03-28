Option Strict On
Option Explicit On

Imports EasyModbus

' Implements a connection to a Modbus slave device
Public Class Connection
    Private WithEvents _client As ModbusClient
    Private _reconnecting As Boolean = False
    Private ReadOnly _lockObject As New Object

    Public Property Name As String
    Public Property IpAddress As String
    Public Property Port As String
    Public Property DeviceId As String
    Public Property Upstream As Boolean
    Public Property ShutDown As Boolean

    Public Property Client As ModbusClient
        Get
            Return _client
        End Get

        Set
            _client = Value
        End Set
    End Property

    Public Event Connected
    Public Event Disconnected

    Private Event ClientFailure

    ' Raises appropriate events and writes console messages on connection status changes
    Private Sub ConnectedChanged(sender As Object) Handles _client.ConnectedChanged
        SyncLock _lockObject
            If DirectCast(sender, ModbusClient).Connected And _reconnecting
                Console.WriteLine($"{Now.Hour}:{Now.Minute}:{Now.Second}.{Now.Millisecond} - {Name} Reconnected.")
                _reconnecting = False
                RaiseEvent Connected
            ElseIf DirectCast(sender, ModbusClient).Connected And Not ShutDown
                Console.WriteLine($"{Now.Hour}:{Now.Minute}:{Now.Second}.{Now.Millisecond} - {Name} Connected.")
                _reconnecting = False
                RaiseEvent Connected
            ElseIf Not (DirectCast(sender, ModbusClient).Connected Or ShutDown Or _reconnecting) Then
                If DirectCast(sender, ModbusClient).Available(4000)
                    Console.WriteLine($"{Now.Hour}:{Now.Minute}:{Now.Second}.{Now.Millisecond} - {Name} disconnected, attempting to reconnect.")
                    RaiseEvent ClientFailure
                Else
                    Console.WriteLine($"{Now.Hour}:{Now.Minute}:{Now.Second}.{Now.Millisecond} - {Name} unavailable.")
                    RaiseEvent ClientFailure
                End If

                RaiseEvent Disconnected
            End If
        End SyncLock
    End Sub

    ' Instantiates new client and connects with it in case of client failure (unavailable, connection timeout)
    Private Sub RemakeClient() Handles Me.ClientFailure
        SyncLock _lockObject
            _client = New ModbusClient(IpAddress, CType(Port, Integer)) With {
                .UnitIdentifier = CByte(DeviceId),
                .ConnectionTimeout = 1000
                }

            Try
                _client.Connect()
                _reconnecting = True
            Catch ex As Exception
                Console.WriteLine($"{Now.Hour}:{Now.Minute}:{Now.Second}.{Now.Millisecond} - Ex - {Name} Connection error:  {ex.Message}")
                _reconnecting = False
                _client.Disconnect()
            End Try
        End SyncLock
    End Sub
End Class