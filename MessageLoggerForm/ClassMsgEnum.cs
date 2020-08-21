using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLoggerForm
{
    public class ClassMsgEnum
    {
        public const byte ADDRESS_MASTER = 0x10;
        public const byte ADDRESS_SLAVE1 = 0x01;
        public const byte PREAMBE = 0x1C;

        /************************************ Message Ids **********************************/
        public enum teMessageId
        {
            eMsgNoId                = 0x00,     /*<-- No message --> */
            eMsgRequestOutputStatus = 0x01,     /*<-- Request for output values. Slave => Master        --> */
            eMsgUpdateOutputStatus  = 0x02,     /*<-- Updated output values. Slave <= Master            --> */
            eMsgVersion             = 0x03,     /*<-- Message for Software Version.  Slave <= Master    --> */
            eMsgInitOutputStatus    = 0x04,     /*<-- Message with the output values from the FLASH.  Slave <= Master    --> */
            eMsgErrorCode           = 0x05,     /*<-- Fault code message. Currently unknown handling    --> */
            eMsgSleep               = 0x06,     /*<-- Sleep request. Slave <= Master                    --> */
            eMsgWakeUp              = 0x07,     /*<-- Wake up request. Slave <=> Master                 --> */
            eMsgAutoInitHardware    = 0x08,     /*<-- Automatic Hardware init request. Slave => Master  --> */
            eMsgManualInitHardware  = 0x09,     /*<-- Message with the Min-Max-Values. Slave => Master  --> */
            eMsgManualInitHwDone    = 0x0A,     /*<-- Message to trigger saving of the system data. Slave => Master  --> */
            eMsgUserTimer           = 0x0B,     /*<-- Message with the user timers. Slave <=> Master    --> */
            eMsgSystemStarted       = 0x0C,     /*<-- Slave started message. Slave => Master            --> */
            eMsgOutputState         = 0x0D,     /*<-- Sends the current, voltage and temperature values. Slave <= Master --> */
            eMsgCurrentTime         = 0x0E,     /*<-- The current time from the ethernet. Slave => Master  --> */
            eMsgLastEntry           = 0xFF
        }

        public enum teMessageCmd
        {
            eNoCmd = 0x00,
            eCmdGet = 0x01,
            eCmdSet = 0x02
        }


        public enum teMessageType
        {
            eNoType = 0x00,
            eTypeRequestResponse = 0x01,
            eTypeRequestNoResponse = 0x02,
            eTypeResponseDenied = 0x04,
            eTypeResponseUnknown = 0x08,
            eTypeResponseAck = 0x10
        }
    }
}
