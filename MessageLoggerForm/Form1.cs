using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Collections.Specialized;
using System.Threading;
using System.Management;
using Microsoft.Win32;

using MessageLoggerForm.COM;
using MessageLoggerForm.Serial;
using MessageLoggerForm.Data;

namespace MessageLoggerForm
{
    public partial class Form1 : Form
    {
        private class SerialCom
        {
            public Class_COM cCOM_Port;
            public Class_Serial cSerial;
            public BackgroundWorker bgw;

            public SerialCom()
            {
                cSerial = new Class_Serial();
                cSerial.OnMessageReceived += new Class_Serial.MessageReceived(MessageReceivedHandler);
                cCOM_Port = new Class_COM();

                
            }

            public SerialCom(Class_COM cCom)
            {
                cSerial = new Class_Serial();
                cSerial.OnMessageReceived += new Class_Serial.MessageReceived(MessageReceivedHandler);
                cCOM_Port = cCom;
            }
        }


        /****************************************************************************************************
        * Variables
        ****************************************************************************************************/
        public const byte ucUsedLables = 4;

        private List<SerialCom> _lstSerialCom;
        private Class_Data.cData _cData;

        private BackgroundWorker[] backgroundWorkersArr;
        public static Class_Lables[] _labels;
        const bool bAppendInFile = true;

        /****************************************************************************************************
        * @brief: Method to enable the double buffered option
        * @param: control - The control object which should activate the double buffered function
        * @return: none
        ****************************************************************************************************/
        public static void SetDoubleBufferd(Control control)
        {
            //Set instance non-public property with name "DoubleBufferd" to true
            typeof(Control).InvokeMember("DoubleBuffered",
                                            System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                                            null,
                                            control,
                                            new object[] { true });
        }


        /****************************************************************************************************
        * Functions of the FORM
        ****************************************************************************************************/
        public Form1()
        {
            InitializeComponent();

            /* Initialize serial handling */
            _lstSerialCom = new List<SerialCom>();
            _cData = new Class_Data.cData();


            _cData.lv.Dock = DockStyle.Fill;
            _cData.lv.GridLines = true;
            tabPage1.Controls.Add(_cData.lv);

            //Init combo boxes with available COM-Ports
            BtnComPortInit_Click(default, default);

            //Activate the double buffered option in the list view
            SetDoubleBufferd(_cData.lv);

            //Initialize lables class
            _labels = new Class_Lables[ucUsedLables];

            for(byte ucIdx = 0; ucIdx < ucUsedLables; ucIdx++)
            {
                _labels[ucIdx] = new Class_Lables();
            }
        }

        /// <summary>
        /// Event handler for a valid received message 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private static void MessageReceivedHandler(object sender, Class_Serial.MessageReceivedArgs args)
        {
            //Form1 frm = new Form1();
             Program.form1.HandleNewMessage((Class_Serial)sender, args.messagesQueued);
        }

        /****************************************************************************************************
        * @brief: Creates background worker threads 
        * @param: none
        * @return: none
        ****************************************************************************************************/
        void InitializeBackgroundWorker(SerialCom serialCom)
        {
            /* Create new background worker thread */
            serialCom.bgw = new BackgroundWorker();

            /* Link them to the handler */
            serialCom.bgw.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            serialCom.bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_Finished);
            serialCom.bgw.WorkerSupportsCancellation = true;

            /* Start the background worker */
            serialCom.bgw.RunWorkerAsync(serialCom);
        }

        /// <summary>
        /// Gets the string from the selected combobox.
        /// </summary>
        /// <param name="btn"> The button which was pressed </param>
        /// <returns></returns>
        private string GetPortName(Button btn) => btn.Name switch
        {
            "BtnComPortStart0" => ComboBoxSerialComPorts0.Text,
            "BtnComPortStart1" => ComboBoxSerialComPorts1.Text,
            _ => throw new ArgumentException("Invalid COM-Port button text")
        };

