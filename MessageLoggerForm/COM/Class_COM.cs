using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;

namespace MessageLoggerForm.COM
{
    class Class_COM
    {
        /****************************************************************************************************
        * Variables
        ****************************************************************************************************/
        private int _baudRate = 38400;
        private SerialPort _serialPort = default;
        private Queue<byte> _receivedData = default;
        public string portName { get => _serialPort.PortName; }

        public Class_COM(int baudRate = 38400)
        {
            _baudRate = baudRate;
            _receivedData = new Queue<byte>();
        }

        /****************************************************************************************************
        * Private methods
        ****************************************************************************************************/
        /// <summary>
        /// Configures the serial port with optional settings
        /// </summary>
        /// <param name="writeTimeout"> Time until a timeout occurs in ms </param>
        /// <param name="readTimeout"> Time until a timeout occurs in ms </param>
        /// <param name="handshake"> Kind of handshake (default: none) </param>
        /// <param name="RecByteThreshold"> How many bytes are received until an event is generated </param>
        /// <param name="readBuffSize"> The size of the receive buffer </param>
        /// <param name="writeBuffSize"> The size of the tranceive buffer </param>
        private void ConfigureSerialPort(int writeTimeout = 1000, int readTimeout = 1000, Handshake handshake = Handshake.None, int RecByteThreshold = 1,
                                           int readBuffSize = 2048, int writeBuffSize = 2048 )
        {
            try
            {
                _serialPort.WriteTimeout = writeTimeout;
                _serialPort.ReadTimeout = readTimeout;
                _serialPort.Handshake = handshake;
                _serialPort.ReceivedBytesThreshold = RecByteThreshold;
                _serialPort.ReadBufferSize = readBuffSize;
                _serialPort.WriteBufferSize = writeBuffSize;
                _serialPort.DataReceived += new SerialDataReceivedEventHandler(Event_DataReceived);
            }
            catch(Exception e)
            {
                MessageBox.Show($"COM-Port config invalid - {e.Message}");
            }
        }

        /// <summary>
        /// Data received event which is triggered when serial data has reached the set threshold limit.
        /// </summary>
        /// <param name="sender"> The sender is ususally the serial port </param>
        /// <param name="e"></param>
        private void Event_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] aucPortBuffer = new byte[255];
            int SP_Read;

            try
            {
                //Read the serial port buffer
                SP_Read = _serialPort.Read(aucPortBuffer, default, aucPortBuffer.Length);

                //Discard port buffer to avoid an overfill
                // _serialPort.DiscardInBuffer();

                for(int buffIdx = 0; buffIdx < SP_Read; buffIdx++)
                {
                    _receivedData.Enqueue(aucPortBuffer[buffIdx]);
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show($"Problem with data received Evt on {_serialPort.PortName} {Environment.NewLine} {exc.Message}");
                _serialPort.DiscardInBuffer();
                return;
            }
        }


        /****************************************************************************************************
        * Public methods
        ****************************************************************************************************/

        /// <summary>
        /// Creates a new serial port object, initializes it and opens it
        /// </summary>
        /// <param name="szPortName"></param>
        public void OpenPort(string szPortName)
        {
            if (_serialPort == default 
                && _serialPort.IsOpen == false)
            {
                //Create new serial port object
                _serialPort = new SerialPort(szPortName, _baudRate, Parity.None, 8, StopBits.One);

                //Configure serial port with default settings
                ConfigureSerialPort();

                //Open COM-Port
                _serialPort.Open();
            }
            else
            {
                MessageBox.Show($"{szPortName} already open");
            }
        }

        /// <summary>
        /// Closes the COM port when open and available.
        /// Also disposes the serial-port object
        /// </summary>
        public void ClosePort()
        {
            //Check first if the serial port is open
            if(_serialPort != null 
                && _serialPort.IsOpen == true)
            {
                //Close port
                _serialPort.Close();

                //Dispose port-object
                _serialPort.Dispose();
                _serialPort = default;
            }
            else
            {
                MessageBox.Show($"COM-Port already closed");
            }
        }

        /// <summary>
        /// Reads the bytes from the _receivedData queue and saves them into a list.
        /// After each read the queue entry is decreased.
        /// </summary>
        /// <returns>Byte-List which contains the available serial data </returns>
        public List<byte> ReadReceivedBytes()
        {
            //Create new list
            List<byte> lstData = new List<byte>();

            //Go trough each entry of the queue and save it into the list
            foreach (byte data in _receivedData)
            {
                lstData.Add(_receivedData.Dequeue());
            }
            return lstData;
        }
        

        /// <summary>
        /// Returns the "IsOpen" value
        /// </summary>
        /// <returns></returns>
        public bool IsOpen()
        {
            return _serialPort.IsOpen;
        }
    }
}
