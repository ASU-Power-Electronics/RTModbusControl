Imports System.ComponentModel
Imports Microsoft.VisualBasic.CompilerServices

<DesignerGenerated()>
Partial Class FrmControls
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
        Me.components = New System.ComponentModel.Container()
        Me.LblRealPower = New System.Windows.Forms.Label()
        Me.TxtRealPower = New System.Windows.Forms.TextBox()
        Me.GrpDeviceList = New System.Windows.Forms.GroupBox()
        Me.LstDevices = New System.Windows.Forms.ListView()
        Me.ClmDevice = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.ClmConnection = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.ClmStatus = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.GrpMeasurements = New System.Windows.Forms.GroupBox()
        Me.LblEnergyStorageUnit = New System.Windows.Forms.Label()
        Me.LblPVGenerationUnit = New System.Windows.Forms.Label()
        Me.LblDeviceFrequencyUnit = New System.Windows.Forms.Label()
        Me.LblCurrentAngleUnit = New System.Windows.Forms.Label()
        Me.LblCurrentMagnitudeUnit = New System.Windows.Forms.Label()
        Me.LblVoltageAngleUnit = New System.Windows.Forms.Label()
        Me.LblVoltageMagnitudeUnit = New System.Windows.Forms.Label()
        Me.LblReactivePowerUnit = New System.Windows.Forms.Label()
        Me.LblRealPowerUnit = New System.Windows.Forms.Label()
        Me.TxtEnergyStorage = New System.Windows.Forms.TextBox()
        Me.TxtPvGeneration = New System.Windows.Forms.TextBox()
        Me.TxtDeviceFrequency = New System.Windows.Forms.TextBox()
        Me.TxtCurrentAngleA = New System.Windows.Forms.TextBox()
        Me.TxtCurrentMagnitudeA = New System.Windows.Forms.TextBox()
        Me.TxtVoltageAngleA = New System.Windows.Forms.TextBox()
        Me.LblEnergyStorage = New System.Windows.Forms.Label()
        Me.LblPVGeneration = New System.Windows.Forms.Label()
        Me.LblDeviceFrequency = New System.Windows.Forms.Label()
        Me.LblCurrentAngle = New System.Windows.Forms.Label()
        Me.TxtVoltageMagnitudeA = New System.Windows.Forms.TextBox()
        Me.TxtReactivePower = New System.Windows.Forms.TextBox()
        Me.LblCurrentMagnitude = New System.Windows.Forms.Label()
        Me.LblVoltageAngle = New System.Windows.Forms.Label()
        Me.LblVoltageMagnitude = New System.Windows.Forms.Label()
        Me.LblReactivePower = New System.Windows.Forms.Label()
        Me.BtnModeSwitch = New System.Windows.Forms.Button()
        Me.BtnApply = New System.Windows.Forms.Button()
        Me.GrpControl = New System.Windows.Forms.GroupBox()
        Me.LblAverageEnergyUnit = New System.Windows.Forms.Label()
        Me.TxtAverageEnergy = New System.Windows.Forms.TextBox()
        Me.LblAverageEnergy = New System.Windows.Forms.Label()
        Me.LblDeltaEUnit = New System.Windows.Forms.Label()
        Me.LblDeltaOmegaUnit = New System.Windows.Forms.Label()
        Me.LblReactivePowerSetpointUnit = New System.Windows.Forms.Label()
        Me.LblRealPowerSetpointUnit = New System.Windows.Forms.Label()
        Me.TxtDeltaE = New System.Windows.Forms.TextBox()
        Me.TxtDeltaOmega = New System.Windows.Forms.TextBox()
        Me.TxtReactivePowerSetpoint = New System.Windows.Forms.TextBox()
        Me.TxtRealPowerSetpoint = New System.Windows.Forms.TextBox()
        Me.LblDeltaE = New System.Windows.Forms.Label()
        Me.LblDeltaOmega = New System.Windows.Forms.Label()
        Me.LblReactivePowerSetpoint = New System.Windows.Forms.Label()
        Me.LblRealPowerSetpoint = New System.Windows.Forms.Label()
        Me.menContextDevices = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.menManageDevices = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.GrpDeviceList.SuspendLayout
        Me.GrpMeasurements.SuspendLayout
        Me.GrpControl.SuspendLayout
        Me.menContextDevices.SuspendLayout
        Me.SuspendLayout
        '
        'LblRealPower
        '
        Me.LblRealPower.AutoSize = true
        Me.LblRealPower.Location = New System.Drawing.Point(6, 16)
        Me.LblRealPower.Name = "LblRealPower"
        Me.LblRealPower.Size = New System.Drawing.Size(78, 13)
        Me.LblRealPower.TabIndex = 3
        Me.LblRealPower.Text = "Real Power (P)"
        '
        'TxtRealPower
        '
        Me.TxtRealPower.Location = New System.Drawing.Point(183, 13)
        Me.TxtRealPower.Name = "TxtRealPower"
        Me.TxtRealPower.ReadOnly = true
        Me.TxtRealPower.Size = New System.Drawing.Size(53, 20)
        Me.TxtRealPower.TabIndex = 4
        '
        'GrpDeviceList
        '
        Me.GrpDeviceList.Controls.Add(Me.LstDevices)
        Me.GrpDeviceList.Location = New System.Drawing.Point(12, 12)
        Me.GrpDeviceList.Name = "GrpDeviceList"
        Me.GrpDeviceList.Size = New System.Drawing.Size(338, 360)
        Me.GrpDeviceList.TabIndex = 0
        Me.GrpDeviceList.TabStop = false
        Me.GrpDeviceList.Text = "Device List"
        '
        'LstDevices
        '
        Me.LstDevices.CheckBoxes = true
        Me.LstDevices.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ClmDevice, Me.ClmConnection, Me.ClmStatus})
        Me.LstDevices.ContextMenuStrip = Me.menContextDevices
        Me.LstDevices.FullRowSelect = true
        Me.LstDevices.HideSelection = false
        Me.LstDevices.Location = New System.Drawing.Point(6, 19)
        Me.LstDevices.MultiSelect = false
        Me.LstDevices.Name = "LstDevices"
        Me.LstDevices.Size = New System.Drawing.Size(326, 335)
        Me.LstDevices.TabIndex = 1
        Me.LstDevices.UseCompatibleStateImageBehavior = false
        Me.LstDevices.View = System.Windows.Forms.View.Details
        '
        'ClmDevice
        '
        Me.ClmDevice.Text = "Device"
        Me.ClmDevice.Width = 87
        '
        'ClmConnection
        '
        Me.ClmConnection.Text = "Connection"
        Me.ClmConnection.Width = 85
        '
        'ClmStatus
        '
        Me.ClmStatus.Text = "Status"
        Me.ClmStatus.Width = 85
        '
        'GrpMeasurements
        '
        Me.GrpMeasurements.Controls.Add(Me.LblEnergyStorageUnit)
        Me.GrpMeasurements.Controls.Add(Me.LblPVGenerationUnit)
        Me.GrpMeasurements.Controls.Add(Me.LblDeviceFrequencyUnit)
        Me.GrpMeasurements.Controls.Add(Me.LblCurrentAngleUnit)
        Me.GrpMeasurements.Controls.Add(Me.LblCurrentMagnitudeUnit)
        Me.GrpMeasurements.Controls.Add(Me.LblVoltageAngleUnit)
        Me.GrpMeasurements.Controls.Add(Me.LblVoltageMagnitudeUnit)
        Me.GrpMeasurements.Controls.Add(Me.LblReactivePowerUnit)
        Me.GrpMeasurements.Controls.Add(Me.LblRealPowerUnit)
        Me.GrpMeasurements.Controls.Add(Me.TxtEnergyStorage)
        Me.GrpMeasurements.Controls.Add(Me.TxtPvGeneration)
        Me.GrpMeasurements.Controls.Add(Me.TxtDeviceFrequency)
        Me.GrpMeasurements.Controls.Add(Me.TxtCurrentAngleA)
        Me.GrpMeasurements.Controls.Add(Me.TxtCurrentMagnitudeA)
        Me.GrpMeasurements.Controls.Add(Me.TxtVoltageAngleA)
        Me.GrpMeasurements.Controls.Add(Me.LblEnergyStorage)
        Me.GrpMeasurements.Controls.Add(Me.LblPVGeneration)
        Me.GrpMeasurements.Controls.Add(Me.LblDeviceFrequency)
        Me.GrpMeasurements.Controls.Add(Me.LblCurrentAngle)
        Me.GrpMeasurements.Controls.Add(Me.TxtVoltageMagnitudeA)
        Me.GrpMeasurements.Controls.Add(Me.TxtReactivePower)
        Me.GrpMeasurements.Controls.Add(Me.LblCurrentMagnitude)
        Me.GrpMeasurements.Controls.Add(Me.LblVoltageAngle)
        Me.GrpMeasurements.Controls.Add(Me.LblVoltageMagnitude)
        Me.GrpMeasurements.Controls.Add(Me.LblReactivePower)
        Me.GrpMeasurements.Controls.Add(Me.TxtRealPower)
        Me.GrpMeasurements.Controls.Add(Me.LblRealPower)
        Me.GrpMeasurements.Location = New System.Drawing.Point(356, 12)
        Me.GrpMeasurements.Name = "GrpMeasurements"
        Me.GrpMeasurements.Size = New System.Drawing.Size(289, 252)
        Me.GrpMeasurements.TabIndex = 2
        Me.GrpMeasurements.TabStop = false
        Me.GrpMeasurements.Text = "Device Measurements"
        '
        'LblEnergyStorageUnit
        '
        Me.LblEnergyStorageUnit.AutoSize = true
        Me.LblEnergyStorageUnit.Location = New System.Drawing.Point(242, 224)
        Me.LblEnergyStorageUnit.Name = "LblEnergyStorageUnit"
        Me.LblEnergyStorageUnit.Size = New System.Drawing.Size(21, 13)
        Me.LblEnergyStorageUnit.TabIndex = 37
        Me.LblEnergyStorageUnit.Text = "MJ"
        '
        'LblPVGenerationUnit
        '
        Me.LblPVGenerationUnit.AutoSize = true
        Me.LblPVGenerationUnit.Location = New System.Drawing.Point(242, 198)
        Me.LblPVGenerationUnit.Name = "LblPVGenerationUnit"
        Me.LblPVGenerationUnit.Size = New System.Drawing.Size(18, 13)
        Me.LblPVGenerationUnit.TabIndex = 36
        Me.LblPVGenerationUnit.Text = "W"
        '
        'LblDeviceFrequencyUnit
        '
        Me.LblDeviceFrequencyUnit.AutoSize = true
        Me.LblDeviceFrequencyUnit.Location = New System.Drawing.Point(242, 172)
        Me.LblDeviceFrequencyUnit.Name = "LblDeviceFrequencyUnit"
        Me.LblDeviceFrequencyUnit.Size = New System.Drawing.Size(32, 13)
        Me.LblDeviceFrequencyUnit.TabIndex = 35
        Me.LblDeviceFrequencyUnit.Text = "rad/s"
        '
        'LblCurrentAngleUnit
        '
        Me.LblCurrentAngleUnit.AutoSize = true
        Me.LblCurrentAngleUnit.Location = New System.Drawing.Point(242, 146)
        Me.LblCurrentAngleUnit.Name = "LblCurrentAngleUnit"
        Me.LblCurrentAngleUnit.Size = New System.Drawing.Size(11, 13)
        Me.LblCurrentAngleUnit.TabIndex = 34
        Me.LblCurrentAngleUnit.Text = "°"
        '
        'LblCurrentMagnitudeUnit
        '
        Me.LblCurrentMagnitudeUnit.AutoSize = true
        Me.LblCurrentMagnitudeUnit.Location = New System.Drawing.Point(242, 120)
        Me.LblCurrentMagnitudeUnit.Name = "LblCurrentMagnitudeUnit"
        Me.LblCurrentMagnitudeUnit.Size = New System.Drawing.Size(14, 13)
        Me.LblCurrentMagnitudeUnit.TabIndex = 33
        Me.LblCurrentMagnitudeUnit.Text = "A"
        '
        'LblVoltageAngleUnit
        '
        Me.LblVoltageAngleUnit.AutoSize = true
        Me.LblVoltageAngleUnit.Location = New System.Drawing.Point(242, 94)
        Me.LblVoltageAngleUnit.Name = "LblVoltageAngleUnit"
        Me.LblVoltageAngleUnit.Size = New System.Drawing.Size(11, 13)
        Me.LblVoltageAngleUnit.TabIndex = 32
        Me.LblVoltageAngleUnit.Text = "°"
        '
        'LblVoltageMagnitudeUnit
        '
        Me.LblVoltageMagnitudeUnit.AutoSize = true
        Me.LblVoltageMagnitudeUnit.Location = New System.Drawing.Point(242, 68)
        Me.LblVoltageMagnitudeUnit.Name = "LblVoltageMagnitudeUnit"
        Me.LblVoltageMagnitudeUnit.Size = New System.Drawing.Size(14, 13)
        Me.LblVoltageMagnitudeUnit.TabIndex = 31
        Me.LblVoltageMagnitudeUnit.Text = "V"
        '
        'LblReactivePowerUnit
        '
        Me.LblReactivePowerUnit.AutoSize = true
        Me.LblReactivePowerUnit.Location = New System.Drawing.Point(242, 42)
        Me.LblReactivePowerUnit.Name = "LblReactivePowerUnit"
        Me.LblReactivePowerUnit.Size = New System.Drawing.Size(24, 13)
        Me.LblReactivePowerUnit.TabIndex = 30
        Me.LblReactivePowerUnit.Text = "VAr"
        '
        'LblRealPowerUnit
        '
        Me.LblRealPowerUnit.AutoSize = true
        Me.LblRealPowerUnit.Location = New System.Drawing.Point(242, 16)
        Me.LblRealPowerUnit.Name = "LblRealPowerUnit"
        Me.LblRealPowerUnit.Size = New System.Drawing.Size(18, 13)
        Me.LblRealPowerUnit.TabIndex = 29
        Me.LblRealPowerUnit.Text = "W"
        '
        'TxtEnergyStorage
        '
        Me.TxtEnergyStorage.Location = New System.Drawing.Point(183, 221)
        Me.TxtEnergyStorage.Name = "TxtEnergyStorage"
        Me.TxtEnergyStorage.ReadOnly = true
        Me.TxtEnergyStorage.Size = New System.Drawing.Size(53, 20)
        Me.TxtEnergyStorage.TabIndex = 28
        '
        'TxtPvGeneration
        '
        Me.TxtPvGeneration.Location = New System.Drawing.Point(183, 195)
        Me.TxtPvGeneration.Name = "TxtPvGeneration"
        Me.TxtPvGeneration.ReadOnly = true
        Me.TxtPvGeneration.Size = New System.Drawing.Size(53, 20)
        Me.TxtPvGeneration.TabIndex = 26
        '
        'TxtDeviceFrequency
        '
        Me.TxtDeviceFrequency.Location = New System.Drawing.Point(183, 169)
        Me.TxtDeviceFrequency.Name = "TxtDeviceFrequency"
        Me.TxtDeviceFrequency.ReadOnly = true
        Me.TxtDeviceFrequency.Size = New System.Drawing.Size(53, 20)
        Me.TxtDeviceFrequency.TabIndex = 24
        '
        'TxtCurrentAngleA
        '
        Me.TxtCurrentAngleA.Location = New System.Drawing.Point(183, 143)
        Me.TxtCurrentAngleA.Name = "TxtCurrentAngleA"
        Me.TxtCurrentAngleA.ReadOnly = true
        Me.TxtCurrentAngleA.Size = New System.Drawing.Size(53, 20)
        Me.TxtCurrentAngleA.TabIndex = 21
        '
        'TxtCurrentMagnitudeA
        '
        Me.TxtCurrentMagnitudeA.Location = New System.Drawing.Point(183, 117)
        Me.TxtCurrentMagnitudeA.Name = "TxtCurrentMagnitudeA"
        Me.TxtCurrentMagnitudeA.ReadOnly = true
        Me.TxtCurrentMagnitudeA.Size = New System.Drawing.Size(53, 20)
        Me.TxtCurrentMagnitudeA.TabIndex = 17
        '
        'TxtVoltageAngleA
        '
        Me.TxtVoltageAngleA.Location = New System.Drawing.Point(183, 91)
        Me.TxtVoltageAngleA.Name = "TxtVoltageAngleA"
        Me.TxtVoltageAngleA.ReadOnly = true
        Me.TxtVoltageAngleA.Size = New System.Drawing.Size(53, 20)
        Me.TxtVoltageAngleA.TabIndex = 13
        '
        'LblEnergyStorage
        '
        Me.LblEnergyStorage.AutoSize = true
        Me.LblEnergyStorage.Location = New System.Drawing.Point(6, 224)
        Me.LblEnergyStorage.Name = "LblEnergyStorage"
        Me.LblEnergyStorage.Size = New System.Drawing.Size(107, 13)
        Me.LblEnergyStorage.TabIndex = 27
        Me.LblEnergyStorage.Text = "Energy Storage (E_s)"
        '
        'LblPVGeneration
        '
        Me.LblPVGeneration.AutoSize = true
        Me.LblPVGeneration.Location = New System.Drawing.Point(6, 198)
        Me.LblPVGeneration.Name = "LblPVGeneration"
        Me.LblPVGeneration.Size = New System.Drawing.Size(112, 13)
        Me.LblPVGeneration.TabIndex = 25
        Me.LblPVGeneration.Text = "PV Generation (P_PV)"
        '
        'LblDeviceFrequency
        '
        Me.LblDeviceFrequency.AutoSize = true
        Me.LblDeviceFrequency.Location = New System.Drawing.Point(6, 172)
        Me.LblDeviceFrequency.Name = "LblDeviceFrequency"
        Me.LblDeviceFrequency.Size = New System.Drawing.Size(111, 13)
        Me.LblDeviceFrequency.TabIndex = 23
        Me.LblDeviceFrequency.Text = "Device Frequency (ω)"
        '
        'LblCurrentAngle
        '
        Me.LblCurrentAngle.AutoSize = true
        Me.LblCurrentAngle.Location = New System.Drawing.Point(6, 146)
        Me.LblCurrentAngle.Name = "LblCurrentAngle"
        Me.LblCurrentAngle.Size = New System.Drawing.Size(95, 13)
        Me.LblCurrentAngle.TabIndex = 19
        Me.LblCurrentAngle.Text = "Current Angle (θ_I)"
        '
        'TxtVoltageMagnitudeA
        '
        Me.TxtVoltageMagnitudeA.Location = New System.Drawing.Point(183, 65)
        Me.TxtVoltageMagnitudeA.Name = "TxtVoltageMagnitudeA"
        Me.TxtVoltageMagnitudeA.ReadOnly = true
        Me.TxtVoltageMagnitudeA.Size = New System.Drawing.Size(53, 20)
        Me.TxtVoltageMagnitudeA.TabIndex = 9
        '
        'TxtReactivePower
        '
        Me.TxtReactivePower.Location = New System.Drawing.Point(183, 39)
        Me.TxtReactivePower.Name = "TxtReactivePower"
        Me.TxtReactivePower.ReadOnly = true
        Me.TxtReactivePower.Size = New System.Drawing.Size(53, 20)
        Me.TxtReactivePower.TabIndex = 6
        '
        'LblCurrentMagnitude
        '
        Me.LblCurrentMagnitude.AutoSize = true
        Me.LblCurrentMagnitude.Location = New System.Drawing.Point(6, 120)
        Me.LblCurrentMagnitude.Name = "LblCurrentMagnitude"
        Me.LblCurrentMagnitude.Size = New System.Drawing.Size(106, 13)
        Me.LblCurrentMagnitude.TabIndex = 15
        Me.LblCurrentMagnitude.Text = "Current Magnitude (I)"
        '
        'LblVoltageAngle
        '
        Me.LblVoltageAngle.AutoSize = true
        Me.LblVoltageAngle.Location = New System.Drawing.Point(6, 94)
        Me.LblVoltageAngle.Name = "LblVoltageAngle"
        Me.LblVoltageAngle.Size = New System.Drawing.Size(101, 13)
        Me.LblVoltageAngle.TabIndex = 11
        Me.LblVoltageAngle.Text = "Voltage Angle (θ_V)"
        '
        'LblVoltageMagnitude
        '
        Me.LblVoltageMagnitude.AutoSize = true
        Me.LblVoltageMagnitude.Location = New System.Drawing.Point(6, 68)
        Me.LblVoltageMagnitude.Name = "LblVoltageMagnitude"
        Me.LblVoltageMagnitude.Size = New System.Drawing.Size(112, 13)
        Me.LblVoltageMagnitude.TabIndex = 7
        Me.LblVoltageMagnitude.Text = "Voltage Magnitude (V)"
        '
        'LblReactivePower
        '
        Me.LblReactivePower.AutoSize = true
        Me.LblReactivePower.Location = New System.Drawing.Point(6, 42)
        Me.LblReactivePower.Name = "LblReactivePower"
        Me.LblReactivePower.Size = New System.Drawing.Size(100, 13)
        Me.LblReactivePower.TabIndex = 5
        Me.LblReactivePower.Text = "Reactive Power (Q)"
        '
        'BtnModeSwitch
        '
        Me.BtnModeSwitch.Enabled = false
        Me.BtnModeSwitch.Location = New System.Drawing.Point(59, 378)
        Me.BtnModeSwitch.Name = "BtnModeSwitch"
        Me.BtnModeSwitch.Size = New System.Drawing.Size(75, 46)
        Me.BtnModeSwitch.TabIndex = 41
        Me.BtnModeSwitch.Text = "Manual Controls"
        Me.BtnModeSwitch.UseVisualStyleBackColor = true
        Me.BtnModeSwitch.Visible = false
        '
        'BtnApply
        '
        Me.BtnApply.Enabled = false
        Me.BtnApply.Location = New System.Drawing.Point(229, 378)
        Me.BtnApply.Name = "BtnApply"
        Me.BtnApply.Size = New System.Drawing.Size(75, 46)
        Me.BtnApply.TabIndex = 40
        Me.BtnApply.Text = "Apply"
        Me.BtnApply.UseVisualStyleBackColor = true
        Me.BtnApply.Visible = false
        '
        'GrpControl
        '
        Me.GrpControl.Controls.Add(Me.LblAverageEnergyUnit)
        Me.GrpControl.Controls.Add(Me.TxtAverageEnergy)
        Me.GrpControl.Controls.Add(Me.LblAverageEnergy)
        Me.GrpControl.Controls.Add(Me.LblDeltaEUnit)
        Me.GrpControl.Controls.Add(Me.LblDeltaOmegaUnit)
        Me.GrpControl.Controls.Add(Me.LblReactivePowerSetpointUnit)
        Me.GrpControl.Controls.Add(Me.LblRealPowerSetpointUnit)
        Me.GrpControl.Controls.Add(Me.TxtDeltaE)
        Me.GrpControl.Controls.Add(Me.TxtDeltaOmega)
        Me.GrpControl.Controls.Add(Me.TxtReactivePowerSetpoint)
        Me.GrpControl.Controls.Add(Me.TxtRealPowerSetpoint)
        Me.GrpControl.Controls.Add(Me.LblDeltaE)
        Me.GrpControl.Controls.Add(Me.LblDeltaOmega)
        Me.GrpControl.Controls.Add(Me.LblReactivePowerSetpoint)
        Me.GrpControl.Controls.Add(Me.LblRealPowerSetpoint)
        Me.GrpControl.Location = New System.Drawing.Point(356, 270)
        Me.GrpControl.Name = "GrpControl"
        Me.GrpControl.Size = New System.Drawing.Size(289, 154)
        Me.GrpControl.TabIndex = 29
        Me.GrpControl.TabStop = false
        Me.GrpControl.Text = "Energy Cell Control"
        '
        'LblAverageEnergyUnit
        '
        Me.LblAverageEnergyUnit.AutoSize = true
        Me.LblAverageEnergyUnit.Location = New System.Drawing.Point(242, 125)
        Me.LblAverageEnergyUnit.Name = "LblAverageEnergyUnit"
        Me.LblAverageEnergyUnit.Size = New System.Drawing.Size(21, 13)
        Me.LblAverageEnergyUnit.TabIndex = 46
        Me.LblAverageEnergyUnit.Text = "MJ"
        '
        'TxtAverageEnergy
        '
        Me.TxtAverageEnergy.Location = New System.Drawing.Point(183, 123)
        Me.TxtAverageEnergy.Name = "TxtAverageEnergy"
        Me.TxtAverageEnergy.ReadOnly = true
        Me.TxtAverageEnergy.Size = New System.Drawing.Size(53, 20)
        Me.TxtAverageEnergy.TabIndex = 45
        '
        'LblAverageEnergy
        '
        Me.LblAverageEnergy.AutoSize = true
        Me.LblAverageEnergy.Location = New System.Drawing.Point(6, 126)
        Me.LblAverageEnergy.Name = "LblAverageEnergy"
        Me.LblAverageEnergy.Size = New System.Drawing.Size(157, 13)
        Me.LblAverageEnergy.TabIndex = 44
        Me.LblAverageEnergy.Text = "Average Stored Energy (E_avg)"
        '
        'LblDeltaEUnit
        '
        Me.LblDeltaEUnit.AutoSize = true
        Me.LblDeltaEUnit.Location = New System.Drawing.Point(242, 100)
        Me.LblDeltaEUnit.Name = "LblDeltaEUnit"
        Me.LblDeltaEUnit.Size = New System.Drawing.Size(14, 13)
        Me.LblDeltaEUnit.TabIndex = 43
        Me.LblDeltaEUnit.Text = "V"
        '
        'LblDeltaOmegaUnit
        '
        Me.LblDeltaOmegaUnit.AutoSize = true
        Me.LblDeltaOmegaUnit.Location = New System.Drawing.Point(242, 74)
        Me.LblDeltaOmegaUnit.Name = "LblDeltaOmegaUnit"
        Me.LblDeltaOmegaUnit.Size = New System.Drawing.Size(32, 13)
        Me.LblDeltaOmegaUnit.TabIndex = 42
        Me.LblDeltaOmegaUnit.Text = "rad/s"
        '
        'LblReactivePowerSetpointUnit
        '
        Me.LblReactivePowerSetpointUnit.AutoSize = true
        Me.LblReactivePowerSetpointUnit.Location = New System.Drawing.Point(242, 48)
        Me.LblReactivePowerSetpointUnit.Name = "LblReactivePowerSetpointUnit"
        Me.LblReactivePowerSetpointUnit.Size = New System.Drawing.Size(24, 13)
        Me.LblReactivePowerSetpointUnit.TabIndex = 40
        Me.LblReactivePowerSetpointUnit.Text = "VAr"
        '
        'LblRealPowerSetpointUnit
        '
        Me.LblRealPowerSetpointUnit.AutoSize = true
        Me.LblRealPowerSetpointUnit.Location = New System.Drawing.Point(242, 22)
        Me.LblRealPowerSetpointUnit.Name = "LblRealPowerSetpointUnit"
        Me.LblRealPowerSetpointUnit.Size = New System.Drawing.Size(18, 13)
        Me.LblRealPowerSetpointUnit.TabIndex = 38
        Me.LblRealPowerSetpointUnit.Text = "W"
        '
        'TxtDeltaE
        '
        Me.TxtDeltaE.Location = New System.Drawing.Point(183, 97)
        Me.TxtDeltaE.Name = "TxtDeltaE"
        Me.TxtDeltaE.ReadOnly = true
        Me.TxtDeltaE.Size = New System.Drawing.Size(53, 20)
        Me.TxtDeltaE.TabIndex = 39
        '
        'TxtDeltaOmega
        '
        Me.TxtDeltaOmega.Location = New System.Drawing.Point(183, 71)
        Me.TxtDeltaOmega.Name = "TxtDeltaOmega"
        Me.TxtDeltaOmega.ReadOnly = true
        Me.TxtDeltaOmega.Size = New System.Drawing.Size(53, 20)
        Me.TxtDeltaOmega.TabIndex = 37
        '
        'TxtReactivePowerSetpoint
        '
        Me.TxtReactivePowerSetpoint.Location = New System.Drawing.Point(183, 45)
        Me.TxtReactivePowerSetpoint.Name = "TxtReactivePowerSetpoint"
        Me.TxtReactivePowerSetpoint.ReadOnly = true
        Me.TxtReactivePowerSetpoint.Size = New System.Drawing.Size(53, 20)
        Me.TxtReactivePowerSetpoint.TabIndex = 33
        '
        'TxtRealPowerSetpoint
        '
        Me.TxtRealPowerSetpoint.Location = New System.Drawing.Point(183, 19)
        Me.TxtRealPowerSetpoint.Name = "TxtRealPowerSetpoint"
        Me.TxtRealPowerSetpoint.ReadOnly = true
        Me.TxtRealPowerSetpoint.Size = New System.Drawing.Size(53, 20)
        Me.TxtRealPowerSetpoint.TabIndex = 31
        '
        'LblDeltaE
        '
        Me.LblDeltaE.AutoSize = true
        Me.LblDeltaE.Location = New System.Drawing.Point(6, 100)
        Me.LblDeltaE.Name = "LblDeltaE"
        Me.LblDeltaE.Size = New System.Drawing.Size(107, 13)
        Me.LblDeltaE.TabIndex = 38
        Me.LblDeltaE.Text = "Voltage Control (δ_E)"
        '
        'LblDeltaOmega
        '
        Me.LblDeltaOmega.AutoSize = true
        Me.LblDeltaOmega.Location = New System.Drawing.Point(6, 74)
        Me.LblDeltaOmega.Name = "LblDeltaOmega"
        Me.LblDeltaOmega.Size = New System.Drawing.Size(103, 13)
        Me.LblDeltaOmega.TabIndex = 36
        Me.LblDeltaOmega.Text = "Speed Control (δ_ω)"
        '
        'LblReactivePowerSetpoint
        '
        Me.LblReactivePowerSetpoint.AutoSize = true
        Me.LblReactivePowerSetpoint.Location = New System.Drawing.Point(6, 48)
        Me.LblReactivePowerSetpoint.Name = "LblReactivePowerSetpoint"
        Me.LblReactivePowerSetpoint.Size = New System.Drawing.Size(154, 13)
        Me.LblReactivePowerSetpoint.TabIndex = 32
        Me.LblReactivePowerSetpoint.Text = "Reactive Power Setpoint (Q_0)"
        '
        'LblRealPowerSetpoint
        '
        Me.LblRealPowerSetpoint.AutoSize = true
        Me.LblRealPowerSetpoint.Location = New System.Drawing.Point(6, 22)
        Me.LblRealPowerSetpoint.Name = "LblRealPowerSetpoint"
        Me.LblRealPowerSetpoint.Size = New System.Drawing.Size(132, 13)
        Me.LblRealPowerSetpoint.TabIndex = 30
        Me.LblRealPowerSetpoint.Text = "Real Power Setpoint (P_0)"
        '
        'menContextDevices
        '
        Me.menContextDevices.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menManageDevices})
        Me.menContextDevices.Name = "ContextMenuStrip1"
        Me.menContextDevices.ShowImageMargin = false
        Me.menContextDevices.Size = New System.Drawing.Size(136, 26)
        '
        'menManageDevices
        '
        Me.menManageDevices.Name = "menManageDevices"
        Me.menManageDevices.Size = New System.Drawing.Size(135, 22)
        Me.menManageDevices.Text = "Manage Devices"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'FrmControls
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(657, 436)
        Me.Controls.Add(Me.GrpControl)
        Me.Controls.Add(Me.BtnApply)
        Me.Controls.Add(Me.BtnModeSwitch)
        Me.Controls.Add(Me.GrpMeasurements)
        Me.Controls.Add(Me.GrpDeviceList)
        Me.Name = "FrmControls"
        Me.Text = "Device Interface"
        Me.GrpDeviceList.ResumeLayout(false)
        Me.GrpMeasurements.ResumeLayout(false)
        Me.GrpMeasurements.PerformLayout
        Me.GrpControl.ResumeLayout(false)
        Me.GrpControl.PerformLayout
        Me.menContextDevices.ResumeLayout(false)
        Me.ResumeLayout(false)

