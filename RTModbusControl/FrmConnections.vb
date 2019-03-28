Option Strict On
Option Explicit On

Imports EasyModbus
Imports RTModbusControl.My.Resources

' TODO: Allow saving connections
Public Class FrmConnections
    ' Updates EnergyCell if it exists, using connection c, updating connection cx in the process, if necessary
    Public Shared Sub UpdateCell(c As Connection, op As String, Optional cx As Connection = Nothing)
        If EnergyCells.All.Exists(Function(p) p.Name.Equals(c.Name)) Then
            If op.Equals("Edit") Then
                If cx IsNot Nothing Then
                    EnergyCells.All.Find(Function(p) p.Name.Equals(cx.Name)).CellConnection = c
                    EnergyCells.All.Find(Function(p) p.Name.Equals(cx.Name)).Name = c.Name
                Else
                    EnergyCells.All.Find(Function(p) p.Name.Equals(c.Name)).CellConnection = c
                    EnergyCells.All.Find(Function(p) p.Name.Equals(c.Name)).Name = c.Name
                End If
            ElseIf op.Equals("Remove") Then
                EnergyCells.All.Remove(EnergyCells.All.Find(Function(p) p.Name.Equals(c.Name)))
            End If
        End If
    End Sub

    ' Populates list on load if connections already exist and aren't shown
    Private Sub PopulateList(sender As Object, e As EventArgs) Handles Me.Load
        ConnectionShortcuts()

        If Connections.All.Count > 0 Then
            For Each c In Connections.All
                If Not LstConnections.Items.ContainsKey(c.Name) Then
                    Dim item As New ListViewItem With {
                        .Name = c.Name}

                    item.SubItems.AddRange({"ClmName", "ClmID", "ClmIP", "ClmPort"})
                    item.SubItems(0).Text = c.DeviceId
                    item.SubItems(1).Text = c.Name
                    item.SubItems(2).Text = c.IpAddress
                    item.SubItems(3).Text = c.Port

                    LstConnections.Items.Add(item)
                End If
            Next
        End If
    End Sub

    ' If the configuration is set to debug, make three test connections automatically.
    <Conditional("DEBUG")>
    Private Shared Sub ConnectionShortcuts()
        For c = 1 To 3
            Connections.All.Add(New Connection With {
                                   .Client = New ModbusClient("129.219.32.77", 1500 + c) With {
                                    .UnitIdentifier = CByte(c),
                                    .ConnectionTimeout = 1000},
                                   .DeviceId = "1",
                                   .IpAddress = "129.219.32.77",
                                   .Name = $"Device {c}",
                                   .Port = (1500 + c).ToString,
                                   .ShutDown = False,
                                   .Upstream = False})
        Next
    End Sub

    ' Opens connection setup form with all fields blank
    Private Sub CreateNewConnection(sender As Object, e As EventArgs) Handles BtnNew.Click
        FrmConnection.InteractionMode = "New"
        FrmConnection.Show()
    End Sub

    ' Opens connection setup form with data pre-filled for editing rather than addition
    Private Sub EditConnection(sender As Object, e As EventArgs) Handles BtnEdit.Click
        If LstConnections.SelectedItems.Count > 0 Then
            Dim item = LstConnections.SelectedItems.Item(0)
            Dim cId = item.SubItems(0).Text
            Dim cName = item.SubItems(1).Text
            Dim cIp = item.SubItems(2).Text
            Dim cPort = item.SubItems(3).Text
            Dim dotCount = 0
            Dim buffer = ""
            Dim ip1 = ""
            Dim ip2 = ""
            Dim ip3 = ""
            Dim ip4 = ""

            For i = 0 To cIp.Length - 1
                If cIp(i).Equals("."c) Then
                    dotCount = dotCount + 1
                    Select Case dotCount
                        Case 1
                            ip1 = buffer
                            buffer = ""
                        Case 2
                            ip2 = buffer
                            buffer = ""
                        Case 3
                            ip3 = buffer
                            buffer = ""
                    End Select
                ElseIf i.Equals(cIp.Length - 1) Then
                    buffer = buffer & cIp(i)
                    ip4 = buffer
                Else
                    buffer = buffer & cIp(i)
                End If
            Next

            FrmConnection.InteractionMode = "Edit"
            FrmConnection.TxtName.Text = cName
            FrmConnection.TxtIP1.Text = ip1
            FrmConnection.TxtIP2.Text = ip2
            FrmConnection.TxtIP3.Text = ip3
            FrmConnection.TxtIP4.Text = ip4
            FrmConnection.TxtPort.Text = cPort
            FrmConnection.TxtDeviceID.Text = cId
            FrmConnection.Show()
        End If
    End Sub

    ' Catastrophe aversion and removal of connection
    Private Sub RemoveConnection(sender As Object, e As EventArgs) Handles BtnRemove.Click
        If LstConnections.SelectedItems.Count > 0 Then
            Dim result As DialogResult

            ' AHHHHHHHHHHH
            result = MessageBox.Show(AllMessages.RemoveConnection,
                                     AllMessages.RemoveConnectionCaption,
                                     MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then
                UpdateCell(
                    Connections.All.Find(Function(p) LstConnections.SelectedItems(0).SubItems(1).Text.Equals(p.Name)),
                    "Remove")
                LstConnections.Items.Remove(LstConnections.SelectedItems(0))
            End If
        End If
    End Sub

    ' Enables or disables Edit/Remove/Test buttons based on list selection
    Private Sub ChangeConnectionSelection(sender As Object, e As EventArgs) Handles LstConnections.ItemSelectionChanged
        If LstConnections.SelectedIndices.Count < 1 Then
            BtnEdit.Enabled = False
            BtnRemove.Enabled = False
            BtnTest.Enabled = False
        Else
            BtnEdit.Enabled = True
            BtnRemove.Enabled = True
            BtnTest.Enabled = True
        End If
    End Sub

    ' Makes specified connection and opens connection testing form
    Private Sub TestConnection(sender As Object, e As EventArgs) Handles BtnTest.Click
        If LstConnections.SelectedItems.Count > 0 Then
            Dim cName As String = LstConnections.SelectedItems(0).SubItems(1).Text
            Dim cnx = Connections.All.Find(Function(p) p.Name.Contains(cName))
            FrmTest.Client = cnx.Client

            If Not FrmTest.Client.Connected Then
                FrmTest.Client.Connect(FrmTest.Client.IPAddress, FrmTest.Client.Port)
            End If

            FrmTest.Show()
        End If
    End Sub
End Class