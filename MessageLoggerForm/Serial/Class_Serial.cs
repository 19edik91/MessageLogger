using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLoggerForm.Serial
{
    public class Class_Serial
    {
        //Customized event args
        public class MessageReceivedArgs : EventArgs
        {
            //Amount of messages which are still queued
            public int messagesQueued;
        }

        /* Create first a delegate and afterwards a variable of the delegate with the event keyword */
        /// <summary>
        /// Event delegate which is called when an event has been risen
        /// </summary>
        /// <param name="sender"> sender of the event </param>
        /// <param name="args"> customized paramters </param>
        public delegate void MessageReceived(object sender, MessageReceivedArgs args);

        //Event. MessageReceived delegate specifies the signature for the MessageReceivedEvt event handler.
        //So it specifies that the event handler method in subscriber class must have an object and a MessageReceivedArgs parameter
        //public event EventHandler<MessageReceivedArgs> MessageReceivedEvt;
        public event MessageReceived OnMessageReceived;

        /******** Serial-communicaiton constants **********/
        private const byte START_FLAG = 0xA5;
        private const byte ESCAPE_FLAG = 0x7E;
        private const byte END_FLAG = 0x66;

        private enum teReceiveState
        {
            eStart,
            eData
        };

        //Queue for the received bytes
        private Queue<byte> _rxQueue = new Queue<byte>();

        //Queue for the decoded message frames
        private Queue<MsgStructure.tsMessageFrame> _msgFrames = new Queue<MsgStructure.tsMessageFrame>();

        
        //Receive state
        teReceiveState _eReceiveState = teReceiveState.eStart;
        bool _bEscFlagReceived = false;
     

        /// <summary>
        /// Method which is called from this class to notify all registered handlers
        /// </summary>
        protected virtual void OnMessageReceivedFn()
        {
            //When the event is not null (has subscribers) than call the delegates
            //-> All event handlers which are registered with the MessageReceivedEvt
            MessageReceivedArgs args = new MessageReceivedArgs();
            args.messagesQueued = _msgFrames.Count;
            OnMessageReceived?.Invoke(this, args);
        }

        /// <summary>
        /// Checks for a start flag on the data. Allows to switch to the next
        /// state.
        /// </summary>
        /// <param name="ucData"> Serial-data which is checked for start.</param>
        private void State_ReceiveStart(byte ucData)
        {
            //Check for start flag
            if(ucData == START_FLAG)
            {
                //Switch to next state
                _eReceiveState = teReceiveState.eData;

                //Enable receive timeout control
                //TODO:
            }
        }

        /// <summary>
        /// Receives data
        /// </summary>
        /// <param name="ucData"></param>
        private void State_ReceiveData(byte ucData)
        {
            //Check for Escape flag
            if (ucData == ESCAPE_FLAG && _bEscFlagReceived == false)
            {
                //Next data is equal to a known flag (start, stop or ESC)
                _bEscFlagReceived = true;
            }
            else
            {
                //Check for end-Flag
                if(ucData == END_FLAG && _bEscFlagReceived == false)
                {
                    //Disable receive timeout control
                    //TODO:

                    //Throw Event to inform about a valid message has been received
                    OnMessageReceivedFn();

                    //Enque casted message frame
                    _msgFrames.Enqueue(MsgStructure.tsMessageFrame.FromArray(_rxQueue.ToArray()));
                    _rxQueue.Clear();

                    //Change state back to start
                    _eReceiveState = teReceiveState.eStart;
                }
                else
                {
                    //Put data into the queue
                    _rxQueue.Enqueue(ucData);
                }

                //Next data is regular data
                _bEscFlagReceived = false;

            }
        }


        /// <summary>
        /// Method to decode the data from a list into the rx-queue
        /// </summary>
        /// <param name="lstReceivedData"> A list with data which was received from a serial port </param>
        public void DataReceived(List<byte> lstReceivedData)
        {
            foreach(byte ucData in lstReceivedData)
            {
                DataReceived(ucData);
            }
        }


        /// <summary>
        /// Method to decode data into the rx-queue
        /// </summary>
        /// <param name="ucData"> Data which shall be saved into the queue</param>
        public void DataReceived(byte ucData)
        {
            switch (_eReceiveState)
            {
                case teReceiveState.eStart:
                    State_ReceiveStart(ucData);
                    break;

                case teReceiveState.eData:
                    State_ReceiveData(ucData);
                    break;
            }
        }

        /// <summary>
        /// Gets the next message frame from the enqued messages
        /// </summary>
        /// <returns></returns>
        public MsgStructure.tsMessageFrame GetNextMessageFrame()
        {
            return _msgFrames.Dequeue();
        }
    }
}
