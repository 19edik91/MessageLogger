using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace MessageLoggerForm
{
    public class Class_Message
    {
        
        /*************************
        * variables and defines
        ************************/
        private string sMsgName;
        private const UInt16 uiPreamble = 0x1C;
        private UInt16 uiMsgDest = 0x10;
        private UInt16 uiMsgFlag = 0xFFFF;     //Response is 0x80 | Request is 0x40
        private UInt16 uiMsgSource = 0xFFFF;
        private UInt16 uiMsgCmd = 0xFFFF;
        private UInt16 uiMsgObj = 0xFFFF;
        private UInt16 uiMsgSize = 0xFFFF;
        private UInt16 uiMsgCounter = 0;


        /****************************************************
         * Standard object constructor
         ***************************************************/
        public Class_Message(string sName, UInt16 uiCmd, UInt16 uiObj, UInt16 uiSize, UInt16 uiFlag, UInt16 uiSrc, UInt16 uiDest)
        {
            sMsgName = sName;
            uiMsgSource = uiSrc;
            uiMsgDest = uiDest;
            uiMsgCmd = uiCmd;
            uiMsgObj = uiObj;
            uiMsgSize = uiSize;
            uiMsgFlag = uiFlag;
        }

        public Class_Message()
        {
            //Empty
        }



        /****************************************************
         * Method for returning the complete message as a string
         ***************************************************/
        public override string ToString()
        {
            /* Create new string builder object. This is more helpful for casting */
            StringBuilder sb = new StringBuilder();

            /* Format string into hex-string by using "{0:X2}" for two-digits hexadecimal */
            sb.AppendFormat("{0:X2} ", uiPreamble);
            sb.AppendFormat("{0:X2} ", uiMsgDest);
            sb.AppendFormat("{0:X2} ", uiMsgSource);
            sb.AppendFormat("{0:X2} ", uiMsgSize);
            sb.AppendFormat("{0:X2} ", uiMsgFlag);
            sb.AppendFormat("{0:X2} ", uiMsgObj);
            sb.AppendFormat("{0:X2} ", uiMsgCmd);

            return sb.ToString();
        }


        /****************************************************
         * Method for getting the payload size
         ***************************************************/
        public UInt16 GetPayLoadSizeString()
        {
            //Payload size is defined as Object + Command + Data. Therefore the payload has to be checked
            UInt16 uiPayload = uiMsgSize;  //Get complete payload size (with Cmd and Obj)
            uiPayload -= 2; //Subtract size of Cmd and Obj
            uiPayload *= 2; //Every two characters are equal to a byte
            uiPayload += Convert.ToUInt16((uiPayload + 1) / 2); //Add "white-spaces" for every byte

            return uiPayload;
        }

        public UInt16 GetPayLoadSizeInt()
        {
            /* Returns the payload size (Obj + Cmd + Payload) */
            return uiMsgSize;
        }

        /****************************************************
         * Method for getting the message name
         ***************************************************/
        public string GetMessageName()
        {
            return sMsgName;
        }
    }
}
