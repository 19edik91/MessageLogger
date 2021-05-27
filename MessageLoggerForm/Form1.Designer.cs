﻿namespace MessageLoggerForm
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listView1 = new System.Windows.Forms.ListView();
            this.LVIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LVTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LVMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LVDestination = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LVSource = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LVKind = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LVPayload = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LVObject = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LVCommand = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LVInterpretation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.RichTextBoxSerialData = new System.Windows.Forms.RichTextBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnClearListView = new System.Windows.Forms.Button();
            this.BtnResizeListView = new System.Windows.Forms.Button();
            this.grpBoardStatus = new System.Windows.Forms.GroupBox();
            this.GrpBoxLogging = new System.Windows.Forms.GroupBox();
            this.ChkBoxLogListView = new System.Windows.Forms.CheckBox();
            this.ChkBoxLogMsgBuffer = new System.Windows.Forms.CheckBox();
            this.ChkBoxLogRecData = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.LblTemperature = new System.Windows.Forms.Label();
            this.LblPower = new System.Windows.Forms.Label();
            this.LblCurrent = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.LblVoltage = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.LblAutomaticModeRes = new System.Windows.Forms.Label();
            this.LblAutomaticModeReq = new System.Windows.Forms.Label();
            this.LblBrightnessResp = new System.Windows.Forms.Label();
            this.LblLedStatusReq = new System.Windows.Forms.Label();
            this.LblBoardStatusReq = new System.Windows.Forms.Label();
            this.LblBoardStatusRes = new System.Windows.Forms.Label();
            this.LblBoardLedStatus = new System.Windows.Forms.Label();
            this.LblBoardBrightness = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LblLedStatusResp = new System.Windows.Forms.Label();
            this.LblBrightnessReq = new System.Windows.Forms.Label();
            this.tabCtrlSerialInterface = new System.Windows.Forms.TabControl();
            this.tabPageSerial0 = new System.Windows.Forms.TabPage();
            this.grpSerialInterface1 = new System.Windows.Forms.GroupBox();
            this.BtnComPortInit0 = new System.Windows.Forms.Button();
            this.LblComPortStatus0 = new System.Windows.Forms.Label();
            this.BtnComPortStop0 = new System.Windows.Forms.Button();
            this.BtnComPortStart0 = new System.Windows.Forms.Button();
            this.ComboBoxSerialComPorts0 = new System.Windows.Forms.ComboBox();
            this.LblComPort = new System.Windows.Forms.Label();
            this.tabPageSerial1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnComPortInit1 = new System.Windows.Forms.Button();
            this.LblComPortStatus1 = new System.Windows.Forms.Label();
            this.BtnComPortStop1 = new System.Windows.Forms.Button();
            this.BtnComPortStart1 = new System.Windows.Forms.Button();
            this.ComboBoxSerialComPorts1 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.serialPort2 = new System.IO.Ports.SerialPort(this.components);
            this.ChkBoxUpdateListView = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grpBoardStatus.SuspendLayout();
            this.GrpBoxLogging.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabCtrlSerialInterface.SuspendLayout();
            this.tabPageSerial0.SuspendLayout();
            this.grpSerialInterface1.SuspendLayout();
            this.tabPageSerial1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.LVIndex,
            this.LVTime,
            this.LVMessage,
            this.LVDestination,
            this.LVSource,
            this.LVKind,
            this.LVPayload,
            this.LVObject,
            this.LVCommand,
            this.LVInterpretation});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(3, 3);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1030, 648);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // LVIndex
            // 
            this.LVIndex.Text = "Index";
            this.LVIndex.Width = 58;
            // 
            // LVTime
            // 
            this.LVTime.Text = "Time";
            this.LVTime.Width = 110;
            // 
            // LVMessage
            // 
            this.LVMessage.Text = "Message";
            this.LVMessage.Width = 63;
            // 
            // LVDestination
            // 
            this.LVDestination.Text = "Destination";
            this.LVDestination.Width = 80;
            // 
            // LVSource
            // 
            this.LVSource.Text = "Source";
            // 
            // LVKind
            // 
            this.LVKind.Text = "Kind";
            // 
            // LVPayload
            // 
            this.LVPayload.Text = "Payload";
            // 
            // LVObject
            // 
            this.LVObject.Text = "Object";
            // 
            // LVCommand
            // 
            this.LVCommand.Text = "Command";
            this.LVCommand.Width = 87;
            // 
            // LVInterpretation
            // 
            this.LVInterpretation.Text = "Interpretation";
            this.LVInterpretation.Width = 120;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1044, 680);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1036, 654);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Messages";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.RichTextBoxSerialData);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1036, 654);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Serial Data";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // RichTextBoxSerialData
            // 
            this.RichTextBoxSerialData.Location = new System.Drawing.Point(6, 7);
            this.RichTextBoxSerialData.Name = "RichTextBoxSerialData";
            this.RichTextBoxSerialData.Size = new System.Drawing.Size(920, 641);
            this.RichTextBoxSerialData.TabIndex = 0;
            this.RichTextBoxSerialData.Text = "";
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 115200;
            this.serialPort1.ReadBufferSize = 2048;
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SerialPort1_DataReceived);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1313, 686);
            this.tableLayoutPanel3.TabIndex = 6;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.grpBoardStatus, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.tabCtrlSerialInterface, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(1053, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(249, 680);
            this.tableLayoutPanel4.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnClearListView);
            this.panel1.Controls.Add(this.BtnResizeListView);
            this.panel1.Location = new System.Drawing.Point(3, 637);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(243, 39);
            this.panel1.TabIndex = 2;
            // 
            // BtnClearListView
            // 
            this.BtnClearListView.Location = new System.Drawing.Point(87, 8);
            this.BtnClearListView.Name = "BtnClearListView";
            this.BtnClearListView.Size = new System.Drawing.Size(75, 23);
            this.BtnClearListView.TabIndex = 7;
            this.BtnClearListView.Text = "Clear List";
            this.BtnClearListView.UseVisualStyleBackColor = true;
            this.BtnClearListView.Click += new System.EventHandler(this.BtnClearListView_Click);
            // 
            // BtnResizeListView
            // 
            this.BtnResizeListView.Location = new System.Drawing.Point(3, 8);
            this.BtnResizeListView.Name = "BtnResizeListView";
            this.BtnResizeListView.Size = new System.Drawing.Size(75, 23);
            this.BtnResizeListView.TabIndex = 6;
            this.BtnResizeListView.Text = "Resize List";
            this.BtnResizeListView.UseVisualStyleBackColor = true;
            this.BtnResizeListView.Click += new System.EventHandler(this.ResizeListViewColumns);
            // 
            // grpBoardStatus
            // 
            this.grpBoardStatus.Controls.Add(this.ChkBoxUpdateListView);
            this.grpBoardStatus.Controls.Add(this.GrpBoxLogging);
            this.grpBoardStatus.Controls.Add(this.tableLayoutPanel2);
            this.grpBoardStatus.Controls.Add(this.tableLayoutPanel1);
            this.grpBoardStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBoardStatus.Location = new System.Drawing.Point(3, 161);
            this.grpBoardStatus.Name = "grpBoardStatus";
            this.grpBoardStatus.Size = new System.Drawing.Size(243, 470);
            this.grpBoardStatus.TabIndex = 5;
            this.grpBoardStatus.TabStop = false;
            this.grpBoardStatus.Text = "Board Status";
            // 
            // GrpBoxLogging
            // 
            this.GrpBoxLogging.Controls.Add(this.ChkBoxLogListView);
            this.GrpBoxLogging.Controls.Add(this.ChkBoxLogMsgBuffer);
            this.GrpBoxLogging.Controls.Add(this.ChkBoxLogRecData);
            this.GrpBoxLogging.Location = new System.Drawing.Point(9, 195);
            this.GrpBoxLogging.Name = "GrpBoxLogging";
            this.GrpBoxLogging.Size = new System.Drawing.Size(200, 100);
            this.GrpBoxLogging.TabIndex = 2;
            this.GrpBoxLogging.TabStop = false;
            this.GrpBoxLogging.Text = "Logging settings";
            // 
            // ChkBoxLogListView
            // 
            this.ChkBoxLogListView.AutoSize = true;
            this.ChkBoxLogListView.Location = new System.Drawing.Point(9, 68);
            this.ChkBoxLogListView.Name = "ChkBoxLogListView";
            this.ChkBoxLogListView.Size = new System.Drawing.Size(118, 17);
            this.ChkBoxLogListView.TabIndex = 2;
            this.ChkBoxLogListView.Text = "Log list view entries";
            this.ChkBoxLogListView.UseVisualStyleBackColor = true;
            // 
            // ChkBoxLogMsgBuffer
            // 
            this.ChkBoxLogMsgBuffer.AutoSize = true;
            this.ChkBoxLogMsgBuffer.Location = new System.Drawing.Point(8, 44);
            this.ChkBoxLogMsgBuffer.Name = "ChkBoxLogMsgBuffer";
            this.ChkBoxLogMsgBuffer.Size = new System.Drawing.Size(119, 17);
            this.ChkBoxLogMsgBuffer.TabIndex = 1;
            this.ChkBoxLogMsgBuffer.Text = "Log message buffer";
            this.ChkBoxLogMsgBuffer.UseVisualStyleBackColor = true;
            // 
            // ChkBoxLogRecData
            // 
            this.ChkBoxLogRecData.AutoSize = true;
            this.ChkBoxLogRecData.Location = new System.Drawing.Point(9, 20);
            this.ChkBoxLogRecData.Name = "ChkBoxLogRecData";
            this.ChkBoxLogRecData.Size = new System.Drawing.Size(112, 17);
            this.ChkBoxLogRecData.TabIndex = 0;
            this.ChkBoxLogRecData.Text = "Log received data";
            this.ChkBoxLogRecData.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.LblTemperature, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.LblPower, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.LblCurrent, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.LblVoltage, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(9, 332);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // LblTemperature
            // 
            this.LblTemperature.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LblTemperature.AutoSize = true;
            this.LblTemperature.Location = new System.Drawing.Point(138, 81);
            this.LblTemperature.Name = "LblTemperature";
            this.LblTemperature.Size = new System.Drawing.Size(24, 13);
            this.LblTemperature.TabIndex = 12;
            this.LblTemperature.Text = "0°C";
            // 
            // LblPower
            // 
            this.LblPower.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LblPower.AutoSize = true;
            this.LblPower.Location = new System.Drawing.Point(138, 56);
            this.LblPower.Name = "LblPower";
            this.LblPower.Size = new System.Drawing.Size(24, 13);
            this.LblPower.TabIndex = 11;
            this.LblPower.Text = "0W";
            // 
            // LblCurrent
            // 
            this.LblCurrent.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LblCurrent.AutoSize = true;
            this.LblCurrent.Location = new System.Drawing.Point(140, 31);
            this.LblCurrent.Name = "LblCurrent";
            this.LblCurrent.Size = new System.Drawing.Size(20, 13);
            this.LblCurrent.TabIndex = 10;
            this.LblCurrent.Text = "0A";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Voltage";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Current";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Power";
            // 
            // LblVoltage
            // 
            this.LblVoltage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LblVoltage.AutoSize = true;
            this.LblVoltage.Location = new System.Drawing.Point(140, 6);
            this.LblVoltage.Name = "LblVoltage";
            this.LblVoltage.Size = new System.Drawing.Size(20, 13);
            this.LblVoltage.TabIndex = 9;
            this.LblVoltage.Text = "0V";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Temperature";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.Controls.Add(this.LblAutomaticModeRes, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.LblAutomaticModeReq, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.LblBrightnessResp, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.LblLedStatusReq, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.LblBoardStatusReq, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.LblBoardStatusRes, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.LblBoardLedStatus, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.LblBoardBrightness, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.LblLedStatusResp, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.LblBrightnessReq, 1, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(9, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 159);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // LblAutomaticModeRes
            // 
            this.LblAutomaticModeRes.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LblAutomaticModeRes.AutoSize = true;
            this.LblAutomaticModeRes.Location = new System.Drawing.Point(152, 131);
            this.LblAutomaticModeRes.Name = "LblAutomaticModeRes";
            this.LblAutomaticModeRes.Size = new System.Drawing.Size(27, 13);
            this.LblAutomaticModeRes.TabIndex = 12;
            this.LblAutomaticModeRes.Text = "OFF";
            // 
            // LblAutomaticModeReq
            // 
            this.LblAutomaticModeReq.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LblAutomaticModeReq.AutoSize = true;
            this.LblAutomaticModeReq.Location = new System.Drawing.Point(85, 131);
            this.LblAutomaticModeReq.Name = "LblAutomaticModeReq";
            this.LblAutomaticModeReq.Size = new System.Drawing.Size(27, 13);
            this.LblAutomaticModeReq.TabIndex = 11;
            this.LblAutomaticModeReq.Text = "OFF";
            // 
            // LblBrightnessResp
            // 
            this.LblBrightnessResp.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LblBrightnessResp.AutoSize = true;
            this.LblBrightnessResp.Location = new System.Drawing.Point(155, 91);
            this.LblBrightnessResp.Name = "LblBrightnessResp";
            this.LblBrightnessResp.Size = new System.Drawing.Size(21, 13);
            this.LblBrightnessResp.TabIndex = 10;
            this.LblBrightnessResp.Text = "0%";
            // 
            // LblLedStatusReq
            // 
            this.LblLedStatusReq.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LblLedStatusReq.AutoSize = true;
            this.LblLedStatusReq.Location = new System.Drawing.Point(85, 52);
            this.LblLedStatusReq.Name = "LblLedStatusReq";
            this.LblLedStatusReq.Size = new System.Drawing.Size(27, 13);
            this.LblLedStatusReq.TabIndex = 6;
            this.LblLedStatusReq.Text = "OFF";
            // 
            // LblBoardStatusReq
            // 
            this.LblBoardStatusReq.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LblBoardStatusReq.AutoSize = true;
            this.LblBoardStatusReq.Location = new System.Drawing.Point(75, 13);
            this.LblBoardStatusReq.Name = "LblBoardStatusReq";
            this.LblBoardStatusReq.Size = new System.Drawing.Size(47, 13);
            this.LblBoardStatusReq.TabIndex = 0;
            this.LblBoardStatusReq.Text = "Request";
            // 
            // LblBoardStatusRes
            // 
            this.LblBoardStatusRes.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LblBoardStatusRes.AutoSize = true;
            this.LblBoardStatusRes.Location = new System.Drawing.Point(138, 13);
            this.LblBoardStatusRes.Name = "LblBoardStatusRes";
            this.LblBoardStatusRes.Size = new System.Drawing.Size(55, 13);
            this.LblBoardStatusRes.TabIndex = 1;
            this.LblBoardStatusRes.Text = "Response";
            // 
            // LblBoardLedStatus
            // 
            this.LblBoardLedStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LblBoardLedStatus.AutoSize = true;
            this.LblBoardLedStatus.Location = new System.Drawing.Point(14, 45);
            this.LblBoardLedStatus.Name = "LblBoardLedStatus";
            this.LblBoardLedStatus.Size = new System.Drawing.Size(37, 26);
            this.LblBoardLedStatus.TabIndex = 2;
            this.LblBoardLedStatus.Text = "LED Status";
            this.LblBoardLedStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblBoardBrightness
            // 
            this.LblBoardBrightness.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LblBoardBrightness.AutoSize = true;
            this.LblBoardBrightness.Location = new System.Drawing.Point(5, 91);
            this.LblBoardBrightness.Name = "LblBoardBrightness";
            this.LblBoardBrightness.Size = new System.Drawing.Size(56, 13);
            this.LblBoardBrightness.TabIndex = 3;
            this.LblBoardBrightness.Text = "Brightness";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 26);
            this.label1.TabIndex = 4;
            this.label1.Text = "Automatic Mode";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblLedStatusResp
            // 
            this.LblLedStatusResp.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LblLedStatusResp.AutoSize = true;
            this.LblLedStatusResp.Location = new System.Drawing.Point(152, 52);
            this.LblLedStatusResp.Name = "LblLedStatusResp";
            this.LblLedStatusResp.Size = new System.Drawing.Size(27, 13);
            this.LblLedStatusResp.TabIndex = 7;
            this.LblLedStatusResp.Text = "OFF";
            // 
            // LblBrightnessReq
            // 
            this.LblBrightnessReq.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LblBrightnessReq.AutoSize = true;
            this.LblBrightnessReq.Location = new System.Drawing.Point(88, 91);
            this.LblBrightnessReq.Name = "LblBrightnessReq";
            this.LblBrightnessReq.Size = new System.Drawing.Size(21, 13);
            this.LblBrightnessReq.TabIndex = 8;
            this.LblBrightnessReq.Text = "0%";
            // 
            // tabCtrlSerialInterface
            // 
            this.tabCtrlSerialInterface.Controls.Add(this.tabPageSerial0);
            this.tabCtrlSerialInterface.Controls.Add(this.tabPageSerial1);
            this.tabCtrlSerialInterface.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrlSerialInterface.Location = new System.Drawing.Point(3, 3);
            this.tabCtrlSerialInterface.Name = "tabCtrlSerialInterface";
            this.tabCtrlSerialInterface.SelectedIndex = 0;
            this.tabCtrlSerialInterface.Size = new System.Drawing.Size(243, 152);
            this.tabCtrlSerialInterface.TabIndex = 8;
            // 
            // tabPageSerial0
            // 
            this.tabPageSerial0.Controls.Add(this.grpSerialInterface1);
            this.tabPageSerial0.Location = new System.Drawing.Point(4, 22);
            this.tabPageSerial0.Name = "tabPageSerial0";
            this.tabPageSerial0.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSerial0.Size = new System.Drawing.Size(235, 126);
            this.tabPageSerial0.TabIndex = 0;
            this.tabPageSerial0.Text = "Serial 1";
            this.tabPageSerial0.UseVisualStyleBackColor = true;
            // 
            // grpSerialInterface1
            // 
            this.grpSerialInterface1.Controls.Add(this.BtnComPortInit0);
            this.grpSerialInterface1.Controls.Add(this.LblComPortStatus0);
            this.grpSerialInterface1.Controls.Add(this.BtnComPortStop0);
            this.grpSerialInterface1.Controls.Add(this.BtnComPortStart0);
            this.grpSerialInterface1.Controls.Add(this.ComboBoxSerialComPorts0);
            this.grpSerialInterface1.Controls.Add(this.LblComPort);
            this.grpSerialInterface1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSerialInterface1.Location = new System.Drawing.Point(3, 3);
            this.grpSerialInterface1.Name = "grpSerialInterface1";
            this.grpSerialInterface1.Size = new System.Drawing.Size(229, 120);
            this.grpSerialInterface1.TabIndex = 7;
            this.grpSerialInterface1.TabStop = false;
            this.grpSerialInterface1.Text = "Serial Interface 1";
            // 
            // BtnComPortInit0
            // 
            this.BtnComPortInit0.Location = new System.Drawing.Point(81, 80);
            this.BtnComPortInit0.Name = "BtnComPortInit0";
            this.BtnComPortInit0.Size = new System.Drawing.Size(121, 23);
            this.BtnComPortInit0.TabIndex = 8;
            this.BtnComPortInit0.Text = "Update COM Ports";
            this.BtnComPortInit0.UseVisualStyleBackColor = true;
            this.BtnComPortInit0.Click += new System.EventHandler(this.BtnComPortInit_Click);
            // 
            // LblComPortStatus0
            // 
            this.LblComPortStatus0.AutoSize = true;
            this.LblComPortStatus0.BackColor = System.Drawing.Color.Red;
            this.LblComPortStatus0.Location = new System.Drawing.Point(15, 55);
            this.LblComPortStatus0.Name = "LblComPortStatus0";
            this.LblComPortStatus0.Size = new System.Drawing.Size(37, 13);
            this.LblComPortStatus0.TabIndex = 6;
            this.LblComPortStatus0.Text = "Status";
            // 
            // BtnComPortStop0
            // 
            this.BtnComPortStop0.Location = new System.Drawing.Point(142, 50);
            this.BtnComPortStop0.Name = "BtnComPortStop0";
            this.BtnComPortStop0.Size = new System.Drawing.Size(60, 23);
            this.BtnComPortStop0.TabIndex = 5;
            this.BtnComPortStop0.Text = "Close";
            this.BtnComPortStop0.UseVisualStyleBackColor = true;
            this.BtnComPortStop0.Click += new System.EventHandler(this.BtnComPortStop_Click);
            // 
            // BtnComPortStart0
            // 
            this.BtnComPortStart0.Location = new System.Drawing.Point(81, 50);
            this.BtnComPortStart0.Name = "BtnComPortStart0";
            this.BtnComPortStart0.Size = new System.Drawing.Size(60, 23);
            this.BtnComPortStart0.TabIndex = 4;
            this.BtnComPortStart0.Text = "Open";
            this.BtnComPortStart0.UseVisualStyleBackColor = true;
            this.BtnComPortStart0.Click += new System.EventHandler(this.BtnComPortStart_Click);
            // 
            // ComboBoxSerialComPorts0
            // 
            this.ComboBoxSerialComPorts0.FormattingEnabled = true;
            this.ComboBoxSerialComPorts0.Location = new System.Drawing.Point(81, 23);
            this.ComboBoxSerialComPorts0.Name = "ComboBoxSerialComPorts0";
            this.ComboBoxSerialComPorts0.Size = new System.Drawing.Size(121, 21);
            this.ComboBoxSerialComPorts0.TabIndex = 3;
            // 
            // LblComPort
            // 
            this.LblComPort.AutoSize = true;
            this.LblComPort.Location = new System.Drawing.Point(6, 26);
            this.LblComPort.Name = "LblComPort";
            this.LblComPort.Size = new System.Drawing.Size(56, 13);
            this.LblComPort.TabIndex = 2;
            this.LblComPort.Text = "COM Port:";
            // 
            // tabPageSerial1
            // 
            this.tabPageSerial1.Controls.Add(this.groupBox1);
            this.tabPageSerial1.Location = new System.Drawing.Point(4, 22);
            this.tabPageSerial1.Name = "tabPageSerial1";
            this.tabPageSerial1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSerial1.Size = new System.Drawing.Size(235, 126);
            this.tabPageSerial1.TabIndex = 1;
            this.tabPageSerial1.Text = "Serial 2";
            this.tabPageSerial1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnComPortInit1);
            this.groupBox1.Controls.Add(this.LblComPortStatus1);
            this.groupBox1.Controls.Add(this.BtnComPortStop1);
            this.groupBox1.Controls.Add(this.BtnComPortStart1);
            this.groupBox1.Controls.Add(this.ComboBoxSerialComPorts1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(229, 120);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Serial Interface 2";
            // 
            // BtnComPortInit1
            // 
            this.BtnComPortInit1.Location = new System.Drawing.Point(81, 80);
            this.BtnComPortInit1.Name = "BtnComPortInit1";
            this.BtnComPortInit1.Size = new System.Drawing.Size(121, 23);
            this.BtnComPortInit1.TabIndex = 8;
            this.BtnComPortInit1.Text = "Update COM Ports";
            this.BtnComPortInit1.UseVisualStyleBackColor = true;
            this.BtnComPortInit1.Click += new System.EventHandler(this.BtnComPortInit_Click);
            // 
            // LblComPortStatus1
            // 
            this.LblComPortStatus1.AutoSize = true;
            this.LblComPortStatus1.BackColor = System.Drawing.Color.Red;
            this.LblComPortStatus1.Location = new System.Drawing.Point(15, 55);
            this.LblComPortStatus1.Name = "LblComPortStatus1";
            this.LblComPortStatus1.Size = new System.Drawing.Size(37, 13);
            this.LblComPortStatus1.TabIndex = 6;
            this.LblComPortStatus1.Text = "Status";
            // 
            // BtnComPortStop1
            // 
            this.BtnComPortStop1.Location = new System.Drawing.Point(142, 50);
            this.BtnComPortStop1.Name = "BtnComPortStop1";
            this.BtnComPortStop1.Size = new System.Drawing.Size(60, 23);
            this.BtnComPortStop1.TabIndex = 5;
            this.BtnComPortStop1.Text = "Close";
            this.BtnComPortStop1.UseVisualStyleBackColor = true;
            this.BtnComPortStop1.Click += new System.EventHandler(this.BtnComPortStop_Click);
            // 
            // BtnComPortStart1
            // 
            this.BtnComPortStart1.Location = new System.Drawing.Point(81, 50);
            this.BtnComPortStart1.Name = "BtnComPortStart1";
            this.BtnComPortStart1.Size = new System.Drawing.Size(60, 23);
            this.BtnComPortStart1.TabIndex = 4;
            this.BtnComPortStart1.Text = "Open";
            this.BtnComPortStart1.UseVisualStyleBackColor = true;
            this.BtnComPortStart1.Click += new System.EventHandler(this.BtnComPortStart_Click);
            // 
            // ComboBoxSerialComPorts1
            // 
            this.ComboBoxSerialComPorts1.FormattingEnabled = true;
            this.ComboBoxSerialComPorts1.Location = new System.Drawing.Point(81, 23);
            this.ComboBoxSerialComPorts1.Name = "ComboBoxSerialComPorts1";
            this.ComboBoxSerialComPorts1.Size = new System.Drawing.Size(121, 21);
            this.ComboBoxSerialComPorts1.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "COM Port:";
            // 
            // serialPort2
            // 
            this.serialPort2.BaudRate = 115200;
            this.serialPort2.ReadBufferSize = 2048;
            this.serialPort2.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SerialPort2_DataReceived);
            // 
            // ChkBoxUpdateListView
            // 
            this.ChkBoxUpdateListView.AutoSize = true;
            this.ChkBoxUpdateListView.Checked = true;
            this.ChkBoxUpdateListView.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkBoxUpdateListView.Location = new System.Drawing.Point(9, 301);
            this.ChkBoxUpdateListView.Name = "ChkBoxUpdateListView";
            this.ChkBoxUpdateListView.Size = new System.Drawing.Size(103, 17);
            this.ChkBoxUpdateListView.TabIndex = 8;
            this.ChkBoxUpdateListView.Text = "Update ListView";
            this.ChkBoxUpdateListView.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1313, 686);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Name = "Form1";
            this.Text = "Message Logger";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.grpBoardStatus.ResumeLayout(false);
            this.grpBoardStatus.PerformLayout();
            this.GrpBoxLogging.ResumeLayout(false);
            this.GrpBoxLogging.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabCtrlSerialInterface.ResumeLayout(false);
            this.tabPageSerial0.ResumeLayout(false);
            this.grpSerialInterface1.ResumeLayout(false);
            this.grpSerialInterface1.PerformLayout();
            this.tabPageSerial1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader LVIndex;
        private System.Windows.Forms.ColumnHeader LVTime;
        private System.Windows.Forms.ColumnHeader LVMessage;
        private System.Windows.Forms.ColumnHeader LVDestination;
        private System.Windows.Forms.ColumnHeader LVSource;
        private System.Windows.Forms.ColumnHeader LVKind;
        private System.Windows.Forms.ColumnHeader LVPayload;
        private System.Windows.Forms.ColumnHeader LVObject;
        private System.Windows.Forms.ColumnHeader LVCommand;
        private System.Windows.Forms.ColumnHeader LVInterpretation;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.GroupBox grpBoardStatus;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label LblTemperature;
        private System.Windows.Forms.Label LblPower;
        private System.Windows.Forms.Label LblCurrent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label LblVoltage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label LblAutomaticModeRes;
        private System.Windows.Forms.Label LblAutomaticModeReq;
        private System.Windows.Forms.Label LblBrightnessResp;
        private System.Windows.Forms.Label LblLedStatusReq;
        private System.Windows.Forms.Label LblBoardStatusReq;
        private System.Windows.Forms.Label LblBoardStatusRes;
        private System.Windows.Forms.Label LblBoardLedStatus;
        private System.Windows.Forms.Label LblBoardBrightness;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LblLedStatusResp;
        private System.Windows.Forms.Label LblBrightnessReq;
        private System.Windows.Forms.RichTextBox RichTextBoxSerialData;
        private System.Windows.Forms.Button BtnResizeListView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnClearListView;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.TabControl tabCtrlSerialInterface;
        private System.Windows.Forms.TabPage tabPageSerial0;
        private System.Windows.Forms.GroupBox grpSerialInterface1;
        private System.Windows.Forms.Button BtnComPortInit0;
        private System.Windows.Forms.Label LblComPortStatus0;
        private System.Windows.Forms.Button BtnComPortStop0;
        private System.Windows.Forms.Button BtnComPortStart0;
        private System.Windows.Forms.ComboBox ComboBoxSerialComPorts0;
        private System.Windows.Forms.Label LblComPort;
        private System.Windows.Forms.TabPage tabPageSerial1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnComPortInit1;
        private System.Windows.Forms.Label LblComPortStatus1;
        private System.Windows.Forms.Button BtnComPortStop1;
        private System.Windows.Forms.Button BtnComPortStart1;
        private System.Windows.Forms.ComboBox ComboBoxSerialComPorts1;
        private System.Windows.Forms.Label label8;
        private System.IO.Ports.SerialPort serialPort2;
        private System.Windows.Forms.GroupBox GrpBoxLogging;
        private System.Windows.Forms.CheckBox ChkBoxLogListView;
        private System.Windows.Forms.CheckBox ChkBoxLogMsgBuffer;
        private System.Windows.Forms.CheckBox ChkBoxLogRecData;
        private System.Windows.Forms.CheckBox ChkBoxUpdateListView;
    }
}

