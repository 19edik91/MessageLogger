namespace MessageLoggerForm
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
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.grpBoardStatus = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.LblBrightnessReq = new System.Windows.Forms.Label();
            this.LblLedStatusResp = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LblBoardBrightness = new System.Windows.Forms.Label();
            this.LblBoardLedStatus = new System.Windows.Forms.Label();
            this.LblBoardStatusRes = new System.Windows.Forms.Label();
            this.LblBoardStatusReq = new System.Windows.Forms.Label();
            this.LblLedStatusReq = new System.Windows.Forms.Label();
            this.LblBrightnessResp = new System.Windows.Forms.Label();
            this.LblAutomaticModeReq = new System.Windows.Forms.Label();
            this.LblAutomaticModeRes = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.LblVoltage = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LblCurrent = new System.Windows.Forms.Label();
            this.LblPower = new System.Windows.Forms.Label();
            this.LblTemperature = new System.Windows.Forms.Label();
            this.grpSerialInterface = new System.Windows.Forms.GroupBox();
            this.LblComPort = new System.Windows.Forms.Label();
            this.ComboBoxSerialComPorts = new System.Windows.Forms.ComboBox();
            this.BtnComPortStart = new System.Windows.Forms.Button();
            this.BtnComPortStop = new System.Windows.Forms.Button();
            this.LblComPortStatus = new System.Windows.Forms.Label();
            this.LblBytesToRead = new System.Windows.Forms.Label();
            this.RichTextBoxSerialData = new System.Windows.Forms.RichTextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.grpBoardStatus.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.grpSerialInterface.SuspendLayout();
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
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(3, 3);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(926, 648);
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
            this.tabControl1.Size = new System.Drawing.Size(940, 680);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(932, 654);
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
            this.tabPage2.Size = new System.Drawing.Size(932, 654);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Serial Data";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 38400;
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
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1183, 686);
            this.tableLayoutPanel3.TabIndex = 6;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.grpBoardStatus, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.grpSerialInterface, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(949, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(231, 680);
            this.tableLayoutPanel4.TabIndex = 8;
            // 
            // grpBoardStatus
            // 
            this.grpBoardStatus.Controls.Add(this.tableLayoutPanel2);
            this.grpBoardStatus.Controls.Add(this.tableLayoutPanel1);
            this.grpBoardStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBoardStatus.Location = new System.Drawing.Point(3, 108);
            this.grpBoardStatus.Name = "grpBoardStatus";
            this.grpBoardStatus.Size = new System.Drawing.Size(225, 569);
            this.grpBoardStatus.TabIndex = 5;
            this.grpBoardStatus.TabStop = false;
            this.grpBoardStatus.Text = "Board Status";
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(9, 195);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel2.TabIndex = 1;
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
            // grpSerialInterface
            // 
            this.grpSerialInterface.Controls.Add(this.LblBytesToRead);
            this.grpSerialInterface.Controls.Add(this.LblComPortStatus);
            this.grpSerialInterface.Controls.Add(this.BtnComPortStop);
            this.grpSerialInterface.Controls.Add(this.BtnComPortStart);
            this.grpSerialInterface.Controls.Add(this.ComboBoxSerialComPorts);
            this.grpSerialInterface.Controls.Add(this.LblComPort);
            this.grpSerialInterface.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSerialInterface.Location = new System.Drawing.Point(3, 3);
            this.grpSerialInterface.Name = "grpSerialInterface";
            this.grpSerialInterface.Size = new System.Drawing.Size(225, 99);
            this.grpSerialInterface.TabIndex = 4;
            this.grpSerialInterface.TabStop = false;
            this.grpSerialInterface.Text = "Serial Interface";
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
            // ComboBoxSerialComPorts
            // 
            this.ComboBoxSerialComPorts.FormattingEnabled = true;
            this.ComboBoxSerialComPorts.Location = new System.Drawing.Point(81, 23);
            this.ComboBoxSerialComPorts.Name = "ComboBoxSerialComPorts";
            this.ComboBoxSerialComPorts.Size = new System.Drawing.Size(121, 21);
            this.ComboBoxSerialComPorts.TabIndex = 3;
            // 
            // BtnComPortStart
            // 
            this.BtnComPortStart.Location = new System.Drawing.Point(81, 50);
            this.BtnComPortStart.Name = "BtnComPortStart";
            this.BtnComPortStart.Size = new System.Drawing.Size(60, 23);
            this.BtnComPortStart.TabIndex = 4;
            this.BtnComPortStart.Text = "Open";
            this.BtnComPortStart.UseVisualStyleBackColor = true;
            this.BtnComPortStart.Click += new System.EventHandler(this.BtnComPortStart_Click);
            // 
            // BtnComPortStop
            // 
            this.BtnComPortStop.Location = new System.Drawing.Point(142, 50);
            this.BtnComPortStop.Name = "BtnComPortStop";
            this.BtnComPortStop.Size = new System.Drawing.Size(60, 23);
            this.BtnComPortStop.TabIndex = 5;
            this.BtnComPortStop.Text = "Close";
            this.BtnComPortStop.UseVisualStyleBackColor = true;
            this.BtnComPortStop.Click += new System.EventHandler(this.BtnComPortStop_Click);
            // 
            // LblComPortStatus
            // 
            this.LblComPortStatus.AutoSize = true;
            this.LblComPortStatus.BackColor = System.Drawing.Color.Red;
            this.LblComPortStatus.Location = new System.Drawing.Point(15, 55);
            this.LblComPortStatus.Name = "LblComPortStatus";
            this.LblComPortStatus.Size = new System.Drawing.Size(37, 13);
            this.LblComPortStatus.TabIndex = 6;
            this.LblComPortStatus.Text = "Status";
            // 
            // LblBytesToRead
            // 
            this.LblBytesToRead.AutoSize = true;
            this.LblBytesToRead.Location = new System.Drawing.Point(18, 75);
            this.LblBytesToRead.Name = "LblBytesToRead";
            this.LblBytesToRead.Size = new System.Drawing.Size(13, 13);
            this.LblBytesToRead.TabIndex = 7;
            this.LblBytesToRead.Text = "0";
            // 
            // RichTextBoxSerialData
            // 
            this.RichTextBoxSerialData.Location = new System.Drawing.Point(6, 7);
            this.RichTextBoxSerialData.Name = "RichTextBoxSerialData";
            this.RichTextBoxSerialData.Size = new System.Drawing.Size(920, 641);
            this.RichTextBoxSerialData.TabIndex = 0;
            this.RichTextBoxSerialData.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 686);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Name = "Form1";
            this.Text = "Message Logger";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.grpBoardStatus.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.grpSerialInterface.ResumeLayout(false);
            this.grpSerialInterface.PerformLayout();
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
        private System.IO.Ports.SerialPort serialPort1;
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
        private System.Windows.Forms.GroupBox grpSerialInterface;
        private System.Windows.Forms.Label LblBytesToRead;
        private System.Windows.Forms.Label LblComPortStatus;
        private System.Windows.Forms.Button BtnComPortStop;
        private System.Windows.Forms.Button BtnComPortStart;
        private System.Windows.Forms.ComboBox ComboBoxSerialComPorts;
        private System.Windows.Forms.Label LblComPort;
        private System.Windows.Forms.RichTextBox RichTextBoxSerialData;
    }
}

