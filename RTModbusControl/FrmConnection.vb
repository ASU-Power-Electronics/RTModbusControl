Option Strict On
Option Explicit On

Imports EasyModbus
Imports RTModbusControl.My.Resources

Public Class FrmConnection
    Public InteractionMode As String
    Private _tempName As String

    ' On form load, if editing, store the name in case it is changed so that we can replace the proper connection
    Private Sub NameSave(sender As Object, e As EventArgs) Handles MyBase.Load
        If InteractionMode.Equals("Edit") Then
            _tempName = TxtName.Text
        End If
    End Sub

    ' Closes form and creates a new Connection object and a ListItem or edits their pre-existing instances
    Private Sub SaveConnection(sender As Object, e As EventArgs) Handles BtnSave.Click
        Dim validity As Integer
        validity = ValidateInput()

        If validity.Equals(0) Then
            Dim id As String = TxtDeviceID.Text
            Dim cName As String = TxtName.Text
            Dim ip As String = TxtIP1.Text & "." & TxtIP2.Text & "." & TxtIP3.Text & "." & TxtIP4.Text
            Dim port As String = TxtPort.Text
            Dim item As New ListViewItem
            Dim connection As New Connection

            item.SubItems.AddRange({"ClmName", "ClmID", "ClmIP", "ClmPort"})
            item.SubItems(0).Text = id
            item.SubItems(1).Text = cName
            item.SubItems(2).Text = ip
            item.SubItems(3).Text = port

            connection.Name = cName
            connection.DeviceId = id
            connection.IpAddress = ip
            connection.Port = port
            connection.Upstream = chkTertiaryController.Checked
            connection.Client = New ModbusClient(ip, CType(port, Integer)) With {
                .UnitIdentifier = CByte(id),
                .ConnectionTimeout = 201
                }

            If InteractionMode.Equals("New") Then
                Connections.All.Add(connection)
                FrmConnections.LstConnections.Items.Add(item)
            ElseIf InteractionMode.Equals("Edit") Then
                Dim cnxToEdit = Connections.All.Find(Function(p) p.Name.Equals(_tempName))
                Connections.All.Remove(cnxToEdit)
                Connections.All.Add(connection)
                FrmConnections.LstConnections.SelectedItems(0).Remove()
                FrmConnections.LstConnections.Items.Add(item)
                FrmConnections.UpdateCell(connection, "Edit", cnxToEdit)
            End If

            DialogResult = DialogResult.OK
            Close()
        ElseIf validity.Equals(1) Then ' Name invalid
            MessageBox.Show(AllMessages.ErrorNameInvalid, AllMessages.ErrorNameInvalidCaption, MessageBoxButtons.OK)
        ElseIf validity.Equals(2) Then ' IP invalid
            MessageBox.Show(AllMessages.ErrorIPInvalid, AllMessages.ErrorIPInvalidCaption, MessageBoxButtons.OK)
        ElseIf validity.Equals(3) Then ' Port invalid
            MessageBox.Show(AllMessages.ErrorPortInvalid, AllMessages.ErrorPortInvalidCaption, MessageBoxButtons.OK)
        Else ' ID invalid
            MessageBox.Show(AllMessages.ErrorIDInvalid, AllMessages.ErrorIDInvalidCaption, MessageBoxButtons.OK)
        End If
    End Sub

    ' Closes form without instantiating a new ListItem
    Private Sub CancelConnection(sender As Object, e As EventArgs) Handles BtnCancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    ' Treat period as tab while in first 3 IP text boxes
    Private Sub TreatIpDotAsTab(sender As Object, e As KeyEventArgs) _
        Handles TxtIP1.KeyDown, TxtIP2.KeyDown, TxtIP3.KeyDown
        If e.KeyCode.Equals(Keys.Decimal) Or e.KeyCode.Equals(Keys.OemPeriod) Then
            SelectNextControl(ActiveControl, True, True, True, False)
        End If
    End Sub

    ' Validate form input for both completion and content
    Private Function ValidateInput() As Integer
        Dim nameIsValid = False
        Dim ip1IsValid = False
        Dim ip2IsValid = False
        Dim ip3IsValid = False
        Dim ip4IsValid = False
        Dim ipIsValid = False
        Dim portIsValid = False
        Dim idIsValid = False
        Dim nameMatches = False
        Dim cName As String
        Dim junk As Integer

        ' Check name (loosely) for validity
        If TxtName.Text.Count > 0 Then
            For Each c As ListViewItem In FrmConnections.LstConnections.Items
                cName = c.SubItems(1).Text
                If cName.Equals(TxtName.Text) And Not InteractionMode.Equals("Edit") Then
                    nameMatches = True
                    Exit For
                End If
            Next

            If Not nameMatches Then
                nameIsValid = True
            End If
        End If

        ' Check IP boxes for existence and range (not route or routability)
        If TxtIP1.Text.Count > 0 Then
            junk = Convert.ToInt16(TxtIP1.Text)
            If junk >= 0 And junk < 256 Then
                ip1IsValid = True
            End If
        End If

        If TxtIP2.Text.Count > 0 Then
            junk = Convert.ToInt16(TxtIP2.Text)
            If junk >= 0 And junk < 256 Then
                ip2IsValid = True
            End If
        End If

        If TxtIP3.Text.Count > 0 Then
            junk = Convert.ToInt16(TxtIP3.Text)
            If junk >= 0 And junk < 256 Then
                ip3IsValid = True
            End If
        End If

        If TxtIP4.Text.Count > 0 Then
            junk = Convert.ToInt16(TxtIP4.Text)
            If junk >= 0 And junk < 256 Then
                ip4IsValid = True
            End If
        End If

        If ip1IsValid And ip2IsValid And ip3IsValid And ip4IsValid Then
            ipIsValid = True
        End If

        ' Check port for existence and range
        If TxtPort.Text.Count > 0 Then
            junk = Convert.ToInt16(TxtPort.Text)
            If junk > 0 And junk < 65536 Then
                portIsValid = True
            End If
        End If

        ' Checks slave device ID for existence and range
        If TxtDeviceID.Text.Count > 0 Then
            junk = Convert.ToInt16(TxtDeviceID.Text)
            If junk > 0 And junk < 248 Then
                idIsValid = True
            End If
        End If

        ' Return validity result based on all validations
        If nameIsValid Then
            If ipIsValid Then
                If portIsValid Then
                    If idIsValid Then
                        Return 0
                    Else
                        Return 4
                    End If
                Else
                    Return 3
                End If
            Else
                Return 2
            End If
        Else
            Return 1
        End If
    End Function
End Class