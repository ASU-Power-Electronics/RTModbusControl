Imports System.ComponentModel
Imports Microsoft.VisualBasic.CompilerServices

<DesignerGenerated()>
Partial Class FrmConnections
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
        Me.LstConnections = New ListView()
        Me.ClmID = CType(New ColumnHeader(), ColumnHeader)
        Me.ClmName = CType(New ColumnHeader(), ColumnHeader)
        Me.ClmIP = CType(New ColumnHeader(), ColumnHeader)
        Me.ClmPort = CType(New ColumnHeader(), ColumnHeader)
        Me.BtnNew = New Button()
        Me.BtnEdit = New Button()
        Me.BtnRemove = New Button()
        Me.BtnTest = New Button()
        Me.SuspendLayout()
        '
        'LstConnections
        '
        Me.LstConnections.Columns.AddRange(New ColumnHeader() {Me.ClmID, Me.ClmName, Me.ClmIP, Me.ClmPort})
        Me.LstConnections.FullRowSelect = True
        Me.LstConnections.Location = New Point(12, 12)
        Me.LstConnections.MultiSelect = False
        Me.LstConnections.Name = "LstConnections"
        Me.LstConnections.Size = New Size(300, 230)
        Me.LstConnections.TabIndex = 0
        Me.LstConnections.UseCompatibleStateImageBehavior = False
        Me.LstConnections.View = View.Details
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
        'BtnNew
        '
        Me.BtnNew.Location = New Point(318, 12)
        Me.BtnNew.Name = "BtnNew"
        Me.BtnNew.Size = New Size(86, 53)
        Me.BtnNew.TabIndex = 1
        Me.BtnNew.Text = "&New Connection"
        Me.BtnNew.UseVisualStyleBackColor = True
        '
        'BtnEdit
        '
        Me.BtnEdit.Enabled = False
        Me.BtnEdit.Location = New Point(318, 71)
        Me.BtnEdit.Name = "BtnEdit"
        Me.BtnEdit.Size = New Size(86, 53)
        Me.BtnEdit.TabIndex = 2
        Me.BtnEdit.Text = "&Edit Connection"
        Me.BtnEdit.UseVisualStyleBackColor = True
        '
        'BtnRemove
        '
        Me.BtnRemove.Enabled = False
        Me.BtnRemove.Location = New Point(318, 130)
        Me.BtnRemove.Name = "BtnRemove"
        Me.BtnRemove.Size = New Size(86, 53)
        Me.BtnRemove.TabIndex = 3
        Me.BtnRemove.Text = "&Remove Connection"
        Me.BtnRemove.UseVisualStyleBackColor = True
        '
        'BtnTest
        '
        Me.BtnTest.Enabled = False
        Me.BtnTest.Location = New Point(318, 189)
        Me.BtnTest.Name = "BtnTest"
        Me.BtnTest.Size = New Size(86, 53)
        Me.BtnTest.TabIndex = 4
        Me.BtnTest.Text = "&Test Connection"
        Me.BtnTest.UseVisualStyleBackColor = True
        '
        'FrmConnections
        '
        Me.AutoScaleDimensions = New SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.ClientSize = New Size(418, 258)
        Me.Controls.Add(Me.BtnTest)
        Me.Controls.Add(Me.BtnRemove)
        Me.Controls.Add(Me.BtnEdit)
        Me.Controls.Add(Me.BtnNew)
        Me.Controls.Add(Me.LstConnections)
        Me.Name = "FrmConnections"
        Me.Text = "Modbus Connections"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LstConnections As ListView
    Friend WithEvents ClmID As ColumnHeader
    Friend WithEvents ClmIP As ColumnHeader
    Friend WithEvents ClmPort As ColumnHeader
    Friend WithEvents BtnNew As Button
    Friend WithEvents BtnEdit As Button
    Friend WithEvents ClmName As ColumnHeader
    Friend WithEvents BtnRemove As Button
    Friend WithEvents BtnTest As Button
End Class
