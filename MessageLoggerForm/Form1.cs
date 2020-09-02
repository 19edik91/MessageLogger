﻿using System;
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

namespace MessageLoggerForm
{
    public partial class Form1 : Form
    {
        /****************************************************************************************************
        * C-DLL functions
        ****************************************************************************************************/
        [DllImport("MessageLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SumVariables(int a, int b);

        [DllImport("MessageLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool MsgLib_GetMessageFrame(out Class_Message.tsMessageFrame psMessageFrame, byte ucBufferIdx);

        [DllImport("MessageLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool MsgLib_PutDataInBuffer(byte[] pucBuffer, byte ucSize, byte ucBufferIdx);

        [DllImport("MessageLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void InitModule();

        [DllImport("MessageLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void MsgLib_ReadBuffer(IntPtr pucBuffer, byte ucSize, byte ucBufferIdx, bool bUpdateBuffer);

        [DllImport("MessageLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte MsgLib_GetWrittenBufferSize(byte ucBufferIdx);

        [DllImport("MessageLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void MsgLib_GetFifoBufferHandlerData(ref UInt16 puiFreeSize, ref UInt16 puiMaxSize, ref UInt16 puiPutIdx, ref UInt16 puiGetIdx, byte ucBufferIdx);

        [DllImport("MessageLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void MsgLib_GetMessageBufferHandlerData(ref UInt16 puiMsgCnt, ref UInt16 puiMsgPutIdx, ref UInt16 puiMsgGetIdx, byte ucBufferIdx);

        [DllImport("MessageLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void MsgLib_GetMessageBufferMessageData(ref UInt16 puiMsgByteCnt, ref UInt16 puiMsgStartIdx, ref UInt16 puiMsgSize, ref UInt16 puiMsgSavedStatus, ref UInt16 puiMsgRespondRec, byte ucBufferIdx, byte ucMsgIndex);


        /****************************************************************************************************
        * Variables
        ****************************************************************************************************/
        const UInt16 uiBaudRate = 38400;
        //const int uiBaudRate = 115200;

        //Messages
        private UInt32 ulMsgIdxCnt = 0;

        const int siTryToRead = 255;
        private byte[] aucBuffer = new byte[siTryToRead];
        private int BufferCnt = 0;
        private string MsgFrame;

        delegate void AddListItem(byte ucPortIdx);
        AddListItem _serialDelegate;

        Queue<Byte>[] _receivedSerialData;

        Mutex[] _mutexSerial;
        Mutex _mutexMessage;

        private BackgroundWorker[] backgroundWorkersArr;

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
            _serialDelegate = new AddListItem(ProcessSerialData);
            _receivedSerialData = new Queue<byte>[2];
            _receivedSerialData[0] = new Queue<byte>();
            _receivedSerialData[1] = new Queue<byte>();
            _mutexSerial = new Mutex[2];
            _mutexSerial[0] = new Mutex();
            _mutexSerial[1] = new Mutex();
            _mutexMessage = new Mutex();

            //Init combo box 
            GetAvailablePorts();

            InitModule();

            //Activate the double buffered option in the list view
            SetDoubleBufferd(listView1);

            //Initialize Background worker
            InitializeBackgroundWorker();
            timer1.Enabled = true;
        }

        /****************************************************************************************************
        * @brief: Creates background worker threads 
        * @param: none
        * @return: none
        ****************************************************************************************************/
        void InitializeBackgroundWorker()
        {
            /* Initializes background worker. At first only one */
            byte ucBgW_Count = 2;

            backgroundWorkersArr = new BackgroundWorker[ucBgW_Count];

            for(byte ucBgW_Idx = 0; ucBgW_Idx < ucBgW_Count; ucBgW_Idx++)
            {
                /* Create new background worker thread */
                backgroundWorkersArr[ucBgW_Idx] = new BackgroundWorker();

                /* Link them to the handler */
                backgroundWorkersArr[ucBgW_Idx].DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
                backgroundWorkersArr[ucBgW_Idx].RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_Finished);

                backgroundWorkersArr[ucBgW_Idx].WorkerSupportsCancellation = true;

                /* Start the background worker */
                backgroundWorkersArr[ucBgW_Idx].RunWorkerAsync(ucBgW_Idx);
            }
        }


        /****************************************************************************************************
        * @brief: Reads all available ports and put them into the combo-box.
        * @param: none
        * @return: none
        ****************************************************************************************************/
        void GetAvailablePorts()
        {
            String[] sPorts = SerialPort.GetPortNames();

            /* Check if comport is already in the list */
            foreach(string sPort in sPorts)
            {
                if(ComboBoxSerialComPorts0.Items.Contains(sPort) == false)
                {
                    ComboBoxSerialComPorts0.Items.Add(sPort);
                }

                if (ComboBoxSerialComPorts1.Items.Contains(sPort) == false)
                {
                    ComboBoxSerialComPorts1.Items.Add(sPort);
                }
            }            
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
            SerialPort sp = null;
            string portname = "";

            if (Convert.ToBoolean(btn.Name == "BtnComPortStart0"))
            {
                sp = serialPort1;
                portname = ComboBoxSerialComPorts0.Text;
            }
            else if(Convert.ToBoolean(btn.Name == "BtnComPortStart1"))
            {
                sp = serialPort2;
                portname = ComboBoxSerialComPorts1.Text;
            }
            

            try
            {
                if(ComboBoxSerialComPorts0.Text.Length != 0)
                {
                    //Set port name to the Combo box name
                    sp.PortName = portname;
                    sp.BaudRate = uiBaudRate;
                    sp.ReadTimeout = 1000;
                    sp.WriteTimeout = 1000;
                    sp.Parity = Parity.None;
                    sp.DataBits = 8;
                    sp.StopBits = StopBits.One;
                    sp.Handshake = Handshake.None;
//                    sp.ReadBufferSize = 14;
                    sp.ReceivedBytesThreshold = 4;
                    sp.Open();

                    if (Convert.ToBoolean(btn.Name == "BtnComPortStart0"))
                        LblComPortStatus0.BackColor = Color.Green;
                    else if (Convert.ToBoolean(btn.Name == "BtnComPortStart1"))
                        LblComPortStatus1.BackColor = Color.Green;

                }
                else
                {
                    MessageBox.Show("No COM-Port selected! ");
                }
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
                if (Convert.ToBoolean(btn.Name == "BtnComPortStop0"))
                {
                    serialPort1.Close();
                    serialPort1.Dispose();
                    LblComPortStatus0.BackColor = Color.Red;
                }
                else if (Convert.ToBoolean(btn.Name == "BtnComPortStop1"))
                {
                    serialPort2.Close();
                    serialPort2.Dispose();
                    LblComPortStatus1.BackColor = Color.Red;
                }
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
            foreach (ColumnHeader column in listView1.Columns)
            {
                column.Width = -2;
            }
        }


        /****************************************************************************************************
         * @brief: Function to put a structure into a byte array
         * @param: obj - Object structure
         * @return: byte[] - Byte array of the structure
         ****************************************************************************************************/
        byte[] StructureToByteArray(object obj)
        {
            int len = Marshal.SizeOf(obj);

            byte[] arr = new byte[len];

            IntPtr ptr = Marshal.AllocHGlobal(len);

            Marshal.StructureToPtr(obj, ptr, true);

            Marshal.Copy(ptr, arr, 0, len);

            Marshal.FreeHGlobal(ptr);

            return arr;
        }

        /****************************************************************************************************
         * @brief: Fills an array for the list view with the interprated data.
         * @param: sMsgFrame - The message frame which shall be interprated and put into the list view
         * @return: none
         ****************************************************************************************************/
        private unsafe void FillListView(Class_Message.tsMessageFrame sMsgFrame)
        {
            /* Create string array with the count of list view colums */
            string[] asMsgArray = new string[listView1.Columns.Count];

            DateTime sDate = DateTime.Now;
            Class_Interpreter Interpreter = new Class_Interpreter();
            
            /* Add items to list view */
            asMsgArray[0] = this.ulMsgIdxCnt.ToString();
            ++this.ulMsgIdxCnt;

            asMsgArray[1] = sDate.TimeOfDay.ToString();

            /* Cast message structure to bytes */
            byte[] aucMsgInBytes = StructureToByteArray(sMsgFrame);
            for (byte ucBuffIdx = 0; ucBuffIdx < aucMsgInBytes.Count(); ucBuffIdx++)
            {
                asMsgArray[2] += aucMsgInBytes[ucBuffIdx].ToString("X2") + " ";
            }

            //asMsgArray[3] = Interpreter.InteprateAddress(sMsgFrame.sHeader.ucDestAddress);
            //asMsgArray[4] = Interpreter.InteprateAddress(sMsgFrame.sHeader.ucSourceAddress);
            //asMsgArray[5] = Interpreter.InteprateType(sMsgFrame.sHeader.ucMsgType);

            /* Get the payload */
            byte[] aucPayloadCpy = new byte[6];

            for(byte ucPayloadIdx = 0; ucPayloadIdx < 6; ucPayloadIdx++)
            {
                asMsgArray[6] += sMsgFrame.sPayload.ucData[ucPayloadIdx].ToString("X2") + " ";
                aucPayloadCpy[ucPayloadIdx] = sMsgFrame.sPayload.ucData[ucPayloadIdx];
            }

            //asMsgArray[7] = Interpreter.InteprateIdAndPayload(sMsgFrame.sPayload.ucMsgId, aucPayloadCpy, ref asMsgArray[9]);
            //asMsgArray[8] = Interpreter.InteprateCommand(sMsgFrame.sPayload.ucCommand);

            Interpreter.InteprateMessageFrame(sMsgFrame, ref asMsgArray[3], ref asMsgArray[4], ref asMsgArray[5], ref asMsgArray[8], ref asMsgArray[7], aucPayloadCpy, ref asMsgArray[9]);

            /* Create new list view item to enable the adding */
            ListViewItem lvItem = new ListViewItem(asMsgArray);

            /* Call per invoke to put the item to the list view */
            this.Invoke((MethodInvoker) delegate
            {
                listView1.Items.Insert(0, lvItem);
            });

        }


        /****************************************************************************************************
         * @brief: Prints the buffer handler values in the console 
         * @param: none
         * @return: none
         ****************************************************************************************************/
        private string PrintReceiveBufferHandlerValues(byte ucBufferIdx)
        {
            UInt16 uiFreeSize = 0xFFFF;
            UInt16 uiMaxSize = 0XFFFF;
            UInt16 uiPutIdx = 0xFFFF;
            UInt16 uiGetIdx = 0xFFFF;
            MsgLib_GetFifoBufferHandlerData(ref uiFreeSize, ref uiMaxSize, ref uiPutIdx, ref uiGetIdx, ucBufferIdx);

            string sFifoVal = "Free: " + uiFreeSize.ToString() + " ";
            sFifoVal += "Size: " + uiMaxSize.ToString() + " ";
            sFifoVal += "PutIdx: " + uiPutIdx.ToString() + " ";
            sFifoVal += "GetIdx: " + uiGetIdx.ToString() + " ";

            return sFifoVal;
        }

        /****************************************************************************************************
         * @brief: Prints the buffer handler values in the console 
         * @param: none
         * @return: none
         ****************************************************************************************************/
        private string PrintMessageBufferHandlerValues(byte ucBufferIdx)
        {
            UInt16 uiMsgCnt = 0xFFFF;
            UInt16 uiPutIdx = 0xFFFF;
            UInt16 uiGetIdx = 0xFFFF;
            MsgLib_GetMessageBufferHandlerData(ref uiMsgCnt, ref uiPutIdx, ref uiGetIdx, ucBufferIdx);

            string sMsgBuffVal = "MsgCnt: " + uiMsgCnt.ToString() + " ";
            sMsgBuffVal += "PutIdx: " + uiPutIdx.ToString() + " ";
            sMsgBuffVal += "GetIdx: " + uiGetIdx.ToString() + " ";

            return sMsgBuffVal;
        }

        /****************************************************************************************************
         * @brief: Prints the buffer handler values in the console 
         * @param: none
         * @return: none
         ****************************************************************************************************/
        private string PrintMessageBufferMessageValues(byte ucBufferIdx, byte ucMsgEntryIndex)
        {
            UInt16 uiByteCnt = 0xFFFF;
            UInt16 uiStartIdx = 0XFFFF;
            UInt16 uiSize = 0xFFFF;
            UInt16 uiSavedState = 0xFFFF;
            UInt16 uiRespState = 0xFFFF;
            MsgLib_GetMessageBufferMessageData(ref uiByteCnt, ref uiStartIdx, ref uiSize, ref uiSavedState, ref uiRespState, ucBufferIdx, ucMsgEntryIndex);

            string sMsgVal = "MsgIdx: " + ucMsgEntryIndex.ToString() + " ";
            sMsgVal += "Size: " + uiSize.ToString() + " ";
            sMsgVal += "StartIdx: " + uiStartIdx.ToString() + " ";
            sMsgVal += "Saved: " + uiSavedState.ToString() + " ";
            sMsgVal += "Resp: " + uiRespState.ToString() + " ";

            return sMsgVal;
        }

        /****************************************************************************************************
         * @brief: Process the received serial data
         * @param: none
         * @return: none
         ****************************************************************************************************/
        private void ProcessSerialData(byte ucPortIndex)
        {
            /* Start blocking */
            if (_mutexSerial[ucPortIndex].WaitOne(1000, true))
            {
                /* Get the count of the received bytes */
                int RecDataCnt = _receivedSerialData[ucPortIndex].Count;

                if (RecDataCnt > 0)
                {
                    /* Copy data from received bytes into local array */
                    byte[] arrayRec = new byte[RecDataCnt];
                    _receivedSerialData[ucPortIndex].CopyTo(arrayRec, 0);
                    _receivedSerialData[ucPortIndex].Clear();

                    /* Save received data into text file */
                    string Line = DateTime.Now.ToString() + " Port: " + ucPortIndex.ToString() + " Put " + RecDataCnt.ToString() + " Bytes | ";
                    for (byte Idx = 0; Idx < RecDataCnt; Idx++)
                    {
                        Line += arrayRec[Idx].ToString("X2");
                    }
                    Line += " ";
                    /* Save received data into text file */
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Public\TestFolder\Lines.txt", true))
                    {
                        file.WriteLine(Line);
                    }
                    
                    //Put the buffer into the serial handling
                    MsgLib_PutDataInBuffer(arrayRec, (byte)arrayRec.Length, ucPortIndex);
                                       
                    /* Put the whole received buffer into the text box */
                    //AddTextToSerialDataTextBox(arrayRec, RecDataCnt);
                }

                /* End blocking */
                _mutexSerial[ucPortIndex].ReleaseMutex();
            }
        }

        /****************************************************************************************************
         * @brief: Wrapper function to detect from which serial port the event was received
         * @param: 
         * @return: 
         ****************************************************************************************************/
        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            SerialPort_DataReceived(sp, 0);
        }

        /****************************************************************************************************
         * @brief: Wrapper function to detect from which serial port the event was received
         * @param: 
         * @return: 
         ****************************************************************************************************/
        private void SerialPort2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            SerialPort_DataReceived(sp, 1);
        }


        /****************************************************************************************************
         * @brief: Interrupt handler on received data. The serial port is read and cleared afterwards. 
         *         Puts the received data into the text box and fills the list view when a message is constructed.
         * @param: sender - The serial port which started this event
         * @return: none
         ****************************************************************************************************/
        private void SerialPort_DataReceived(SerialPort sp, byte PortIdx)
        {
            byte[] aucSerialPortBuffer = new byte[255];
            int SP_Read;
            try
            {
                /* Block multiple callbacks */
                if (_mutexSerial[PortIdx].WaitOne(1000, true))
                {
                    //Read the serial port buffer
                    SP_Read = sp.Read(aucSerialPortBuffer, 0, aucSerialPortBuffer.Length);

                    //Clear the serial port buffer to avoid an overfill
                    sp.DiscardInBuffer();

                    for (int buffIdx = 0; buffIdx < SP_Read; buffIdx++)
                    {
                        _receivedSerialData[PortIdx].Enqueue(aucSerialPortBuffer[buffIdx]);
                    }

                    /* Release mutex */
                    _mutexSerial[PortIdx].ReleaseMutex();
                }
            }
            catch(Exception ex)
            {
                
                MessageBox.Show(ex.ToString());
                sp.DiscardInBuffer();
                return;
            }
        }


        /****************************************************************************************************
         * @brief: Handles the received bytes and fills the text box and also the list view. Was necessary
         *         for decoupling the Data-Received thread from this handling thread otherwise the program
         *         could be freezed when COM port is disabled.
         * @param: 
         * @return: none
         ****************************************************************************************************/
        private unsafe void HandleReceivedBytes(object state)
        {
            for (byte ucPortIdx = 0; ucPortIdx < 2; ucPortIdx++)
            {
                if (_mutexSerial[ucPortIdx].WaitOne(1000, true))
                {

                if ((ucPortIdx == 0 && serialPort1.IsOpen) || (ucPortIdx == 1 && serialPort2.IsOpen))
                {
                    //if (true)
                    ReadReceivedBytesFromBuffer(ucPortIdx, false);
                    //else
                    {
                        /* When a message was constructed with the serial-handling put it into the list view */
                        Class_Message.tsMessageFrame sMsgFrame;
                        if (MsgLib_GetMessageFrame(out sMsgFrame, ucPortIdx))
                        {
                            FillListView(sMsgFrame);
                        }
                    }
                }
                _mutexSerial[ucPortIdx].ReleaseMutex();
                }
            }            
        }


        /****************************************************************************************************
         * @brief: Handles the received bytes and fills the text box and also the list view. Was necessary
         *         for decoupling the Data-Received thread from this handling thread otherwise the program
         *         could be freezed when COM port is disabled.
         * @param: 
         * @return: none
         ****************************************************************************************************/
        private unsafe byte[] ReadReceivedBytesFromBuffer(byte ucPortIdx, bool bUpdateBuffer)
        {
            /* Copy data from received bytes into local array */
            byte ucBuffCnt = MsgLib_GetWrittenBufferSize(ucPortIdx);
            byte[] arrayRec = new byte[ucBuffCnt];

            if (ucBuffCnt > 0)
            {
                string sFifoValues = "FifoValues: " + PrintReceiveBufferHandlerValues(ucPortIdx);
                string sMsgBuffValues = "MsgBuffValues: " + PrintMessageBufferHandlerValues(ucPortIdx);
                string sMsgValues = "MsgValues: " + "\n";
                for (int MsgIdx = 0; MsgIdx < 10; MsgIdx++)
                {
                    sMsgValues += PrintMessageBufferMessageValues(ucPortIdx, Convert.ToByte(MsgIdx));
                    sMsgValues += "\n";
                }


                fixed (byte* p = arrayRec)
                {
                    MsgLib_ReadBuffer((IntPtr)p, ucBuffCnt, ucPortIdx, bUpdateBuffer);
                }

                /* Save data into text file */
                string Line = "BufferCnt: " + ucBuffCnt.ToString() + "   ";
                for (byte Idx = 0; Idx < ucBuffCnt; Idx++)
                {
                    Line += arrayRec[Idx].ToString("X2") + " ";
                }
                Line += "\n";

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Public\TestFolder\Buffer_Lines.txt", true))
                {
                    file.WriteLine(sFifoValues);
                    file.WriteLine(sMsgBuffValues);
                    file.WriteLine(sMsgValues);
                    file.WriteLine(Line);
                }               
            }
            return arrayRec;
        }



        /****************************************************************************************************
         * @brief: Checks for the available COM ports
         * @param: 
         * @return: none
         ****************************************************************************************************/
        private void BtnComPortInit_Click(object sender, EventArgs e)
        {
            //Init combo box 
            GetAvailablePorts();
        }

        /****************************************************************************************************
          * @brief: Clear the list view
          * @param: 
          * @return: none
          ****************************************************************************************************/
        private void BtnClearListView_Click(object sender, EventArgs e)
        {
            this.listView1.Items.Clear();
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
                while (true)
                {
                    byte ucBgW_Idx = (byte)e.Argument;


                    if (ucBgW_Idx == 0)
                        HandleReceivedBytes(sender);
                    else
                    {
                        ProcessSerialData(0);
                        ProcessSerialData(1);
                    }
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


        /****************************************************************************************************
        * @brief: Test method for checking when a back ground worker stops working
        * @param: 
        * @return: none
        ****************************************************************************************************/
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if(backgroundWorkersArr[0].IsBusy == true)
            {
                if (progressBar1.Value == 0)
                {
                    progressBar1.Value = progressBar1.Maximum;
                }
                else
                {
                    progressBar1.Value = progressBar1.Minimum;
                }
            }

            if (backgroundWorkersArr[1].IsBusy == true)
            {
                if (progressBar2.Value == 0)
                {
                    progressBar2.Value = progressBar2.Maximum;
                }
                else
                {
                    progressBar2.Value = progressBar2.Minimum;
                }
            }
        }
    }
}
