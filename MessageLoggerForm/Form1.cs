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
        public static extern bool MsgLib_GetMessageFrame(out Class_Message.tsMessageFrame psMessageFrame);

        [DllImport("MessageLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool MsgLib_PutDataInBuffer(byte[] pucBuffer, byte ucSize);

        [DllImport("MessageLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void InitModule();


        /****************************************************************************************************
        * Variables
        ****************************************************************************************************/
        const UInt16 uiBaudRate = 38400;
        //const int uiBaudRate = 115200;

        //Messages
        private UInt32 ulMsgIdxCnt = 0;

        const int siTryToRead = 255;
        private byte[] aucBuffer = new byte[siTryToRead];
        private byte BufferCnt = 0;
        private string MsgFrame;

        delegate void AddListItem();
        AddListItem _serialDelegate;

        Queue<Byte> _receivedSerialData;

        Mutex _mutexSerial;

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
            _receivedSerialData = new Queue<byte>();
            _mutexSerial = new Mutex();

            //Init combo box 
            GetAvailablePorts();

            InitModule();

            //Activate the double buffered option in the list view
            SetDoubleBufferd(listView1);
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
                    sp.ReadTimeout = 500;
                    sp.WriteTimeout = 500;
                    sp.Parity = Parity.None;
                    sp.DataBits = 8;
                    sp.StopBits = StopBits.One;
                    sp.Handshake = Handshake.None;
                    sp.ReadBufferSize = 14;
//                    sp.ReceivedBytesThreshold = 2;
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
        private void AddTextToSerialDataTextBox()
        {
            //Put the time index at the first place
            DateTime sDate = DateTime.Now; 
            RichTextBoxSerialData.Text += sDate.TimeOfDay.ToString() + " ";

            //Put the whole buffer data into the text box
            for (byte Idx = 0; Idx < BufferCnt; Idx++)
            {
                string sBufferEntry = this.aucBuffer[Idx].ToString("X2");
                
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
         * @brief: Process the received serial data
         * @param: none
         * @return: none
         ****************************************************************************************************/
        private void ProcessSerialData()
        {
            /* Get the count of the received bytes */
            int RecDataCnt = _receivedSerialData.Count;

            /* Start blocking */
            _mutexSerial.WaitOne();

            /* Copy data from received bytes into local array */
            byte[] arrayRec = new byte[RecDataCnt];
            _receivedSerialData.CopyTo(arrayRec, 0);
            _receivedSerialData.Clear();

            /* End blocking */
            _mutexSerial.ReleaseMutex();


            ///********
            string Line = "";

            for (byte Idx = 0; Idx < RecDataCnt; Idx++)
            {
                Line += arrayRec[Idx].ToString("X2");
            }

            Line += " ";

            //System.IO.File.WriteAllText(@"C:\Users\Public\TestFolder\Lines.txt", Line);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Public\TestFolder\Lines.txt", true))
            {
                file.WriteLine(Line);
            }
            //********/

            //Put the buffer into the serial handling
            MsgLib_PutDataInBuffer(arrayRec, (byte)arrayRec.Length);

            //Decouple the receive thread from the handling thread.
            //Shall improve / fix freezes when the serial port is closed during received data
            //ThreadPool.QueueUserWorkItem(HandleReceivedBytes);

            //Use invoke to put the whole received buffer into the text box
            //this.Invoke(new MethodInvoker(AddTextToSerialDataTextBox));
            AddTextToSerialDataTextBox();

            /* When a message was constructed with the serial-handling put
             * it into the list view */
            Class_Message.tsMessageFrame sMsgFrame;
            while (MsgLib_GetMessageFrame(out sMsgFrame))
            {
                FillListView(sMsgFrame);
            }
        }

        /****************************************************************************************************
         * @brief: Interrupt handler on received data. The serial port is read and cleared afterwards. 
         *         Puts the received data into the text box and fills the list view when a message is constructed.
         * @param: sender - The serial port which started this event
         * @return: none
         ****************************************************************************************************/
        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;

            byte[] aucSerialPortBuffer = new byte[255];
            int SP_Read;
            try
            {
                //Read the serial port buffer
                SP_Read = sp.Read(aucSerialPortBuffer, 0, aucSerialPortBuffer.Length);                

                //Clear the serial port buffer to avoid an overfill
                sp.DiscardInBuffer();

                /* Block multiple callbacks */
                _mutexSerial.WaitOne();

                for(int buffIdx = 0; buffIdx < SP_Read; buffIdx++)
                {
                    _receivedSerialData.Enqueue(aucSerialPortBuffer[buffIdx]);
                }

                /* Release mutex */
                _mutexSerial.ReleaseMutex();

                //Use the delegate callback function 
                Invoke(_serialDelegate);
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
        private void HandleReceivedBytes(object state)
        {
            //Use invoke to put the whole received buffer into the text box
            this.Invoke(new MethodInvoker(AddTextToSerialDataTextBox));
            //AddTextToSerialDataTextBox();

            /* When a message was constructed with the serial-handling put
             * it into the list view */
            Class_Message.tsMessageFrame sMsgFrame;
            while(MsgLib_GetMessageFrame(out sMsgFrame))
            {
                FillListView(sMsgFrame);
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
    }
}
