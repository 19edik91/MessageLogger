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



namespace MessageLoggerForm
{
    public partial class Form1 : Form
    {

        const UInt16 uiBaudRate = 32800;



        public Form1()
        {
            InitializeComponent();

            //Init combo box 
            getAvailablePorts();
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
                serialPort1.PortName = ComboBoxSerialComPorts.Text;
                serialPort1.Close();
                serialPort1.Dispose();
                LblComPortStatus.BackColor = Color.Red;
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("COM Port has to be opened first");
            }
        }

        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;

            const int siTryToRead = 255;
            byte[] aucBuffer = new byte[siTryToRead];
            try
            {
                sp.Read(aucBuffer, 0, sp.BytesToRead);
                sp.DiscardInBuffer();
            }
            catch(Exception)
            {
                MessageBox.Show("Full");
                return;
            }
        }
    }
}
