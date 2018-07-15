Imports System.ComponentModel
Imports Microsoft.VisualBasic.CompilerServices

<DesignerGenerated()>
Partial Class FrmTest
    Inherits Form

    'Form overrides dispose to clean up the component list.
    <DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.LblName = New Label()
        Me.TxtReadNumber = New TextBox()
        Me.BtnRead = New Button()
        Me.GrpDeviceInfo = New GroupBox()
        Me.GrpDeviceID = New GroupBox()
        Me.LblID = New Label()
        Me.GrpDeviceAddress = New GroupBox()
        Me.LblAddress = New Label()
        Me.GrpDeviceName = New GroupBox()
        Me.GrpRead = New GroupBox()
        Me.ChkHexRead = New CheckBox()
        Me.RadRead4 = New RadioButton()
        Me.RadRead3 = New RadioButton()
        Me.RadRead2 = New RadioButton()
        Me.LblNumber1 = New Label()
        Me.RadRead1 = New RadioButton()
        Me.GrpWrite = New GroupBox()
        Me.ChkHexWrite = New CheckBox()
        Me.LblNumber2 = New Label()
        Me.BtnWrite = New Button()
        Me.TxtWriteNumber = New TextBox()
        Me.RadWrite2 = New RadioButton()
        Me.RadWrite1 = New RadioButton()
        Me.BtnDone = New Button()
        Me.GrpResult = New GroupBox()
        Me.TxtResult = New TextBox()
        Me.TxtValue = New TextBox()
        Me.LblValue = New Label()
        Me.GrpDeviceInfo.SuspendLayout()
        Me.GrpDeviceID.SuspendLayout()
        Me.GrpDeviceAddress.SuspendLayout()
        Me.GrpDeviceName.SuspendLayout()
        Me.GrpRead.SuspendLayout()
        Me.GrpWrite.SuspendLayout()
        Me.GrpResult.SuspendLayout()
        Me.SuspendLayout()
        '
        'LblName
        '
        Me.LblName.AutoSize = True
        Me.LblName.Location = New Point(6, 16)
        Me.LblName.Name = "LblName"
        Me.LblName.Size = New Size(131, 13)
        Me.LblName.TabIndex = 2
        Me.LblName.Text = "Device Name Placeholder"
        '
        'TxtReadNumber
        '
        Me.TxtReadNumber.Location = New Point(6, 133)
        Me.TxtReadNumber.Name = "TxtReadNumber"
        Me.TxtReadNumber.Size = New Size(47, 20)
        Me.TxtReadNumber.TabIndex = 13
        '
        'BtnRead
        '
        Me.BtnRead.Location = New Point(154, 19)
        Me.BtnRead.Name = "BtnRead"
        Me.BtnRead.Size = New Size(69, 38)
        Me.BtnRead.TabIndex = 15
        Me.BtnRead.Text = "&Read"
        Me.BtnRead.UseVisualStyleBackColor = True
        '
        'GrpDeviceInfo
        '
        Me.GrpDeviceInfo.Controls.Add(Me.GrpDeviceID)
        Me.GrpDeviceInfo.Controls.Add(Me.GrpDeviceAddress)
        Me.GrpDeviceInfo.Controls.Add(Me.GrpDeviceName)
        Me.GrpDeviceInfo.Location = New Point(12, 12)
        Me.GrpDeviceInfo.Name = "GrpDeviceInfo"
        Me.GrpDeviceInfo.Size = New Size(383, 68)
        Me.GrpDeviceInfo.TabIndex = 0
        Me.GrpDeviceInfo.TabStop = False
        Me.GrpDeviceInfo.Text = "Device Information"
        '
        'GrpDeviceID
        '
        Me.GrpDeviceID.Controls.Add(Me.LblID)
        Me.GrpDeviceID.Location = New Point(292, 19)
        Me.GrpDeviceID.Name = "GrpDeviceID"
        Me.GrpDeviceID.Size = New Size(74, 38)
        Me.GrpDeviceID.TabIndex = 5
        Me.GrpDeviceID.TabStop = False
        Me.GrpDeviceID.Text = "Device ID"
        '
        'LblID
        '
        Me.LblID.AutoSize = True
        Me.LblID.Location = New Point(6, 16)
        Me.LblID.Name = "LblID"
        Me.LblID.Size = New Size(25, 13)
        Me.LblID.TabIndex = 6
        Me.LblID.Text = "000"
        '
        'GrpDeviceAddress
        '
        Me.GrpDeviceAddress.Controls.Add(Me.LblAddress)
        Me.GrpDeviceAddress.Location = New Point(154, 19)
        Me.GrpDeviceAddress.Name = "GrpDeviceAddress"
        Me.GrpDeviceAddress.Size = New Size(132, 38)
        Me.GrpDeviceAddress.TabIndex = 3
        Me.GrpDeviceAddress.TabStop = False
        Me.GrpDeviceAddress.Text = "Address"
        '
        'LblAddress
        '
        Me.LblAddress.AutoSize = True
        Me.LblAddress.Location = New Point(6, 16)
        Me.LblAddress.Name = "LblAddress"
        Me.LblAddress.Size = New Size(121, 13)
        Me.LblAddress.TabIndex = 4
        Me.LblAddress.Text = "255.255.255.255:65535"
        '
        'GrpDeviceName
        '
        Me.GrpDeviceName.Controls.Add(Me.LblName)
        Me.GrpDeviceName.Location = New Point(6, 19)
        Me.GrpDeviceName.Name = "GrpDeviceName"
        Me.GrpDeviceName.Size = New Size(142, 38)
        Me.GrpDeviceName.TabIndex = 1
        Me.GrpDeviceName.TabStop = False
        Me.GrpDeviceName.Text = "Name"
        '
        'GrpRead
        '
        Me.GrpRead.Controls.Add(Me.ChkHexRead)
        Me.GrpRead.Controls.Add(Me.RadRead4)
        Me.GrpRead.Controls.Add(Me.RadRead3)
        Me.GrpRead.Controls.Add(Me.BtnRead)
        Me.GrpRead.Controls.Add(Me.RadRead2)
        Me.GrpRead.Controls.Add(Me.LblNumber1)
        Me.GrpRead.Controls.Add(Me.RadRead1)
        Me.GrpRead.Controls.Add(Me.TxtReadNumber)
        Me.GrpRead.Location = New Point(12, 86)
        Me.GrpRead.Name = "GrpRead"
        Me.GrpRead.Size = New Size(233, 162)
        Me.GrpRead.TabIndex = 7
        Me.GrpRead.TabStop = False
        Me.GrpRead.Text = "Read Commands"
        '
        'ChkHexRead
        '
        Me.ChkHexRead.AutoSize = True
        Me.ChkHexRead.Location = New Point(59, 135)
        Me.ChkHexRead.Name = "ChkHexRead"
        Me.ChkHexRead.Size = New Size(87, 17)
        Me.ChkHexRead.TabIndex = 14
        Me.ChkHexRead.Text = "Hexadecimal"
        Me.ChkHexRead.UseVisualStyleBackColor = True
        '
        'RadRead4
        '
        Me.RadRead4.AutoSize = True
        Me.RadRead4.Location = New Point(6, 89)
        Me.RadRead4.Name = "RadRead4"
        Me.RadRead4.Size = New Size(91, 17)
        Me.RadRead4.TabIndex = 11
        Me.RadRead4.TabStop = True
        Me.RadRead4.Text = "Input Register"
        Me.RadRead4.UseVisualStyleBackColor = True
        '
        'RadRead3
        '
        Me.RadRead3.AutoSize = True
        Me.RadRead3.Location = New Point(6, 66)
        Me.RadRead3.Name = "RadRead3"
        Me.RadRead3.Size = New Size(103, 17)
        Me.RadRead3.TabIndex = 10
        Me.RadRead3.TabStop = True
        Me.RadRead3.Text = "Holding Register"
        Me.RadRead3.UseVisualStyleBackColor = True
        '
        'RadRead2
        '
        Me.RadRead2.AutoSize = True
        Me.RadRead2.Location = New Point(6, 43)
        Me.RadRead2.Name = "RadRead2"
        Me.RadRead2.Size = New Size(91, 17)
        Me.RadRead2.TabIndex = 9
        Me.RadRead2.TabStop = True
        Me.RadRead2.Text = "Discrete Input"
        Me.RadRead2.UseVisualStyleBackColor = True
        '
        'LblNumber1
        '
        Me.LblNumber1.AutoSize = True
        Me.LblNumber1.Location = New Point(3, 117)
        Me.LblNumber1.Name = "LblNumber1"
        Me.LblNumber1.Size = New Size(108, 13)
        Me.LblNumber1.TabIndex = 12
        Me.LblNumber1.Text = "Coil/Register Number"
        '
        'RadRead1
        '
        Me.RadRead1.AutoSize = True
        Me.RadRead1.Location = New Point(6, 19)
        Me.RadRead1.Name = "RadRead1"
        Me.RadRead1.Size = New Size(42, 17)
        Me.RadRead1.TabIndex = 8
        Me.RadRead1.TabStop = True
        Me.RadRead1.Text = "Coil"
        Me.RadRead1.UseVisualStyleBackColor = True
        '
        'GrpWrite
        '
        Me.GrpWrite.Controls.Add(Me.LblValue)
        Me.GrpWrite.Controls.Add(Me.TxtValue)
        Me.GrpWrite.Controls.Add(Me.ChkHexWrite)
        Me.GrpWrite.Controls.Add(Me.LblNumber2)
        Me.GrpWrite.Controls.Add(Me.BtnWrite)
        Me.GrpWrite.Controls.Add(Me.TxtWriteNumber)
        Me.GrpWrite.Controls.Add(Me.RadWrite2)
        Me.GrpWrite.Controls.Add(Me.RadWrite1)
        Me.GrpWrite.Location = New Point(12, 254)
        Me.GrpWrite.Name = "GrpWrite"
        Me.GrpWrite.Size = New Size(233, 153)
        Me.GrpWrite.TabIndex = 16
        Me.GrpWrite.TabStop = False
        Me.GrpWrite.Text = "Write Commands"
        '
        'ChkHexWrite
        '
        Me.ChkHexWrite.AutoSize = True
        Me.ChkHexWrite.Location = New Point(128, 89)
        Me.ChkHexWrite.Name = "ChkHexWrite"
        Me.ChkHexWrite.Size = New Size(87, 17)
        Me.ChkHexWrite.TabIndex = 21
        Me.ChkHexWrite.Text = "Hexadecimal"
        Me.ChkHexWrite.UseVisualStyleBackColor = True
        '
        'LblNumber2
        '
        Me.LblNumber2.AutoSize = True
        Me.LblNumber2.Location = New Point(3, 71)
        Me.LblNumber2.Name = "LblNumber2"
        Me.LblNumber2.Size = New Size(119, 13)
        Me.LblNumber2.TabIndex = 19
        Me.LblNumber2.Text = "Coil/Register Number(s)"
        '
        'BtnWrite
        '
        Me.BtnWrite.Location = New Point(154, 19)
        Me.BtnWrite.Name = "BtnWrite"
        Me.BtnWrite.Size = New Size(69, 38)
        Me.BtnWrite.TabIndex = 22
        Me.BtnWrite.Text = "&Write"
        Me.BtnWrite.UseVisualStyleBackColor = True
        '
        'TxtWriteNumber
        '
        Me.TxtWriteNumber.Location = New Point(6, 87)
        Me.TxtWriteNumber.Name = "TxtWriteNumber"
        Me.TxtWriteNumber.Size = New Size(116, 20)
        Me.TxtWriteNumber.TabIndex = 20
        '
        'RadWrite2
        '
        Me.RadWrite2.AutoSize = True
        Me.RadWrite2.Location = New Point(6, 42)
        Me.RadWrite2.Name = "RadWrite2"
        Me.RadWrite2.Size = New Size(75, 17)
        Me.RadWrite2.TabIndex = 18
        Me.RadWrite2.TabStop = True
        Me.RadWrite2.Text = "Register(s)"
        Me.RadWrite2.UseVisualStyleBackColor = True
        '
        'RadWrite1
        '
        Me.RadWrite1.AutoSize = True
        Me.RadWrite1.Location = New Point(6, 19)
        Me.RadWrite1.Name = "RadWrite1"
        Me.RadWrite1.Size = New Size(53, 17)
        Me.RadWrite1.TabIndex = 17
        Me.RadWrite1.TabStop = True
        Me.RadWrite1.Text = "Coil(s)"
        Me.RadWrite1.UseVisualStyleBackColor = True
        '
        'BtnDone
        '
        Me.BtnDone.Location = New Point(401, 12)
        Me.BtnDone.Name = "BtnDone"
        Me.BtnDone.Size = New Size(91, 68)
        Me.BtnDone.TabIndex = 26
        Me.BtnDone.Text = "&Done"
        Me.BtnDone.UseVisualStyleBackColor = True
        '
        'GrpResult
        '
        Me.GrpResult.Controls.Add(Me.TxtResult)
        Me.GrpResult.Location = New Point(251, 86)
        Me.GrpResult.Name = "GrpResult"
        Me.GrpResult.Size = New Size(241, 321)
        Me.GrpResult.TabIndex = 24
        Me.GrpResult.TabStop = False
        Me.GrpResult.Text = "Result"
        '
        'TxtResult
        '
        Me.TxtResult.Location = New Point(6, 19)
        Me.TxtResult.Multiline = True
        Me.TxtResult.Name = "TxtResult"
        Me.TxtResult.ReadOnly = True
        Me.TxtResult.Size = New Size(229, 295)
        Me.TxtResult.TabIndex = 25
        Me.TxtResult.TabStop = False
        Me.TxtResult.WordWrap = False
        '
        'TxtValue
        '
        Me.TxtValue.Location = New Point(6, 126)
        Me.TxtValue.Name = "TxtValue"
        Me.TxtValue.Size = New Size(116, 20)
        Me.TxtValue.TabIndex = 23
        '
        'LblValue
        '
        Me.LblValue.AutoSize = True
        Me.LblValue.Location = New Point(3, 110)
        Me.LblValue.Name = "LblValue"
        Me.LblValue.Size = New Size(85, 13)
        Me.LblValue.TabIndex = 22
        Me.LblValue.Text = "Value(s) to Write"
        '
        'FrmTest
        '
        Me.AcceptButton = Me.BtnDone
        Me.AutoScaleDimensions = New SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.ClientSize = New Size(507, 421)
        Me.Controls.Add(Me.GrpResult)
        Me.Controls.Add(Me.BtnDone)
        Me.Controls.Add(Me.GrpWrite)
        Me.Controls.Add(Me.GrpRead)
        Me.Controls.Add(Me.GrpDeviceInfo)
        Me.Name = "FrmTest"
        Me.Text = "Test Connection"
        Me.GrpDeviceInfo.ResumeLayout(False)
        Me.GrpDeviceID.ResumeLayout(False)
        Me.GrpDeviceID.PerformLayout()
        Me.GrpDeviceAddress.ResumeLayout(False)
        Me.GrpDeviceAddress.PerformLayout()
        Me.GrpDeviceName.ResumeLayout(False)
        Me.GrpDeviceName.PerformLayout()
        Me.GrpRead.ResumeLayout(False)
        Me.GrpRead.PerformLayout()
        Me.GrpWrite.ResumeLayout(False)
        Me.GrpWrite.PerformLayout()
        Me.GrpResult.ResumeLayout(False)
        Me.GrpResult.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LblName As Label
    Friend WithEvents TxtReadNumber As TextBox
    Friend WithEvents BtnRead As Button
    Friend WithEvents GrpDeviceInfo As GroupBox
    Friend WithEvents GrpRead As GroupBox
    Friend WithEvents GrpWrite As GroupBox
    Friend WithEvents LblAddress As Label
    Friend WithEvents LblID As Label
    Friend WithEvents LblNumber1 As Label
    Friend WithEvents GrpDeviceAddress As GroupBox
    Friend WithEvents GrpDeviceName As GroupBox
    Friend WithEvents GrpDeviceID As GroupBox
    Friend WithEvents RadRead4 As RadioButton
    Friend WithEvents RadRead3 As RadioButton
    Friend WithEvents RadRead2 As RadioButton
    Friend WithEvents RadRead1 As RadioButton
    Friend WithEvents BtnWrite As Button
    Friend WithEvents BtnDone As Button
    Friend WithEvents LblNumber2 As Label
    Friend WithEvents TxtWriteNumber As TextBox
    Friend WithEvents RadWrite2 As RadioButton
    Friend WithEvents RadWrite1 As RadioButton
    Friend WithEvents GrpResult As GroupBox
    Friend WithEvents TxtResult As TextBox
    Friend WithEvents ChkHexRead As CheckBox
    Friend WithEvents ChkHexWrite As CheckBox
    Friend WithEvents LblValue As Label
    Friend WithEvents TxtValue As TextBox
End Class
