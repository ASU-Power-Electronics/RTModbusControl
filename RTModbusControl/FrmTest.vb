Option Strict On
Option Explicit On

Imports EasyModbus
Imports RTModbusControl.My.Resources

Public Class FrmTest
    Private _mode As String ' Read or write mode selector
    Public Client As ModbusClient

    ' Populates device information labels and default radio selections on load
    Private Sub PrepareTestForm(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cName As String
        Dim cIp As String
        Dim cPort As String
        Dim cId As String
        Dim cAddress As String

        cId = FrmConnections.LstConnections.SelectedItems(0).SubItems(0).Text
        cName = FrmConnections.LstConnections.SelectedItems(0).SubItems(1).Text
        cIp = FrmConnections.LstConnections.SelectedItems(0).SubItems(2).Text
        cPort = FrmConnections.LstConnections.SelectedItems(0).SubItems(3).Text
        cAddress = cIp & ":" & cPort

        LblName.Text = cName
        LblAddress.Text = cAddress
        LblID.Text = cId

        RadRead1.Checked = True
        RadWrite1.Checked = True
    End Sub

    ' Closes form after user indicates completion of testing
    Private Sub CloseTestForm(sender As Object, e As EventArgs) Handles BtnDone.Click
        Close()
    End Sub

    ' Checks address number and sends the read command to the selected device
    Private Sub ReadFromDevice(sender As Object, e As EventArgs) Handles BtnRead.Click
        Dim addressIsValid As Boolean
        Dim bitResult As Boolean()
        Dim wordResult As Integer()
        Dim result As String
        Dim addressNumber As String = TxtReadNumber.Text

        _mode = "Read"
        addressIsValid = AddressNumberIsValid()

        If addressIsValid Then
            If RadRead1.Checked Then ' Coil, no offset
                bitResult = Client.ReadCoils(CType(addressNumber, Integer) - 1, 1)
                result = ConvertBooleanToBits(bitResult)
                TxtResult.Text += $"{$"Coil at {TxtReadNumber.Text} = {result}"}{vbCrLf}"
            ElseIf RadRead2.Checked Then ' Discrete Input
                addressNumber = (CType(addressNumber, Decimal)).ToString()
                bitResult = Client.ReadDiscreteInputs(CType(addressNumber, Integer) - 1, 1)
                result = ConvertBooleanToBits(bitResult)
                TxtResult.Text += $"{$"Discrete input at {TxtReadNumber.Text} = {result}"}{vbCrLf}"
            ElseIf RadRead3.Checked Then ' Holding Register
                addressNumber = (CType(addressNumber, Decimal)).ToString()
                wordResult = Client.ReadHoldingRegisters(CType(addressNumber, Integer) - 1, 1)
                result = wordResult(0).ToString()
                TxtResult.Text += $"{$"Holding register at {TxtReadNumber.Text} = {result}"}{vbCrLf}"
            ElseIf RadRead4.Checked Then ' Input Register
                addressNumber = (CType(addressNumber, Decimal)).ToString()
                wordResult = Client.ReadInputRegisters(CType(addressNumber, Integer) - 1, 1)
                result = wordResult(0).ToString()
                TxtResult.Text += $"{$"Input register at {TxtReadNumber.Text} = {result}"}{vbCrLf}"
            End If
        Else
            MessageBox.Show(AllMessages.ErrorAddressInvalid, AllMessages.ErrorAddressInvalidCaption,
                            MessageBoxButtons.OK)
        End If
    End Sub

    ' Checks address number and sends the write command to the selected device
    Private Sub WriteToDevice(sender As Object, e As EventArgs) Handles BtnWrite.Click
        Dim addressIsValid As Boolean
        Dim addressIsRange As Boolean
        Dim addressNumber As String = TxtWriteNumber.Text
        Dim registerValue As String = TxtValue.Text

        _mode = "Write"
        addressIsValid = AddressNumberIsValid()
        addressIsRange = AddressNumberIsRange(addressNumber)

        If addressIsValid Then
            If addressIsRange Then
                Dim addresses As Collection = ParseRange(addressNumber, "address")
                Dim values As Collection = ParseRange(registerValue, "value")
                Dim numValues = values.Count
                Dim booleanValues(numValues - 1) As Boolean
                Dim integerValues(numValues - 1) As Integer

                If RadWrite1.Checked Then ' Multiple Coils
                    Dim i = 0

                    For Each v In values
                        If v.ToString.Equals("0") Then
                            booleanValues(i) = False
                        ElseIf v.ToString.Equals("1") Then
                            booleanValues(i) = True
                        End If

                        i = i + 1
                    Next

                    Client.WriteMultipleCoils(CType(addresses(1), Integer) - 1, booleanValues)
                    TxtResult.Text += $"{$"Coils at {TxtWriteNumber.Text} written."}{vbCrLf}"
                ElseIf RadWrite2.Checked Then ' Multiple Registers
                    Dim i = 0

                    For Each v In values
                        integerValues(i) = CType(v, Integer)
                        i = i + 1
                    Next

                    Client.WriteMultipleRegisters(CType(addresses(1), Integer) - 1, integerValues)
                    TxtResult.Text += $"{$"Registers at {TxtWriteNumber.Text} written."}{vbCrLf}"
                End If
            ElseIf RadWrite1.Checked Then ' Single Coil
                Client.WriteSingleCoil(CType(addressNumber, Integer) - 1, CType(registerValue, Boolean))
                TxtResult.Text += $"{$"Coil at {TxtWriteNumber.Text} written."}{vbCrLf}"
            ElseIf RadWrite2.Checked Then ' Single Register
                Client.WriteSingleRegister(CType(addressNumber, Integer) - 1, CType(registerValue, Integer))
                TxtResult.Text += $"{$"Register at {TxtWriteNumber.Text} written."}{vbCrLf}"
            End If
        Else
            MessageBox.Show(AllMessages.ErrorAddressRangeInvalid, AllMessages.ErrorAddressInvalidCaption,
                            MessageBoxButtons.OK)
        End If
    End Sub

    ' Input validation function for this form
    Private Function AddressNumberIsValid() As Boolean
        Dim address As String

        Select Case _mode
            Case "Read"
                address = TxtReadNumber.Text

                If (address.Count > 0) Then
                    If (ChkHexRead.Checked) Then
                        If _
                            Convert.ToInt16(address, 16) >= Convert.ToInt16("0", 16) And
                             Convert.ToInt16(address, 16) < Convert.ToInt16("270F", 16) Then
                            Return True
                        Else
                            Return False
                        End If
                    Else
                        If Convert.ToInt16(address) > 0 And Convert.ToInt16(address) < 10000 Then
                            Return True
                        Else
                            Return False
                        End If
                    End If
                Else
                    Return False
                End If
            Case "Write"
                Dim addressIsRange As Boolean
                address = TxtWriteNumber.Text

                If address.Count > 0 Then ' field is non-empty
                    addressIsRange = AddressNumberIsRange(address)

                    If addressIsRange Then ' we need to handle a range of registers/values
                        Dim addresses As Collection = ParseRange(address, "address")

                        For Each a As String In addresses
                            If ChkHexWrite.Checked Then
                                If _
                                    Not _
                                    (Convert.ToInt16(a, 16) >= Convert.ToInt16("0", 16) And
                                     Convert.ToInt16(a, 16) < Convert.ToInt16("270F", 16)) Then
                                    Return False
                                End If
                            Else
                                If Not (Convert.ToInt16(a) > 0 And Convert.ToInt16(a) < 10000) Then
                                    Return False
                                End If
                            End If
                        Next ' a

                        Return True
                    Else
                        If (ChkHexRead.Checked) Then
                            If _
                                Convert.ToInt16(address, 16) >= Convert.ToInt16("0", 16) And
                                 Convert.ToInt16(address, 16) < Convert.ToInt16("270F", 16) Then
                                Return True
                            Else
                                Return False
                            End If
                        Else
                            If Convert.ToInt16(address) > 0 And Convert.ToInt16(address) < 10000 Then
                                Return True
                            Else
                                Return False
                            End If
                        End If
                    End If
                Else
                    Return False
                End If
        End Select

        Return False ' catch-all for completeness
    End Function

    ' Checks input register address text to determine if it is a range.
    Private Shared Function AddressNumberIsRange(a As String) As Boolean
        For i = 0 To a.Length - 1
            If a(i).Equals("-"c) Then
                Return True
            End If
        Next

        Return False ' if we get here, it's not a range
    End Function

    ' Parses a range of input register addresses or values to use in testing.
    Private Shared Function ParseRange(a As String, mode As String) As Collection
        Dim buffer = ""
        Dim vals As New Collection()

        If mode.Equals("address") Then
            For i = 0 To a.Length - 1 ' read character by character
                If a(i).Equals("-"c) Then ' we have a range indicator
                    vals.Add(buffer)
                    buffer = ""
                ElseIf a(i).Equals(" "c) Then
                    ' don't parse whitespace
                ElseIf i.Equals(a.Length - 1) Then ' we're at the end
                    buffer = buffer & a(i)
                    vals.Add(buffer)
                Else ' just add the next character to the buffer
                    buffer = buffer & a(i)
                End If
            Next ' i
        ElseIf mode.Equals("value") Then
            For i = 0 To a.Length - 1 ' read character by character
                If a(i).Equals(","c) Or a(i).Equals(";"c) Then ' we have a delimiter
                    vals.Add(buffer)
                    buffer = ""
                ElseIf a(i).Equals(" "c) Then
                    ' don't parse whitespace
                ElseIf i.Equals(a.Length - 1) Then ' we're at the end
                    buffer = buffer & a(i)
                    vals.Add(buffer)
                Else ' just add the next character to the buffer
                    buffer = buffer & a(i)
                End If
            Next ' i
        End If

        Return vals
    End Function

    ' Converts boolean array to bits for display
    Private Shared Function ConvertBooleanToBits(boolBits As IList(Of Boolean)) As String
        Dim bits As String = String.Empty

        For bit = 0 To boolBits.Count - 1
            If boolBits(bit) Then
                bits = $"{bits}1"
            Else
                bits = $"{bits}0"
            End If
        Next ' bit

        Return bits
    End Function
End Class