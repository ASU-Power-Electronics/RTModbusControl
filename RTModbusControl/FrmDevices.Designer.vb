<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDevices
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.BtnRemove = New System.Windows.Forms.Button()
        Me.BtnEdit = New System.Windows.Forms.Button()
        Me.BtnNew = New System.Windows.Forms.Button()
        Me.LstConnections = New System.Windows.Forms.ListView()
        Me.ClmID = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.ClmName = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.ClmIP = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.ClmPort = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout
        '
        'BtnRemove
        '
        Me.BtnRemove.Enabled = false
        Me.BtnRemove.Location = New System.Drawing.Point(318, 130)
        Me.BtnRemove.Name = "BtnRemove"
        Me.BtnRemove.Size = New System.Drawing.Size(86, 53)
        Me.BtnRemove.TabIndex = 8
        Me.BtnRemove.Text = "&Remove Device"
        Me.BtnRemove.UseVisualStyleBackColor = true
        '
        'BtnEdit
        '
        Me.BtnEdit.Enabled = false
        Me.BtnEdit.Location = New System.Drawing.Point(318, 71)
        Me.BtnEdit.Name = "BtnEdit"
        Me.BtnEdit.Size = New System.Drawing.Size(86, 53)
        Me.BtnEdit.TabIndex = 7
        Me.BtnEdit.Text = "&Edit Device"
        Me.BtnEdit.UseVisualStyleBackColor = true
        '
        'BtnNew
        '
        Me.BtnNew.Location = New System.Drawing.Point(318, 12)
        Me.BtnNew.Name = "BtnNew"
        Me.BtnNew.Size = New System.Drawing.Size(86, 53)
        Me.BtnNew.TabIndex = 6
        Me.BtnNew.Text = "&New Device"
        Me.BtnNew.UseVisualStyleBackColor = true
        '
        'LstConnections
        '
        Me.LstConnections.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ClmID, Me.ClmName, Me.ClmIP, Me.ClmPort})
        Me.LstConnections.FullRowSelect = true
        Me.LstConnections.Location = New System.Drawing.Point(12, 12)
        Me.LstConnections.MultiSelect = false
        Me.LstConnections.Name = "LstConnections"
        Me.LstConnections.Size = New System.Drawing.Size(300, 171)
        Me.LstConnections.TabIndex = 5
        Me.LstConnections.UseCompatibleStateImageBehavior = false
        Me.LstConnections.View = System.Windows.Forms.View.Details
        '
        'ClmID
        '
        Me.ClmID.Text = "ID"
        Me.ClmID.Width = 33
        '
        'ClmName
        '
        Me.ClmName.Text = "Name"
        Me.ClmName.Width = 106
        '
        'ClmIP
        '
        Me.ClmIP.Text = "IP Address"
        Me.ClmIP.Width = 98
        '
        'ClmPort
        '
        Me.ClmPort.Text = "Port"
        Me.ClmPort.Width = 59
        '
        'FrmDevices
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(416, 195)
        Me.Controls.Add(Me.BtnRemove)
        Me.Controls.Add(Me.BtnEdit)
        Me.Controls.Add(Me.BtnNew)
        Me.Controls.Add(Me.LstConnections)
        Me.Name = "FrmDevices"
        Me.Text = "FrmDevices"
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents BtnRemove As Button
    Friend WithEvents BtnEdit As Button
    Friend WithEvents BtnNew As Button
    Friend WithEvents LstConnections As ListView
    Friend WithEvents ClmID As ColumnHeader
    Friend WithEvents ClmName As ColumnHeader
    Friend WithEvents ClmIP As ColumnHeader
    Friend WithEvents ClmPort As ColumnHeader
End Class
