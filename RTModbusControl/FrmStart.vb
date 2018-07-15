Option Strict On
Option Explicit On

Public Class FrmStart
    ' Opens connections management form
    Private Sub OpenConnectionsForm(sender As Object, e As EventArgs) Handles BtnConnections.Click
        FrmConnections.Show()
    End Sub

    ' Opens controls management form
    Private Sub OpenDeviceInterfaceForm(sender As Object, e As EventArgs) Handles BtnControls.Click
        FrmControls.Show()
    End Sub

    ' Opens About dialog
    Private Sub OpenAbout(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        AboutBox1.Show()
    End Sub

    ' Handles program exit on form close, including disconnecting all clients, then exits the application
    Private Shared Sub ExitProgram(sender As Object, e As EventArgs) Handles MyBase.FormClosing
        For Each c As EnergyCell In EnergyCells.All
            If c.CellConnection.Client.Connected Then
                c.CellConnection.ShutDown = True
                c.CellConnection.Client.Disconnect()
            End If
        Next

        For Each c As UpstreamController In UpstreamControllers.All
            If c.ControllerConnection.Client.Connected Then
                c.ControllerConnection.ShutDown = True
                c.ControllerConnection.Client.Disconnect()
            End If
        Next

        Application.Exit()
    End Sub
End Class