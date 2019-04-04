Option Strict On
Option Explicit On

Imports RTModbusControl.My.Resources

' TODO: Allow new Connections/EnergyCells/Controllers to be added after initialization (menu option, button, right-click, etc.)
Public Class FrmControls
    Private _lastSelectedDevice As Integer = 0 ' Index of current EnergyCell to display
    Private _controlLoop As RTControlLoop

    ' On form load, check for existing connections and pair with devices; if no connections, offer to guide creation.
    ' Begins control Loop after initializations complete.
    ' Also attempts to handle re-initialization after leaving form and coming back
    Private Sub InitializeControls(sender As Object, e As EventArgs) Handles Me.Load, Me.Activated
        Dim numConnections As Integer = Connections.All.Count

        If numConnections > 0 Then
            InitializeDevices()
            InitializeConnections()
        Else
            Dim messageResult As DialogResult = MessageBox.Show(AllMessages.NoSlaveConnection,
                                                                AllMessages.NoSlaveConnectionCaption,
                                                                MessageBoxButtons.OKCancel,
                                                                MessageBoxIcon.None,
                                                                MessageBoxDefaultButton.Button1)
            If messageResult.Equals(DialogResult.OK) Then
                Dim connectionResult As DialogResult

                FrmConnection.InteractionMode = "New"
                connectionResult = FrmConnection.ShowDialog()

                If connectionResult.Equals(DialogResult.Cancel) Then
                    Close()
                ElseIf connectionResult.Equals(DialogResult.OK) Then
                    InitializeDevices()
                    InitializeConnections()
                End If
            ElseIf messageResult.Equals(DialogResult.Cancel) Then
                MessageBox.Show(AllMessages.NoConnections,
                                AllMessages.NoConnectionsCaption,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.None,
                                MessageBoxDefaultButton.Button1)
                Close()
            End If
        End If

        If IsNothing(_controlLoop) Then
            _controlLoop = New RTControlLoop()
            AddHandler _controlLoop.Update, AddressOf LoopUpdateDelegate
            _controlLoop.RunLoop()
        ElseIf Not RTControlLoop.GetIsRunning() Then
            _controlLoop.RunLoop()
        End If
    End Sub

    ' On form close, stops the control loop timer, shuts down energy cell and controller client connections, and clears the device list
    Private Sub CloseControls(sender As Object, e As EventArgs) Handles Me.FormClosing
        If Not IsNothing(_controlLoop)
            _controlLoop.StopLoop()
        End If

        Parallel.ForEach(EnergyCells.All, Async Sub(ec)
            ec.Status = "Local"
            Await ec.SendLocalControlCommandAsync()
            ec.CellConnection.ShutDown = True
            ec.CellConnection.Client.Disconnect()
        End Sub)

        Parallel.ForEach(UpstreamControllers.All, Sub(c)
            c.ControllerConnection.ShutDown = True
            c.ControllerConnection.Client.Disconnect()
        End Sub)

        LstDevices.Clear()
    End Sub

    ' On selection of new device in listview, update display of data and enable/disable/rename buttons as appropriate
    ' There is a well-known "feature" in which this event fires twice (de-select and select), thus the .Count > 0 check
    ' TODO: Investigate adding a tag for pseudo binding objects to items
    '       This can done by changing the binding on LstDevices.ItemSelectionChanged
    Private Sub ChangeDeviceSelection(sender As Object, e As EventArgs) Handles LstDevices.ItemSelectionChanged
        If LstDevices.SelectedItems.Count > 0 Then
            Dim cItem = DirectCast(e, ListViewItemSelectionChangedEventArgs).Item
            Dim cName = cItem.Name
            Dim upstream = Connections.All.Find(Function(p) p.Name.Equals(cName)).Upstream

            If Not upstream Then
                _lastSelectedDevice = DirectCast(e, ListViewItemSelectionChangedEventArgs).ItemIndex
                Dim currentEnergyCell = EnergyCells.All.Find(Function(p) p.Name.Equals(cName))
                ' Measurement display change
                DisplayValue(currentEnergyCell.RealPower, "RealPower", TxtRealPower, LblRealPowerUnit)
                DisplayValue(currentEnergyCell.ReactivePower, "ReactivePower", TxtReactivePower, LblReactivePowerUnit)
                DisplayValue(currentEnergyCell.VoltageMagnitudeA, "Voltage", TxtVoltageMagnitudeA,
                             LblVoltageMagnitudeUnit)
                DisplayValue(currentEnergyCell.VoltageAngleA, "Angle", TxtVoltageAngleA, LblVoltageAngleUnit)
                DisplayValue(currentEnergyCell.CurrentMagnitudeA, "Current", TxtCurrentMagnitudeA,
                             LblCurrentMagnitudeUnit)
                DisplayValue(currentEnergyCell.CurrentAngleA, "Angle", TxtCurrentAngleA, LblCurrentAngleUnit)
                DisplayValue(currentEnergyCell.Speed, "Frequency", TxtDeviceFrequency, LblDeviceFrequencyUnit)
                DisplayValue(currentEnergyCell.PvGeneration, "RealPower", TxtPvGeneration, LblPVGenerationUnit)
                DisplayValue(currentEnergyCell.EnergyStorage, "Energy", TxtEnergyStorage, LblEnergyStorageUnit)

                ' Setpoint display change
                If _
                    (currentEnergyCell.Status.Equals("Remote") And currentEnergyCell.Mode.Equals("Automated")) Or
                    currentEnergyCell.Status.Equals("Local") Then
                    DisplayValue(currentEnergyCell.RealPowerSetpoint, "RealPower", TxtRealPowerSetpoint,
                                 LblRealPowerSetpointUnit)
                    DisplayValue(currentEnergyCell.ReactivePowerSetpoint, "ReactivePower", TxtReactivePowerSetpoint,
                                 LblReactivePowerSetpointUnit)
                    DisplayValue(currentEnergyCell.DeltaW, "Frequency", TxtDeltaOmega, LblDeltaOmegaUnit)
                    DisplayValue(currentEnergyCell.DeltaE, "Voltage", TxtDeltaE, LblDeltaEUnit)
                    DisplayValue(currentEnergyCell.EnergyStorageAverage, "Energy", TxtAverageEnergy,
                                 LblAverageEnergyUnit)
                    LblDeltaOmega.Text = AllControlText.SpeedControlLabel
                Else ' Only update last three
                    DisplayValue(currentEnergyCell.DeltaW, "Frequency", TxtDeltaOmega, LblDeltaOmegaUnit)
                    DisplayValue(currentEnergyCell.DeltaE, "Voltage", TxtDeltaE, LblDeltaEUnit)
                    DisplayValue(currentEnergyCell.EnergyStorageAverage, "Energy", TxtAverageEnergy,
                                 LblAverageEnergyUnit)
                    LblDeltaOmega.Text = AllControlText.SpeedControlLabel
                End If

                ' Form controls display check/change
                If cItem.Checked Then
                    If currentEnergyCell.Mode.Equals("Automated") Then
                        TxtRealPowerSetpoint.ReadOnly = True
                        TxtReactivePowerSetpoint.ReadOnly = True
                        TxtAverageEnergy.ReadOnly = True
                        TxtDeltaOmega.ReadOnly = True
                        TxtDeltaE.ReadOnly = True
                        BtnModeSwitch.Text = AllControlText.BtnTextManualControls
                        BtnModeSwitch.Enabled = True
                        BtnModeSwitch.Visible = True
                        BtnApply.Enabled = False
                        BtnApply.Visible = False
                    ElseIf currentEnergyCell.Mode.Equals("Manual") Then
                        TxtRealPowerSetpoint.ReadOnly = False
                        TxtReactivePowerSetpoint.ReadOnly = False
                        TxtAverageEnergy.ReadOnly = True
                        TxtDeltaOmega.ReadOnly = True
                        TxtDeltaE.ReadOnly = True
                        BtnModeSwitch.Text = AllControlText.BtnTextAutomatedControls
                        BtnModeSwitch.Enabled = True
                        BtnModeSwitch.Visible = True
                        BtnApply.Enabled = True
                        BtnApply.Visible = True
                    End If
                Else
                    If currentEnergyCell.Mode.Equals("Automated") Then
                        BtnModeSwitch.Text = AllControlText.BtnTextManualControls
                    ElseIf currentEnergyCell.Mode.Equals("Manual") Then
                        BtnModeSwitch.Text = AllControlText.BtnTextAutomatedControls
                    End If

                    TxtRealPowerSetpoint.ReadOnly = True
                    TxtReactivePowerSetpoint.ReadOnly = True
                    TxtAverageEnergy.ReadOnly = True
                    TxtDeltaOmega.ReadOnly = True
                    TxtDeltaE.ReadOnly = True
                    BtnModeSwitch.Enabled = False
                    BtnModeSwitch.Visible = False
                    BtnApply.Enabled = False
                    BtnApply.Visible = False
                End If
            Else
                Dim currentController = UpstreamControllers.All.Find(Function(p) p.Name.Equals(cName))

                ' Measurement display change
                DisplayValue(0, "RealPower", TxtRealPower, LblRealPowerUnit)
                DisplayValue(0, "ReactivePower", TxtReactivePower, LblReactivePowerUnit)
                DisplayValue(0, "Voltage", TxtVoltageMagnitudeA, LblVoltageMagnitudeUnit)
                DisplayValue(0, "Angle", TxtVoltageAngleA, LblVoltageAngleUnit)
                DisplayValue(0, "Current", TxtCurrentMagnitudeA, LblCurrentMagnitudeUnit)
                DisplayValue(0, "Angle", TxtCurrentAngleA, LblCurrentAngleUnit)
                DisplayValue(0, "Frequency", TxtDeviceFrequency, LblDeviceFrequencyUnit)
                DisplayValue(0, "RealPower", TxtPvGeneration, LblPVGenerationUnit)
                DisplayValue(0, "Energy", TxtEnergyStorage, LblEnergyStorageUnit)

                ' Setpoint display change
                DisplayValue(0, "RealPower", TxtRealPowerSetpoint, LblRealPowerSetpointUnit)
                DisplayValue(0, "ReactivePower", TxtReactivePowerSetpoint, LblReactivePowerSetpointUnit)
                DisplayValue(0, "Voltage", TxtAverageEnergy, LblAverageEnergyUnit)
                DisplayValue(currentController.DeltaOmega, "Frequency", TxtDeltaOmega, LblDeltaOmegaUnit)
                DisplayValue(0, "Voltage", TxtDeltaE, LblDeltaEUnit)
                LblDeltaOmega.Text = AllControlText.SynchronizingSpeedLabel

                ' Disable and hide buttons
                BtnModeSwitch.Enabled = False
                BtnModeSwitch.Visible = False
                BtnApply.Enabled = False
                BtnApply.Visible = False
            End If
        End If
    End Sub

    ' On change in status of checkbox for energy cell, update appropriate form controls and device properties
    Private Sub SwitchStatus(sender As Object, e As ItemCheckEventArgs) Handles LstDevices.ItemCheck
        Dim cItem = DirectCast(sender, ListView).Items(e.Index)
        Dim cName = cItem.Name
        Dim upstream = Connections.All.Find(Function(p) p.Name.Equals(cName)).Upstream

        If Not upstream Then
            ' Form controls display check/change
            If e.NewValue.Equals(CheckState.Unchecked) Then
                EnergyCells.All.Find(Function(p) p.Name.Equals(cName)).Status = "Local"
                TxtRealPowerSetpoint.ReadOnly = True
                TxtReactivePowerSetpoint.ReadOnly = True
                TxtAverageEnergy.ReadOnly = True
                TxtDeltaOmega.ReadOnly = True
                TxtDeltaE.ReadOnly = True
                BtnModeSwitch.Enabled = False
                BtnModeSwitch.Visible = False
                BtnApply.Enabled = False
                BtnApply.Visible = False
            Else
                EnergyCells.All.Find(Function(p) p.Name.Equals(cName)).Status = "Remote"
                If EnergyCells.All.Find(Function(p) p.Name.Equals(cName)).Mode.Equals("Automated") Then
                    TxtRealPowerSetpoint.ReadOnly = True
                    TxtReactivePowerSetpoint.ReadOnly = True
                    TxtAverageEnergy.ReadOnly = True
                    TxtDeltaOmega.ReadOnly = True
                    TxtDeltaE.ReadOnly = True
                    BtnModeSwitch.Text = AllControlText.BtnTextManualControls
                    BtnModeSwitch.Enabled = True
                    BtnModeSwitch.Visible = True
                    BtnApply.Enabled = False
                    BtnApply.Visible = False
                ElseIf EnergyCells.All.Find(Function(p) p.Name.Equals(cName)).Mode.Equals("Manual") Then
                    TxtRealPowerSetpoint.ReadOnly = False
                    TxtReactivePowerSetpoint.ReadOnly = False
                    TxtAverageEnergy.ReadOnly = True
                    TxtDeltaOmega.ReadOnly = True
                    TxtDeltaE.ReadOnly = True
                    BtnModeSwitch.Text = AllControlText.BtnTextAutomatedControls
                    BtnModeSwitch.Enabled = True
                    BtnModeSwitch.Visible = True
                    BtnApply.Enabled = True
                    BtnApply.Visible = True
                End If
            End If
        Else ' Disable and hide buttons
            BtnModeSwitch.Enabled = False
            BtnModeSwitch.Visible = False
            BtnApply.Enabled = False
            BtnApply.Visible = False
        End If
    End Sub

    ' On switching modes, update text on button, mode in energy cell, apply button status, and text box read only statuses
    Private Sub SwitchMode(sender As Object, e As EventArgs) Handles BtnModeSwitch.Click
        Dim cName = LstDevices.Items(_lastSelectedDevice).SubItems(0).Text

        If BtnModeSwitch.Text.Equals(AllControlText.BtnTextAutomatedControls) Then
            BtnModeSwitch.Text = AllControlText.BtnTextManualControls
            EnergyCells.All.Find(Function(p) p.Name.Equals(cName)).Mode = "Automated"
            TxtRealPowerSetpoint.ReadOnly = True
            TxtReactivePowerSetpoint.ReadOnly = True
            TxtAverageEnergy.ReadOnly = True
            BtnApply.Enabled = False
            BtnApply.Visible = False
        ElseIf BtnModeSwitch.Text.Equals(AllControlText.BtnTextManualControls) Then
            BtnModeSwitch.Text = AllControlText.BtnTextAutomatedControls
            LblRealPowerSetpointUnit.Text = $"W"
            LblReactivePowerSetpointUnit.Text = $"VAr"
            EnergyCells.All.Find(Function(p) p.Name.Equals(cName)).Mode = "Manual"
            TxtRealPowerSetpoint.ReadOnly = False
            TxtReactivePowerSetpoint.ReadOnly = False
            TxtAverageEnergy.ReadOnly = True
            BtnApply.Enabled = True
            BtnApply.Visible = True
        End If
    End Sub

    ' Applies manual control settings to selected EnergyCell
    Private Sub ApplyChanges(sender As Object, e As EventArgs) Handles BtnApply.Click
        Dim cName = LstDevices.Items(_lastSelectedDevice).SubItems(0).Text

        EnergyCells.All.Find(Function(p) p.Name.Equals(cName)).RealPowerSetpoint = CDbl(TxtRealPowerSetpoint.Text)
        EnergyCells.All.Find(Function(p) p.Name.Equals(cName)).ReactivePowerSetpoint = CDbl(TxtReactivePowerSetpoint.Text)
    End Sub

    ' Allows editing of EnergyCells/Controllers during operation by opening edit form
    Private Sub ManageDevices(sender As Object, e As EventArgs) Handles menManageDevices.Click
        FrmDevices.Show()
    End Sub

    ' Delegate subroutine for handling cross-thread display updates to the UI
    ' And the associated thread-safe invoker of UI updates, the event handler for RTControlLoop.Update
    Private Delegate Sub LoopDelegate()

    Private Sub LoopUpdateDelegate(sender As Object, e As EventArgs)
        If LstDevices.InvokeRequired Then
            LstDevices.BeginInvoke(New LoopDelegate(AddressOf UpdateDisplay))
        End If
    End Sub

    ' Called by ControlLoop.OnPulse every period (Ts) to update display of variables via the delegate above
    Private Sub UpdateDisplay()
        If LstDevices.Items.Count > 0 Then
            Dim cName As String

            If LstDevices.SelectedItems.Count > 0 Then
                cName = LstDevices.SelectedItems(0).Name
            Else
                cName = LstDevices.Items(_lastSelectedDevice).Name
            End If

            Dim upstream = Connections.All.Find(Function(p) p.Name.Equals(cName)).Upstream

            If Not upstream Then
                Dim currentEnergyCell = EnergyCells.All.Find(Function(p) p.Name.Equals(cName))

                ' Update measurement display
                DisplayValue(currentEnergyCell.RealPower, "RealPower", TxtRealPower, LblRealPowerUnit)
                DisplayValue(currentEnergyCell.ReactivePower, "ReactivePower", TxtReactivePower, LblReactivePowerUnit)
                DisplayValue(currentEnergyCell.VoltageMagnitudeA, "Voltage", TxtVoltageMagnitudeA,
                             LblVoltageMagnitudeUnit)
                DisplayValue(currentEnergyCell.VoltageAngleA, "Angle", TxtVoltageAngleA, LblVoltageAngleUnit)
                DisplayValue(currentEnergyCell.CurrentMagnitudeA, "Current", TxtCurrentMagnitudeA,
                             LblCurrentMagnitudeUnit)
                DisplayValue(currentEnergyCell.CurrentAngleA, "Angle", TxtCurrentAngleA, LblCurrentAngleUnit)
                DisplayValue(currentEnergyCell.Speed, "Frequency", TxtDeviceFrequency, LblDeviceFrequencyUnit)
                DisplayValue(currentEnergyCell.PvGeneration, "RealPower", TxtPvGeneration, LblPVGenerationUnit)
                DisplayValue(currentEnergyCell.EnergyStorage, "Energy", TxtEnergyStorage, LblEnergyStorageUnit)

                ' Update setpoint display if automated remote or local control
                If (currentEnergyCell.Status.Equals("Remote") And currentEnergyCell.Mode.Equals("Automated")) Or
                        currentEnergyCell.Status.Equals("Local") Then
                    DisplayValue(currentEnergyCell.RealPowerSetpoint, "RealPower", TxtRealPowerSetpoint,
                                 LblRealPowerSetpointUnit)
                    DisplayValue(currentEnergyCell.ReactivePowerSetpoint, "ReactivePower", TxtReactivePowerSetpoint,
                                 LblReactivePowerSetpointUnit)
                    DisplayValue(currentEnergyCell.DeltaW, "Frequency", TxtDeltaOmega, LblDeltaOmegaUnit)
                    DisplayValue(currentEnergyCell.DeltaE, "Voltage", TxtDeltaE, LblDeltaEUnit)
                    DisplayValue(currentEnergyCell.EnergyStorageAverage, "Energy", TxtAverageEnergy,
                                 LblAverageEnergyUnit)
                    LblDeltaOmega.Text = AllControlText.SpeedControlLabel
                Else ' Only update last three if manual remote control
                    DisplayValue(currentEnergyCell.DeltaW, "Frequency", TxtDeltaOmega, LblDeltaOmegaUnit)
                    DisplayValue(currentEnergyCell.DeltaE, "Voltage", TxtDeltaE, LblDeltaEUnit)
                    DisplayValue(currentEnergyCell.EnergyStorageAverage, "Energy", TxtAverageEnergy,
                                 LblAverageEnergyUnit)
                    LblDeltaOmega.Text = AllControlText.SpeedControlLabel
                End If
            Else
                Dim currentController = UpstreamControllers.All.Find(Function(p) p.Name.Equals(cName))

                ' Update measurement display
                DisplayValue(0, "RealPower", TxtRealPower, LblRealPowerUnit)
                DisplayValue(0, "ReactivePower", TxtReactivePower, LblReactivePowerUnit)
                DisplayValue(0, "Voltage", TxtVoltageMagnitudeA, LblVoltageMagnitudeUnit)
                DisplayValue(0, "Angle", TxtVoltageAngleA, LblVoltageAngleUnit)
                DisplayValue(0, "Current", TxtCurrentMagnitudeA, LblCurrentMagnitudeUnit)
                DisplayValue(0, "Angle", TxtCurrentAngleA, LblCurrentAngleUnit)
                DisplayValue(0, "Frequency", TxtDeviceFrequency, LblDeviceFrequencyUnit)
                DisplayValue(0, "RealPower", TxtPvGeneration, LblPVGenerationUnit)
                DisplayValue(0, "Energy", TxtEnergyStorage, LblEnergyStorageUnit)

                ' Update setpoint display
                DisplayValue(0, "RealPower", TxtRealPowerSetpoint, LblRealPowerSetpointUnit)
                DisplayValue(0, "ReactivePower", TxtReactivePowerSetpoint, LblReactivePowerSetpointUnit)
                DisplayValue(0, "Energy", TxtAverageEnergy, LblAverageEnergyUnit)
                DisplayValue(currentController.DeltaOmega, "Frequency", TxtDeltaOmega, LblDeltaOmegaUnit)
                DisplayValue(0, "Voltage", TxtDeltaE, LblDeltaEUnit)
                LblDeltaOmega.Text = AllControlText.SynchronizingSpeedLabel
            End If

            ' Update Connection Status for all devices (including upstream controllers)
            For Each device As ListViewItem In LstDevices.Items
                If EnergyCells.All.Exists(Function(p) p.Name.Equals(device.Name))
                    Dim currentItem = EnergyCells.All.Find(Function(p) p.Name.Equals(device.Name))

                    If currentItem.CellConnection.Client.Connected Then
                        LstDevices.Items.Item(currentItem.Name).SubItems(2).Text = AllControlText.StatusConnected
                    Else
                        If currentItem.CellConnection.Client.Available(4000) Then
                            LstDevices.Items.Item(currentItem.Name).SubItems(2).Text = AllControlText.StatusConnecting
                        Else
                            LstDevices.Items.Item(currentItem.Name).SubItems(2).Text = AllControlText.StatusUnavailable
                        End If
                    End If
                ElseIf UpstreamControllers.All.Exists(Function(p) p.Name.Equals(device.Name))
                    Dim currentItem = UpstreamControllers.All.Find(Function(p) p.Name.Equals(device.Name))

                    If currentItem.ControllerConnection.Client.Connected Then
                        LstDevices.Items.Item(currentItem.Name).SubItems(2).Text = AllControlText.StatusConnected
                    Else
                        If currentItem.ControllerConnection.Client.Available(4000) Then
                            LstDevices.Items.Item(currentItem.Name).SubItems(2).Text = AllControlText.StatusConnecting
                        Else
                            LstDevices.Items.Item(currentItem.Name).SubItems(2).Text = AllControlText.StatusUnavailable
                        End If
                    End If
                End If
            Next

        End If

        Application.DoEvents()
    End Sub

    ' Iterates over connections, creating EnergyCells or Controllers as appropriate and adding them to their respective collections.
    ' Adds items to LstDevices for interaction if they're not already there.
    Private Sub InitializeDevices()
        For Each c As Connection In Connections.All
            Dim cName = c.Name

            Dim item As New ListViewItem With {
                .Name = cName}

            item.SubItems.AddRange({"ClmDevice", "ClmConnection", "ClmStatus"})
            item.SubItems(0).Text = cName
            item.SubItems(1).Text = $"{c.IpAddress}:{c.Port}"
            item.SubItems(2).Text = $"None"
            item.Checked = False

            If Not c.Upstream Then
                If Not EnergyCells.All.Exists(Function(p) p.Name.Equals(cName)) Then
                    Dim energyCell As New EnergyCell With {
                        .Name = cName,
                        .CellConnection = c}

                    EnergyCells.All.Add(energyCell)

                    If LstDevices.Items.count > 0
                        If IsNothing(LstDevices.FindItemWithText(cName, True, 0)) Then
                            LstDevices.Items.Add(item)
                        End If
                    Else
                        LstDevices.Items.Add(item)
                    End If
                Else
                    If LstDevices.Items.count > 0
                        If IsNothing(LstDevices.FindItemWithText(cName, True, 0)) Then
                            LstDevices.Items.Add(item)
                        End If
                    Else
                        LstDevices.Items.Add(item)
                    End If
                End If
            Else ' Handle the upstream controller
                If Not UpstreamControllers.All.Exists(Function(p) p.Name.Equals(cName)) Then
                    Dim upstreamController As New UpstreamController With {
                            .Name = cName,
                            .ControllerConnection = c}

                    UpstreamControllers.All.Add(upstreamController)

                    If LstDevices.Items.count > 0
                        If IsNothing(LstDevices.FindItemWithText(cName, True, 0)) Then
                            LstDevices.Items.Add(item)
                        End If
                    Else
                        LstDevices.Items.Add(item)
                    End If
                Else
                    If LstDevices.Items.count > 0
                        If IsNothing(LstDevices.FindItemWithText(cName, True, 0)) Then
                            LstDevices.Items.Add(item)
                        End If
                    Else
                        LstDevices.Items.Add(item)
                    End If
                End If
            End If
        Next
    End Sub

    ' Makes Connections to all known EnergyCells if possible and updates status of each in LstDevices.
    ' Ditto for UpstreamControllers.
    Private Sub InitializeConnections()
        For Each c As EnergyCell In EnergyCells.All
            If Not c.CellConnection.Client.Connected Then
                c.CellConnection.ShutDown = False

                If c.CellConnection.Client.Available(4000) Then
                    Try
                        c.CellConnection.Client.Connect(c.CellConnection.Client.IPAddress, c.CellConnection.Client.Port)
                        LstDevices.Items.Item(c.Name).SubItems(2).Text = AllControlText.StatusConnected
                    Catch ex As Exception
                        Console.WriteLine($"{Now.Hour}:{Now.Minute}:{Now.Second}.{Now.Millisecond} - Ex - {c.Name}:  {ex.Message}")
                    End Try
                Else
                    LstDevices.Items.Item(c.Name).SubItems(2).Text = AllControlText.StatusUnavailable
                End If
            Else
                LstDevices.Items.Item(c.Name).SubItems(2).Text = AllControlText.StatusConnected
            End If
        Next

        For Each c As UpstreamController In UpstreamControllers.All
            If Not c.ControllerConnection.Client.Connected Then
                c.ControllerConnection.ShutDown = False

                If c.ControllerConnection.Client.Available(4000) Then
                    Try
                        c.ControllerConnection.Client.Connect(c.ControllerConnection.Client.IPAddress,
                                                              c.ControllerConnection.Client.Port)
                        LstDevices.Items.Item(c.Name).SubItems(2).Text = AllControlText.StatusConnected
                    Catch ex As Exception
                        Console.WriteLine($"{Now.Hour}:{Now.Minute}:{Now.Second}.{Now.Millisecond} - Ex - {c.Name}:  {ex.Message}")
                    End Try
                Else
                    LstDevices.Items.Item(c.Name).SubItems(2).Text = AllControlText.StatusUnavailable
                End If
            Else
                LstDevices.Items.Item(c.Name).SubItems(2).Text = AllControlText.StatusConnected
            End If
        Next
    End Sub

    ' Presents data in form, keeping displayed values in [0, 1000) and changing units
    Private Shared Sub DisplayValue(number As Double, numType As String, ByRef txt As TextBox, ByRef lbl As Label)
        Dim numDivisions = 0
        Dim unitSet As String()

        Select Case numType
            Case "Voltage"
                unitSet = {"V", "kV", "MV", "GV"}
            Case "Current"
                unitSet = {"A", "kA", "MA", "GA"}
            Case "Angle"
                unitSet = {"°"}
            Case "RealPower"
                unitSet = {"W", "kW", "MW", "GW"}
            Case "ReactivePower"
                unitSet = {"VAr", "kVAr", "MVAr", "GVAr"}
            Case "Frequency"
                unitSet = {"rad/s", "krad/s", "Mrad/s", "Grad/s"}
            Case "Energy"
                unitSet = {"MJ", "GJ", "TJ", "PJ"}
            Case Else
                unitSet = {"", "", "", ""}
        End Select

        While Math.Abs(number) > 1000.0
            number = number/1000.0
            numDivisions = numDivisions + 1
        End While

        txt.Text = CType(number, String)
        lbl.Text = unitSet(numDivisions)
    End Sub
End Class