End Sub

    Friend WithEvents LblRealPower As Label
    Friend WithEvents TxtRealPower As TextBox
    Friend WithEvents GrpDeviceList As GroupBox
    Friend WithEvents GrpMeasurements As GroupBox
    Friend WithEvents BtnModeSwitch As Button
    Friend WithEvents BtnApply As Button
    Friend WithEvents LstDevices As ListView
    Friend WithEvents ClmDevice As ColumnHeader
    Friend WithEvents ClmConnection As ColumnHeader
    Friend WithEvents ClmStatus As ColumnHeader
    Friend WithEvents TxtVoltageMagnitudeA As TextBox
    Friend WithEvents TxtReactivePower As TextBox
    Friend WithEvents LblCurrentMagnitude As Label
    Friend WithEvents LblVoltageAngle As Label
    Friend WithEvents LblVoltageMagnitude As Label
    Friend WithEvents LblReactivePower As Label
    Friend WithEvents TxtVoltageAngleA As TextBox
    Friend WithEvents LblEnergyStorage As Label
    Friend WithEvents LblPVGeneration As Label
    Friend WithEvents LblDeviceFrequency As Label
    Friend WithEvents LblCurrentAngle As Label
    Friend WithEvents GrpControl As GroupBox
    Friend WithEvents TxtEnergyStorage As TextBox
    Friend WithEvents TxtPvGeneration As TextBox
    Friend WithEvents TxtDeviceFrequency As TextBox
    Friend WithEvents TxtCurrentAngleA As TextBox
    Friend WithEvents TxtCurrentMagnitudeA As TextBox
    Friend WithEvents TxtDeltaE As TextBox
    Friend WithEvents TxtDeltaOmega As TextBox
    Friend WithEvents TxtReactivePowerSetpoint As TextBox
    Friend WithEvents TxtRealPowerSetpoint As TextBox
    Friend WithEvents LblDeltaE As Label
    Friend WithEvents LblDeltaOmega As Label
    Friend WithEvents LblReactivePowerSetpoint As Label
    Friend WithEvents LblRealPowerSetpoint As Label
    Friend WithEvents LblEnergyStorageUnit As Label
    Friend WithEvents LblPVGenerationUnit As Label
    Friend WithEvents LblDeviceFrequencyUnit As Label
    Friend WithEvents LblCurrentAngleUnit As Label
    Friend WithEvents LblCurrentMagnitudeUnit As Label
    Friend WithEvents LblVoltageAngleUnit As Label
    Friend WithEvents LblVoltageMagnitudeUnit As Label
    Friend WithEvents LblReactivePowerUnit As Label
    Friend WithEvents LblRealPowerUnit As Label
    Friend WithEvents LblDeltaEUnit As Label
    Friend WithEvents LblDeltaOmegaUnit As Label
    Friend WithEvents LblReactivePowerSetpointUnit As Label
    Friend WithEvents LblRealPowerSetpointUnit As Label
    Friend WithEvents LblAverageEnergyUnit As Label
    Friend WithEvents TxtAverageEnergy As TextBox
    Friend WithEvents LblAverageEnergy As Label
    Friend WithEvents menContextDevices As ContextMenuStrip
    Friend WithEvents menManageDevices As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
End Class
