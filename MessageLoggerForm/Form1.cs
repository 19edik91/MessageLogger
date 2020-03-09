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

        //Messages
        private UInt32 ulMsgIdxCnt = 0;

        private ListViewItem lvItem;

        const int siTryToRead = 255;
        private byte[] aucBuffer = new byte[siTryToRead];
        private byte BufferCnt = 0;
        private string MsgFrame;


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
                if(ComboBoxSerialComPorts.Items.Contains(sPort) == false)
                {
                    ComboBoxSerialComPorts.Items.Add(sPort);
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
            try
            {
                if(ComboBoxSerialComPorts.Text.Length != 0)
                {
                    //Set port name to the Combo box name
                    serialPort1.PortName = ComboBoxSerialComPorts.Text;
                    serialPort1.BaudRate = uiBaudRate;
                    serialPort1.ReadTimeout = 1000;
                    serialPort1.WriteTimeout = 1000;
                    serialPort1.Parity = Parity.None;
                    serialPort1.DataBits = 8;
                    serialPort1.Handshake = Handshake.None;
                    serialPort1.ReadBufferSize = 2;
                    serialPort1.Open();
                    LblComPortStatus.BackColor = Color.Green;
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
            try
            {
                serialPort1.Close();
                serialPort1.Dispose();
                LblComPortStatus.BackColor = Color.Red;
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
         * @brief: Adds the new created list view item at the first position
         * @param: none
         * @return: none
         ****************************************************************************************************/
        private void AddNewItem()
        {
            listView1.Items.Insert(0, this.lvItem);
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
            this.lvItem = new ListViewItem(asMsgArray);

            /* Call per invoke to put the item to the list view */
            this.Invoke(new MethodInvoker(AddNewItem));
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
            this.BufferCnt = (byte)sp.BytesToRead;

            try
            {
                //Read the serial port buffer
                sp.Read(this.aucBuffer, 0, BufferCnt);

                //Clear the serial port buffer to avoid an overfill
                sp.DiscardInBuffer();

                //Put the buffer into the serial handling
                MsgLib_PutDataInBuffer(this.aucBuffer, BufferCnt);

                //Decouple the receive thread from the handling thread.
                //Shall improve / fix freezes when the serial port is closed during received data
                ThreadPool.QueueUserWorkItem(HandleReceivedBytes);
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
            if (MsgLib_GetMessageFrame(out sMsgFrame))
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
