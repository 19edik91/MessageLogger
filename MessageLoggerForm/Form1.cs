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


namespace MessageLoggerForm
{
    public partial class Form1 : Form
    {
        [DllImport("MessageLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SumVariables(int a, int b);

        [DllImport("MessageLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool MsgLib_GetMessageFrame(out Class_Message.tsMessageFrame psMessageFrame);

        [DllImport("MessageLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool MsgLib_PutDataInBuffer(byte[] pucBuffer, byte ucSize);

        [DllImport("MessageLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void InitModule();

        const UInt16 uiBaudRate = 38400;

        //Messages
        private Class_Message sMsg = new Class_Message();
        private UInt32 ulMsgIdxCnt = 0;

        private ListViewItem lvItem;

        const int siTryToRead = 255;
        private byte[] aucBuffer = new byte[siTryToRead];
        private byte BufferCnt = 0;
        private string MsgFrame;


        public Form1()
        {
            InitializeComponent();

            //Init combo box 
            getAvailablePorts();

            InitModule();
        }

        //Function to get all available Ports and display them in the combo box
        void getAvailablePorts()
        {
            String[] sPorts = SerialPort.GetPortNames();
            ComboBoxSerialComPorts.Items.AddRange(sPorts);
        }

        //Function to start the COM port
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

        public static string ToHexString(string str)
        {
            var sb = new StringBuilder();

            var bytes = Encoding.Unicode.GetBytes(str);
            foreach (var t in bytes)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString(); // returns: "48656C6C6F20776F726C64" for "Hello world"
        }

        private void ChangeLabel()
        {
            //for (byte Idx = 0; Idx < BufferCnt; Idx++)
            //{
            //    string s = aucBuffer[Idx].ToString("X2");
            //    
            //    LblMessage.Text += s + " ";
            //}
            LblMessage.Text = MsgFrame;

            LblMessage.Text += "\n";
        }

        private void AddNewItem()
        {
            listView1.Items.Add(this.lvItem);
        }

        private unsafe void FillListView(Class_Message.tsMessageFrame sMsgFrame)
        {
            /* Create string array with the count of list view colums */
            string[] asMsgArray = new string[listView1.Columns.Count];

            
            DateTime sDate = DateTime.Now;

            /* Add items to list view */
            asMsgArray[0] = this.ulMsgIdxCnt.ToString();
            ++this.ulMsgIdxCnt;

            asMsgArray[1] = sDate.TimeOfDay.ToString();

            //            asMsgArray[2] = sMsgFrame.sHeader.ucMsgType.ToString("X2");
            asMsgArray[2] = "Empty";

            asMsgArray[3] = sMsgFrame.sHeader.ucDestAddress.ToString("X2");
            asMsgArray[4] = sMsgFrame.sHeader.ucSourceAddress.ToString("X2");
            asMsgArray[5] = sMsgFrame.sHeader.ucMsgType.ToString("X2");

            /* Get the payload */
            string sPayload = "";
            for(byte ucPayloadIdx = 0; ucPayloadIdx < 6; ucPayloadIdx++)
            {
                sPayload += sMsgFrame.sPayload.ucData[ucPayloadIdx].ToString("X2") + " ";
            }
            asMsgArray[6] = sPayload;

            asMsgArray[7] = sMsgFrame.sPayload.ucMsgId.ToString("X2");
            asMsgArray[8] = sMsgFrame.sPayload.ucCommand.ToString("X2");

            this.lvItem = new ListViewItem(asMsgArray);

            this.Invoke(new MethodInvoker(AddNewItem));            
        }


        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;

            //const int siTryToRead = 255;
            //byte[] aucBuffer = new byte[siTryToRead];
            this.BufferCnt = (byte)sp.BytesToRead;

            try
            {
                sp.Read(aucBuffer, 0, BufferCnt);

                sp.DiscardInBuffer();

                MsgLib_PutDataInBuffer(aucBuffer, BufferCnt);

//                this.Invoke(new MethodInvoker(ChangeLabel));

                Class_Message.tsMessageFrame sMsgFrame;
                if(MsgLib_GetMessageFrame(out sMsgFrame))
                {

                    FillListView(sMsgFrame);
//                    DateTime sDate = DateTime.Now; 
//                    MsgFrame = sDate.TimeOfDay.ToString() + " "+  sMsgFrame.sHeader.ucPreamble.ToString("X2") +" "+ sMsgFrame.sHeader.ucDestAddress.ToString("X2") +" "+ sMsgFrame.sHeader.ucSourceAddress.ToString("X2") + " " + sMsgFrame.sHeader.ucMsgType.ToString("X2") + "\n";
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
