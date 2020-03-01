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
            eMsgNoId = 0x00,
            eMsgOutputStatus = 0x01,
            eMsgFaultStatus = 0x02,
            eMsgVersion = 0x03,
            eMsgDimPermit = 0x04,
            eMsgErrorCode = 0x05,
            eMsgSleep = 0x06,
            eMsgWakeUp = 0x07,
            eMsgAutoInitHardware = 0x08,
            eMsgManualInitHardware = 0x09,
            eMsgManualInitHwDone = 0x0A,
            eMsgUserTimer = 0x0B,
            eMsgSystemStarted = 0x0C,
            eMsgLastEntry = 0xFF
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
