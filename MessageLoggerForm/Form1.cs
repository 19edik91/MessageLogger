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
        * Functions of the FORM
        ****************************************************************************************************/
        public Form1()
        {
            InitializeComponent();

            //Init combo box 
            GetAvailablePorts();

            InitModule();
        }

        /****************************************************************************************************
        * @brief: Reads all available ports and put them into the combo-box.
        * @param: none
        * @return: none
        ****************************************************************************************************/
        void GetAvailablePorts()
        {
            String[] sPorts = SerialPort.GetPortNames();
            ComboBoxSerialComPorts.Items.AddRange(sPorts);
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
                    serialPort1.ReadTimeout = 2000;
                    serialPort1.WriteTimeout = 1000;
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
            asMsgArray[2] = "Empty";

            asMsgArray[3] = Interpreter.InteprateAddress(sMsgFrame.sHeader.ucDestAddress);
            asMsgArray[4] = Interpreter.InteprateAddress(sMsgFrame.sHeader.ucSourceAddress);
            asMsgArray[5] = Interpreter.InteprateType(sMsgFrame.sHeader.ucMsgType);

            /* Get the payload */
            string sPayload = "";
            for(byte ucPayloadIdx = 0; ucPayloadIdx < 6; ucPayloadIdx++)
            {
                sPayload += sMsgFrame.sPayload.ucData[ucPayloadIdx].ToString("X2") + " ";
            }
            asMsgArray[6] = sPayload;

            asMsgArray[7] = Interpreter.InteprateID(sMsgFrame.sPayload.ucMsgId);
            asMsgArray[8] = Interpreter.InteprateCommand(sMsgFrame.sPayload.ucCommand);

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

                //Use invoke to put the whole received buffer into the text box
                this.Invoke(new MethodInvoker(AddTextToSerialDataTextBox));

                /* When a message was constructed with the serial-handling put
                 * it into the list view */
                Class_Message.tsMessageFrame sMsgFrame;
                if(MsgLib_GetMessageFrame(out sMsgFrame))
                {
                    FillListView(sMsgFrame);
                }                
            }
            catch(Exception ex)
            {
                
                MessageBox.Show(ex.ToString());
                sp.DiscardInBuffer();
                return;
            }
        }
    }
}
