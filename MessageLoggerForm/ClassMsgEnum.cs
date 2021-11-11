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
        public class teMessageId : Class_Helper.Enumeration
        {
            public static teMessageId eMsgRequestOutputStatus = new teMessageId(nameof(eMsgRequestOutputStatus), 0x01);
            public static teMessageId eMsgUpdateOutputStatus = new teMessageId(nameof(eMsgUpdateOutputStatus), 0x02);
            public static teMessageId eMsgVersion = new teMessageId(nameof(eMsgVersion), 0x03);
            public static teMessageId eMsgInitOutputStatus = new teMessageId(nameof(eMsgInitOutputStatus), 0x04);
            public static teMessageId eMsgErrorCode = new teMessageId(nameof(eMsgErrorCode), 0x05);
            public static teMessageId eMsgSleep = new teMessageId(nameof(eMsgSleep), 0x06);
            public static teMessageId eMsgWakeUp = new teMessageId(nameof(eMsgWakeUp), 0x07);
            public static teMessageId eMsgAutoInitHardware = new teMessageId(nameof(eMsgAutoInitHardware), 0x08);
            public static teMessageId eMsgManualInitHardware = new teMessageId(nameof(eMsgManualInitHardware), 0x09);
            public static teMessageId eMsgManualInitHwDone = new teMessageId(nameof(eMsgManualInitHwDone), 0x0A);
            public static teMessageId eMsgUserTimer = new teMessageId(nameof(eMsgUserTimer), 0x0B);
            public static teMessageId eMsgSystemStarted = new teMessageId(nameof(eMsgSystemStarted), 0x0C);
            public static teMessageId eMsgOutputState = new teMessageId(nameof(eMsgOutputState), 0x0D);
            public static teMessageId eMsgCurrentTime = new teMessageId(nameof(eMsgCurrentTime), 0x0E);
            public static teMessageId eMsgStillAlive = new teMessageId(nameof(eMsgStillAlive), 0x0F);
            public static teMessageId eMsgDebug = new teMessageId(nameof(eMsgDebug), 0x10);
            public static teMessageId eMsgInitDone = new teMessageId(nameof(eMsgInitDone), 0x11);
            public static teMessageId eMsgLastEntry = new teMessageId(nameof(eMsgLastEntry), 0xFF);

            public teMessageId(string name, int id) : base(name, id){}


            public static IEnumerable<teMessageId> List()
            {
                return new[]
                {
                    eMsgRequestOutputStatus,
                    eMsgUpdateOutputStatus,
                    eMsgVersion,
                    eMsgInitOutputStatus,
                    eMsgErrorCode,
                    eMsgSleep,
                    eMsgWakeUp,
                    eMsgAutoInitHardware,
                    eMsgManualInitHardware,
                    eMsgManualInitHwDone,
                    eMsgUserTimer,
                    eMsgSystemStarted,
                    eMsgOutputState,
                    eMsgCurrentTime,
                    eMsgStillAlive,
                    eMsgDebug,
                    eMsgInitDone,
                    eMsgLastEntry
                };
            }
        }


        public class teMessageCmd : Class_Helper.Enumeration
        {
            public static teMessageCmd eNoCmd = new teMessageCmd(nameof(eNoCmd), 0x00);
            public static teMessageCmd eCmdGet = new teMessageCmd(nameof(eCmdGet), 0x01);
            public static teMessageCmd eCmdSet = new teMessageCmd(nameof(eCmdSet), 0x02);

            public teMessageCmd(string name, int id) : base(name, id) { }

            public static IEnumerable<teMessageCmd> List()
            {
                return new[] 
                {
                    eNoCmd,
                    eCmdGet,
                    eCmdSet
                };
            }
        }


        public class teMessageType : Class_Helper.Enumeration
        {
            public static teMessageType eNoType = new teMessageType(nameof(eNoType), 0x00);
            public static teMessageType eTypeRequestResponse = new teMessageType(nameof(eTypeRequestResponse), 0x01);
            public static teMessageType eTypeRequestNoResponse = new teMessageType(nameof(eTypeRequestNoResponse), 0x02);
            public static teMessageType eTypeResponseDenied = new teMessageType(nameof(eTypeResponseDenied), 0x04);
            public static teMessageType eTypeResponseUnknown = new teMessageType(nameof(eTypeResponseUnknown), 0x08);
            public static teMessageType eTypeResponseAck = new teMessageType(nameof(eTypeResponseAck), 0x10);

            public teMessageType(string name, int id) : base(name, id) { }

            public static IEnumerable<teMessageType> List()
            {
                return new[]
                {
                    eNoType,
                    eTypeRequestResponse,
                    eTypeRequestNoResponse,
                    eTypeResponseDenied,
                    eTypeResponseUnknown,
                    eTypeResponseAck
                };
            }
        }


        public static string GetMessageIdName(int Id)
        {
            string szName = "";
            IEnumerable<teMessageId> eMsgIds = teMessageId.List();

            foreach(var item in eMsgIds)
            {
                if(item.Id == Id)
                {
                    szName = item.Name;
                }
            }

            return szName;
        }
    }
}