        private void SetComPortStatusLblColor(Button btn, Color clr)
        {
            if (btn.Name == "BtnComPortStart0")
                LblComPortStatus0.BackColor = clr;
            else if (btn.Name == "BtnComPortStart1")
                LblComPortStatus0.BackColor = clr;
            else
                throw new ArgumentException("Invalid COM-Port button text");
        }

        
        /****************************************************************************************************
          * @brief: Event handler for the "COM-Port-START" button. Checks if a COM-Port is selected and opens
          *         it with the predefined settings.
          * @param: none
          * @return: none
          ****************************************************************************************************/
        private void BtnComPortStart_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            //Create new COM-Port object
            Class_COM cComPort = new Class_COM();

            try
            {
                //Open the COM-Port with the selected name
                cComPort.OpenPort(GetPortName(btn));

                //Change color of the label
                SetComPortStatusLblColor(btn, Color.Green);

                //Add a new serial object to COM-Port to list
                SerialCom serialCom = new SerialCom(cComPort);
                InitializeBackgroundWorker(serialCom);

                _lstSerialCom.Add(serialCom);
            }
            catch(InvalidOperationException)
            {
                MessageBox.Show("COM Port has to be closed first");
            }
        }



        /****************************************************************************************************
         * @brief: Event handler for the "COM-Port-STOP" button. Closes the serial port.
         * @param: none
         * @return: none
         ****************************************************************************************************/
        private void BtnComPortStop_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            
            try
            {
                //Find the serial COM class in the serial-com-List
                SerialCom cSerialCom = _lstSerialCom.Find(x => x.cCOM_Port.portName == GetPortName(btn));

                //Close the COM-Port
                cSerialCom.cCOM_Port.ClosePort();

                //Change the COM-Port color
                SetComPortStatusLblColor(btn, Color.Red);

                //Delete this entry from the COM-Port list
                _lstSerialCom.Remove(cSerialCom);
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("COM Port has to be opened first");
            }
        }



        /****************************************************************************************************
         * @brief: Adds the text box with the serial data in hex-format
         * @param: none
         * @return: none
         ****************************************************************************************************/
        private void AddTextToSerialDataTextBox(byte[] pucBuffer, int ucBufferCnt)
        {
            //Put the time index at the first place
            DateTime sDate = DateTime.Now; 
            RichTextBoxSerialData.Text += sDate.TimeOfDay.ToString() + " ";

            //Put the whole buffer data into the text box
            for (byte Idx = 0; Idx < ucBufferCnt; Idx++)
            {
                string sBufferEntry = pucBuffer[Idx].ToString("X2");
                
                RichTextBoxSerialData.Text += sBufferEntry + " ";
            }
            RichTextBoxSerialData.Text += "\n";
        }


        /****************************************************************************************************
         * @brief: Re-sizes the columns of the list view
         * @param: none
         * @return: none
         ****************************************************************************************************/
        private void ResizeListViewColumns(object sender, EventArgs e)
        {
            foreach (ColumnHeader column in _cData.lv.Columns)
            {
                column.Width = -2;
            }
        }



        /// <summary>
        /// Fills an array for the list view with the interprated data.
        /// </summary>
        /// <param name="sMsgFrame">sMsgFrame - The message frame which shall be interprated and put into the list view</param>
        private void FillListView(MsgStructure.tsMessageFrame sMsgFrame)
        {
            //Use cdata to fill the data table
            _cData.FillRow(DateTime.Now,
                sMsgFrame.aucData,
                Class_Interpreter.GetAddress(sMsgFrame.ucDestAddress),
                Class_Interpreter.GetAddress(sMsgFrame.ucSourceAddress),
                Class_Interpreter.GetType((MsgEnum.teMessageType)sMsgFrame.ucMsgType),
                sMsgFrame.ucQueryID,
                Class_Interpreter.GetID((MsgEnum.teMessageId)sMsgFrame.ucMsgId),
                Class_Interpreter.GetCommand((MsgEnum.teMessageCmd)sMsgFrame.ucCommand),
                Class_Interpreter.GetPayload(sMsgFrame));

            /* Check if list view shall be updated */
            if (ChkBoxUpdateListView.Checked == true)
            {
                if (ChkBoxShowACK.Checked == false 
                    && sMsgFrame.ucMsgType == ((int)MsgEnum.teMessageType.eTypeAck))
                {
                    //Do nothing
                }
                else
                {
                   /* Call per invoke to put the item to the list view */
                   this.Invoke((MethodInvoker)delegate
                   {
                       for (byte ucLblIdx = 0; ucLblIdx < ucUsedLables; ucLblIdx++)
                       {
                           LblLedStatusReq.Text = _labels[ucLblIdx].szReqLed; ;
                           LblLedStatusResp.Text = _labels[ucLblIdx].szResLed;
                           LblBrightnessReq.Text = _labels[ucLblIdx].szBrightnessReq;
                           LblBrightnessResp.Text = _labels[ucLblIdx].szBrightnessRes;
                           LblAutomaticModeReq.Text = _labels[ucLblIdx].szAutoReq;
                           LblAutomaticModeRes.Text = _labels[ucLblIdx].szAutoRes;
            
                           switch (ucLblIdx)
                           {
                                case 0:
                                {
                                    LblVoltage_0.Text = _labels[ucLblIdx].szVoltage;
                                    LblCurrent_0.Text = _labels[ucLblIdx].szCurrent;
                                    LblPower_0.Text = _labels[ucLblIdx].szPower;
                                    LblTemperature_0.Text = _labels[ucLblIdx].szTemp;
                                    break;
                                }
            
                               case 1:
                                {
                                    LblVoltage_1.Text = _labels[ucLblIdx].szVoltage;
                                    LblCurrent_1.Text = _labels[ucLblIdx].szCurrent;
                                    LblPower_1.Text = _labels[ucLblIdx].szPower;
                                    LblTemperature_1.Text = _labels[ucLblIdx].szTemp;
                                    break;
                                }
            
                               case 2:
                                {
                                    LblVoltage_2.Text = _labels[ucLblIdx].szVoltage;
                                    LblCurrent_2.Text = _labels[ucLblIdx].szCurrent;
                                    LblPower_2.Text = _labels[ucLblIdx].szPower;
                                    LblTemperature_2.Text = _labels[ucLblIdx].szTemp;
                                    break;
                                }
                               default:
                                   break;
                           }
                       }

                       _cData.AddNewestRowToListView();
                   });
                }
            }

            /* Save list view entry in text file */
            if(ChkBoxLogListView.Checked == true)
            {
                //TODO: Check for correct conversion
                string localString = _cData.dt.Rows[0].ToString();
                //foreach (string sArrEntry in asMsgArray)
                //{
                //    localString += sArrEntry + " ";
                //}

                /* Save received data into text file */
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\kraemere\Desktop\ListView.txt", bAppendInFile))
                {
                    file.WriteLine(localString);
                }
            }
        }

        /// <summary>
        /// Checks if serial data has been received. Retrieves it, saves the data into
        /// a log-file when checkbox is checked and puts it into the serial class
        /// </summary>
        private void ProcessSerialData(SerialCom serialCom)
        {
            //Get the received bytes
            var byteStream = serialCom.cCOM_Port.ReadReceivedBytes();

            //Check if list contains any data
            if (byteStream.Count > 0)
            {
                // Save received data into text file
                if (ChkBoxLogRecData.Checked == true)
                {
                    string Line = $"{DateTime.Now.TimeOfDay.ToString()} - Port: {serialCom.cCOM_Port.portName} - Put {byteStream.Count} Bytes | ";

                    foreach (byte data in byteStream)
                    {
                        Line += data.ToString("X2") + " ";
                    }
                    Line += Environment.NewLine;

                    /* Save received data into text file */
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\kraemere\Desktop\Lines.txt", bAppendInFile))
                    {
                        file.WriteLine(Line);
                    }
                }

                //Put the list into the serial class to convert to correct message frames
                serialCom.cSerial.DataReceived(byteStream);
            }
        }

        /// <summary>
        /// Gets the message frame from the serial class and puts it into the list view method
        /// </summary>
        /// <param name="cSerial"> The serial module which has generated the event </param>
        /// <param name="queuedMessages"> The amount of messages which can be retrieved </param>
        private void HandleNewMessage(Class_Serial cSerial, int queuedMessages)
        {
            for(int msgIdx = 0; msgIdx < queuedMessages; msgIdx++)
            {
                //Get the message frame
                MsgStructure.tsMessageFrame msgFrame = cSerial.GetNextMessageFrame();

                //Update list view with new message
                FillListView(msgFrame);
            }
        }

        /****************************************************************************************************
         * @brief: Checks for the available COM ports
         * @param: 
         * @return: none
         ****************************************************************************************************/
        private void BtnComPortInit_Click(object sender, EventArgs e)
        {
            //Init combo box
            var lstComPorts = Class_Helper.ComPorts.GetAvailablePorts();
            foreach (string szComPort in lstComPorts)
            {
                if (ComboBoxSerialComPorts0.Items.Contains(szComPort) == false)
                    ComboBoxSerialComPorts0.Items.Add(szComPort);
                if (ComboBoxSerialComPorts1.Items.Contains(szComPort) == false)
                    ComboBoxSerialComPorts1.Items.Add(szComPort);
            }
        }

        /****************************************************************************************************
          * @brief: Clear the list view
          * @param: 
          * @return: none
          ****************************************************************************************************/
        private void BtnClearListView_Click(object sender, EventArgs e)
        {
            _cData.lv.Items.Clear();
        }

        /****************************************************************************************************
        * @brief: Background worker handling callback. This method is the start of each background worker thread
        * @param: DoWorkEventArgs - The argument which is set with the initialization of the background worker
        * @return: none
        ****************************************************************************************************/
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //BackgroundWorker bgw = sender as BackgroundWorker;

                while (true)
                {
                    ProcessSerialData((SerialCom)e.Argument);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
        }
        
        /****************************************************************************************************
        * @brief: Background worker handling callback when the background worker finished its work
        * @param: 
        * @return: none
        ****************************************************************************************************/
        private void backgroundWorker_Finished(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundWorker bgw = (BackgroundWorker)sender;
            bgw.Dispose();
        }


        /// <summary>
        /// Sends a test message on the bus. Only for tes purposes!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTestSend_Click(object sender, EventArgs e)
        {
            //Create a message frame first (Frame + Payload structure)
            MsgStructure.tMsgRequestOutputState sMsg = new MsgStructure.tMsgRequestOutputState();
            sMsg.ucBrightness = 55;
            sMsg.ucAutomaticModeActive = 1;
            sMsg.ucBurnTime = 20;
            sMsg.ucInitMenuActive = 0;
            sMsg.ucInitMenuActiveInv = 1;
            sMsg.ucLedStatus = 1;
            sMsg.ucMotionDetectionOnOff = 1;
            sMsg.ucNightModeOnOff = 0;
            sMsg.ucOutputIndex = 2;

            MsgStructure.tsMessageFrame sMsgFrame = new MsgStructure.tsMessageFrame();
            sMsgFrame.aucData = Class_Helper.Serializer.SerializeMarsh(sMsg);
            sMsgFrame.ucMsgId = (byte)MsgEnum.teMessageId.eMsgRequestOutputStatus;
            sMsgFrame.ucCommand = ((byte)MsgEnum.teMessageCmd.eCmdSet);
            sMsgFrame.ucDestAddress = MsgEnum.ADDRESS_SLAVE1;
            sMsgFrame.ucMsgType = (byte)MsgEnum.teMessageType.eTypeRequest;
            sMsgFrame.ucPayloadLen = (byte)sMsgFrame.aucData.Length;
            sMsgFrame.ucPreamble = MsgEnum.PREAMBE;
            sMsgFrame.ucQueryID = 1;
            sMsgFrame.ucSourceAddress = MsgEnum.ADDRESS_MASTER;
            sMsgFrame.ulCrc32 = 0xFFFFFFFF;

            //Create bus frame
            Queue<byte> queByteStream = _lstSerialCom[tabCtrlSerialInterface.SelectedIndex].cSerial.CreateBusMessageFrame(sMsgFrame);

            //Send over bus
            _lstSerialCom[tabCtrlSerialInterface.SelectedIndex].cCOM_Port.WriteBytes(queByteStream.ToArray());
        }
    }
}
