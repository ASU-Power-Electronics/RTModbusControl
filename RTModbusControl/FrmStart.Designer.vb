Imports System.ComponentModel
Imports Microsoft.VisualBasic.CompilerServices

<DesignerGenerated()>
Partial Class FrmStart
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmStart))
        Me.BtnConnections = New System.Windows.Forms.Button()
        Me.BtnControls = New System.Windows.Forms.Button()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TxtWelcome = New System.Windows.Forms.TextBox()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnConnections
        '
        Me.BtnConnections.Location = New System.Drawing.Point(276, 27)
        Me.BtnConnections.Name = "BtnConnections"
        Me.BtnConnections.Size = New System.Drawing.Size(94, 47)
        Me.BtnConnections.TabIndex = 0
        Me.BtnConnections.Text = "Connections"
        Me.BtnConnections.UseVisualStyleBackColor = True
        '
        'BtnControls
        '
        Me.BtnControls.Location = New System.Drawing.Point(276, 100)
        Me.BtnControls.Name = "BtnControls"
        Me.BtnControls.Size = New System.Drawing.Size(94, 47)
        Me.BtnControls.TabIndex = 1
        Me.BtnControls.Text = "Controls"
        Me.BtnControls.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(382, 24)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.TabStop = True
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'TxtWelcome
        '
        Me.TxtWelcome.Location = New System.Drawing.Point(12, 27)
        Me.TxtWelcome.Multiline = True
        Me.TxtWelcome.Name = "TxtWelcome"
        Me.TxtWelcome.ReadOnly = True
        Me.TxtWelcome.Size = New System.Drawing.Size(258, 145)
        Me.TxtWelcome.TabIndex = 3
        Me.TxtWelcome.TabStop = False
        Me.TxtWelcome.Text = resources.GetString("TxtWelcome.Text")
        '
        'FrmStart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(382, 184)
        Me.Controls.Add(Me.TxtWelcome)
        Me.Controls.Add(Me.BtnControls)
        Me.Controls.Add(Me.BtnConnections)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "FrmStart"
        Me.Text = "RT Modbus Control"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnConnections As Button
    Friend WithEvents BtnControls As Button
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TxtWelcome As TextBox
End Class
