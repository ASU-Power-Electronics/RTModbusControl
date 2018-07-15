Imports System.ComponentModel
Imports Microsoft.VisualBasic.CompilerServices

<DesignerGenerated()>
Partial Class FrmConnection
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
        Me.BtnSave = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.LblIP = New System.Windows.Forms.Label()
        Me.LblDeviceID = New System.Windows.Forms.Label()
        Me.TxtDeviceID = New System.Windows.Forms.TextBox()
        Me.TxtPort = New System.Windows.Forms.TextBox()
        Me.LblPort = New System.Windows.Forms.Label()
        Me.TxtIP1 = New System.Windows.Forms.MaskedTextBox()
        Me.TxtIP2 = New System.Windows.Forms.MaskedTextBox()
        Me.TxtIP3 = New System.Windows.Forms.MaskedTextBox()
        Me.TxtIP4 = New System.Windows.Forms.MaskedTextBox()
        Me.LblPeriod1 = New System.Windows.Forms.Label()
        Me.LblPeriod2 = New System.Windows.Forms.Label()
        Me.LblPeriod3 = New System.Windows.Forms.Label()
        Me.LblName = New System.Windows.Forms.Label()
        Me.TxtName = New System.Windows.Forms.TextBox()
        Me.chkTertiaryController = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'BtnSave
        '
        Me.BtnSave.Location = New System.Drawing.Point(94, 142)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(71, 39)
        Me.BtnSave.TabIndex = 14
        Me.BtnSave.Text = "&Save"
        Me.BtnSave.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnCancel.Location = New System.Drawing.Point(171, 142)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(71, 39)
        Me.BtnCancel.TabIndex = 15
        Me.BtnCancel.Text = "&Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'LblIP
        '
        Me.LblIP.AutoSize = True
        Me.LblIP.Location = New System.Drawing.Point(21, 44)
        Me.LblIP.Name = "LblIP"
        Me.LblIP.Size = New System.Drawing.Size(58, 13)
        Me.LblIP.TabIndex = 2
        Me.LblIP.Text = "IP Address"
        '
        'LblDeviceID
        '
        Me.LblDeviceID.AutoSize = True
        Me.LblDeviceID.Location = New System.Drawing.Point(21, 96)
        Me.LblDeviceID.Name = "LblDeviceID"
        Me.LblDeviceID.Size = New System.Drawing.Size(55, 13)
        Me.LblDeviceID.TabIndex = 12
        Me.LblDeviceID.Text = "Device ID"
        '
        'TxtDeviceID
        '
        Me.TxtDeviceID.Location = New System.Drawing.Point(85, 93)
        Me.TxtDeviceID.Name = "TxtDeviceID"
        Me.TxtDeviceID.Size = New System.Drawing.Size(157, 20)
        Me.TxtDeviceID.TabIndex = 13
        '
        'TxtPort
        '
        Me.TxtPort.Location = New System.Drawing.Point(85, 67)
        Me.TxtPort.Name = "TxtPort"
        Me.TxtPort.Size = New System.Drawing.Size(157, 20)
        Me.TxtPort.TabIndex = 11
        '
        'LblPort
        '
        Me.LblPort.AutoSize = True
        Me.LblPort.Location = New System.Drawing.Point(21, 70)
        Me.LblPort.Name = "LblPort"
        Me.LblPort.Size = New System.Drawing.Size(26, 13)
        Me.LblPort.TabIndex = 10
        Me.LblPort.Text = "Port"
        '
        'TxtIP1
        '
        Me.TxtIP1.Location = New System.Drawing.Point(85, 41)
        Me.TxtIP1.Mask = "099"
        Me.TxtIP1.Name = "TxtIP1"
        Me.TxtIP1.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.TxtIP1.Size = New System.Drawing.Size(25, 20)
        Me.TxtIP1.TabIndex = 3
        '
        'TxtIP2
        '
        Me.TxtIP2.Location = New System.Drawing.Point(129, 41)
        Me.TxtIP2.Mask = "099"
        Me.TxtIP2.Name = "TxtIP2"
        Me.TxtIP2.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.TxtIP2.Size = New System.Drawing.Size(25, 20)
        Me.TxtIP2.TabIndex = 5
        '
        'TxtIP3
        '
        Me.TxtIP3.Location = New System.Drawing.Point(173, 41)
        Me.TxtIP3.Mask = "099"
        Me.TxtIP3.Name = "TxtIP3"
        Me.TxtIP3.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.TxtIP3.Size = New System.Drawing.Size(25, 20)
        Me.TxtIP3.TabIndex = 7
        '
        'TxtIP4
        '
        Me.TxtIP4.Location = New System.Drawing.Point(217, 41)
        Me.TxtIP4.Mask = "099"
        Me.TxtIP4.Name = "TxtIP4"
        Me.TxtIP4.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.TxtIP4.Size = New System.Drawing.Size(25, 20)
        Me.TxtIP4.TabIndex = 9
        '
        'LblPeriod1
        '
        Me.LblPeriod1.AutoSize = True
        Me.LblPeriod1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPeriod1.Location = New System.Drawing.Point(114, 44)
        Me.LblPeriod1.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.LblPeriod1.Name = "LblPeriod1"
        Me.LblPeriod1.Size = New System.Drawing.Size(11, 13)
        Me.LblPeriod1.TabIndex = 4
        Me.LblPeriod1.Text = "."
        '
        'LblPeriod2
        '
        Me.LblPeriod2.AutoSize = True
        Me.LblPeriod2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPeriod2.Location = New System.Drawing.Point(158, 44)
        Me.LblPeriod2.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.LblPeriod2.Name = "LblPeriod2"
        Me.LblPeriod2.Size = New System.Drawing.Size(11, 13)
        Me.LblPeriod2.TabIndex = 6
        Me.LblPeriod2.Text = "."
        '
        'LblPeriod3
        '
        Me.LblPeriod3.AutoSize = True
        Me.LblPeriod3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPeriod3.Location = New System.Drawing.Point(202, 44)
        Me.LblPeriod3.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.LblPeriod3.Name = "LblPeriod3"
        Me.LblPeriod3.Size = New System.Drawing.Size(11, 13)
        Me.LblPeriod3.TabIndex = 8
        Me.LblPeriod3.Text = "."
        '
        'LblName
        '
        Me.LblName.AutoSize = True
        Me.LblName.Location = New System.Drawing.Point(21, 18)
        Me.LblName.Name = "LblName"
        Me.LblName.Size = New System.Drawing.Size(35, 13)
        Me.LblName.TabIndex = 0
        Me.LblName.Text = "Name"
        '
        'TxtName
        '
        Me.TxtName.Location = New System.Drawing.Point(85, 15)
        Me.TxtName.Name = "TxtName"
        Me.TxtName.Size = New System.Drawing.Size(157, 20)
        Me.TxtName.TabIndex = 1
        '
        'chkTertiaryController
        '
        Me.chkTertiaryController.AutoSize = True
        Me.chkTertiaryController.Location = New System.Drawing.Point(85, 119)
        Me.chkTertiaryController.Name = "chkTertiaryController"
        Me.chkTertiaryController.Size = New System.Drawing.Size(108, 17)
        Me.chkTertiaryController.TabIndex = 16
        Me.chkTertiaryController.Text = "Tertiary Controller"
        Me.chkTertiaryController.UseVisualStyleBackColor = True
        '
        'FrmConnection
        '
        Me.AcceptButton = Me.BtnSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BtnCancel
        Me.ClientSize = New System.Drawing.Size(262, 191)
        Me.Controls.Add(Me.chkTertiaryController)
        Me.Controls.Add(Me.TxtName)
        Me.Controls.Add(Me.LblName)
        Me.Controls.Add(Me.LblPeriod3)
        Me.Controls.Add(Me.LblPeriod2)
        Me.Controls.Add(Me.LblPeriod1)
        Me.Controls.Add(Me.TxtIP4)
        Me.Controls.Add(Me.TxtIP3)
        Me.Controls.Add(Me.TxtIP2)
        Me.Controls.Add(Me.TxtIP1)
        Me.Controls.Add(Me.TxtPort)
        Me.Controls.Add(Me.LblPort)
        Me.Controls.Add(Me.TxtDeviceID)
        Me.Controls.Add(Me.LblDeviceID)
        Me.Controls.Add(Me.LblIP)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnSave)
        Me.Name = "FrmConnection"
        Me.Text = "Add/Edit Connection"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnSave As Button
    Friend WithEvents BtnCancel As Button
    Friend WithEvents LblIP As Label
    Friend WithEvents LblDeviceID As Label
    Friend WithEvents TxtDeviceID As TextBox
    Friend WithEvents TxtPort As TextBox
    Friend WithEvents LblPort As Label
    Friend WithEvents TxtIP1 As MaskedTextBox
    Friend WithEvents TxtIP2 As MaskedTextBox
    Friend WithEvents TxtIP3 As MaskedTextBox
    Friend WithEvents TxtIP4 As MaskedTextBox
    Friend WithEvents LblPeriod1 As Label
    Friend WithEvents LblPeriod2 As Label
    Friend WithEvents LblPeriod3 As Label
    Friend WithEvents LblName As Label
    Friend WithEvents TxtName As TextBox
    Friend WithEvents chkTertiaryController As CheckBox
End Class
