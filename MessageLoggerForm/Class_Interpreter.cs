﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


namespace MessageLoggerForm
{
    public class Class_Interpreter
    {
        /****************************************************************************************************
        * C-DLL functions
        ****************************************************************************************************/
        [DllImport("MessageLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void MsgLib_GetMsgOutputStatus(byte[] ucPayloadArray, out MsgStructureCasted.tMsgRequestOutputStateCS psMsgOutputStateCS);

        [DllImport("MessageLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void MsgLib_GetMsgUpdateOutputStatus(byte[] ucPayloadArray, out MsgStructureCasted.tMsgUpdateOutputStateCS psMsgUpdateOutputStateCS);

        [DllImport("MessageLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void MsgLib_GetCurrentTime(byte[] ucPayloadArray, out MsgStructureCasted.tMsgCurrentTimeCS psMsgCurrentTime);

        [DllImport("MessageLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void MsgLib_GetMsgOutputStatusResponse(byte[] ucPayloadArray, out MsgStructureCasted.tsMsgOutputStateResponseCS psMsgOutputStateResponseCS);

        [DllImport("MessageLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void MsgLib_GetMsgVersion(byte[] ucPayloadArray, out MsgStructureCasted.tsMsgVersionCS psMsgVersionCS);

        [DllImport("MessageLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void MsgLib_GetMsgFaultMessage(byte[] ucPayloadArray, out MsgStructureCasted.tsMsgFaultMessageCS psMsgFaultMessageCS);

        [DllImport("MessageLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void MsgLib_GetMsgManualInit(byte[] ucPayloadArray, out MsgStructureCasted.tsMsgManualInitCS psMsgManualInitCS);

        [DllImport("MessageLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void MsgLib_GetMsgUserTimer(byte[] ucPayloadArray, out MsgStructureCasted.tsMsgUserTimerCS psMsgUserTimerCS);


        /****************************************************
         * @brief: Standard constructor for this class
         * @param: none
         * @return: none
         ***************************************************/
        public Class_Interpreter()
        {

        }

        /****************************************************
         * @brief: Interprates the command and returns a string
         * @param: ucCommand - Command as a byte
         * @return: sCmd - Interprated command as a string
         ***************************************************/
        public string InteprateCommand(byte ucCommand)
        {
            string sCmd = "No Cmd found";

            switch((ClassMsgEnum.teMessageCmd)ucCommand)
            {
                case ClassMsgEnum.teMessageCmd.eCmdGet:
                {
                    sCmd = "Get";
                    break;
                }

                case ClassMsgEnum.teMessageCmd.eCmdSet:
                {
                    sCmd = "Set";
                    break;
                }

                case ClassMsgEnum.teMessageCmd.eNoCmd:
                default:
                    break;
            }

            return sCmd;
        }

        /****************************************************
         * @brief: Interprates the type and returns a string
         * @param: ucType - Type as a byte
         * @return: sType - Interprated type as a string
         ***************************************************/
        public string InteprateType(byte ucType, ref bool bInterpretationNeeded)
        {
            string sType = "No Type found";

            switch ((ClassMsgEnum.teMessageType)ucType)
            {
                case ClassMsgEnum.teMessageType.eTypeRequestNoResponse:
                {
                    sType = "Request no response";
                    break;
                }

                case ClassMsgEnum.teMessageType.eTypeRequestResponse:
                {
                    sType = "Request with response";
                    break;
                }

                case ClassMsgEnum.teMessageType.eTypeResponseAck:
                {
                    sType = "Response - ACK";
                    bInterpretationNeeded = false;
                    break;
                }

                case ClassMsgEnum.teMessageType.eTypeResponseDenied:
                {
                    sType = "Response - Denied";
                    bInterpretationNeeded = false;
                    break;
                }

                case ClassMsgEnum.teMessageType.eTypeResponseUnknown:
                {
                    sType = "Response - Unknown";
                    break;
                }

                case ClassMsgEnum.teMessageType.eNoType:
                default:
                    break;
            }

            return sType;
        }

        /****************************************************
         * @brief: Interprates the message ID and returns a string
         * @param: ucID - Message ID as a byte
         * @return: sID - Interprated ID as a string
         ***************************************************/
        public string InteprateIdAndPayload(byte ucID, byte ucCommand, byte[] aucPayload, ref string sPayloadInterpreation)
        {
            string sID = "No Cmd found";

            switch ((ClassMsgEnum.teMessageId)ucID)
            {
                case ClassMsgEnum.teMessageId.eMsgRequestOutputStatus:
                    {
                        sID = "Outputs request status";

                        MsgStructureCasted.tMsgRequestOutputStateCS sMsg;
                        MsgLib_GetMsgOutputStatus(aucPayload, out sMsg);

                        sPayloadInterpreation = "Brightness: " + sMsg.b7Brightness + "%" + " | ";
                        sPayloadInterpreation += "LED-Status: " + Convert.ToBoolean(sMsg.bLedStatus).ToString() + " | ";
                        sPayloadInterpreation += "Automatic Mode: " + Convert.ToBoolean(sMsg.bAutomaticModeActive).ToString() + " | ";

                        if (Convert.ToBoolean(sMsg.bInitMenuActive) == true && Convert.ToBoolean(sMsg.bInitMenuActiveInv) == false)
                        {
                            sPayloadInterpreation += "Init Menu acitve: true";
                        }
                        else
                        {
                            sPayloadInterpreation += "Init Menu acitve: false";
                        }

                        sPayloadInterpreation += "Night Mode: " + Convert.ToBoolean(sMsg.bNightModeOnOff).ToString() + " | ";
                        sPayloadInterpreation += "PIR Mode: " + Convert.ToBoolean(sMsg.bMotionDetectionOnOff).ToString() + " | ";
                        sPayloadInterpreation += "Burntime :" + Convert.ToBoolean(sMsg.ucBurnTime).ToString();            
                        break;
                    }

                case ClassMsgEnum.teMessageId.eMsgUpdateOutputStatus:
                    {
                        sID = "Output update status";

                        MsgStructureCasted.tMsgUpdateOutputStateCS sMsg;
                        MsgLib_GetMsgUpdateOutputStatus(aucPayload, out sMsg);

                        sPayloadInterpreation = "Brightness: " + sMsg.b7Brightness + "%" + " | ";
                        sPayloadInterpreation += "LED-Status: " + Convert.ToBoolean(sMsg.bLedStatus).ToString() + " | ";
                        sPayloadInterpreation += "Nightmode: " + Convert.ToBoolean(sMsg.bNightModeOnOff).ToString() + " | ";
                        sPayloadInterpreation += "PIR detected: " + Convert.ToBoolean(sMsg.bMotionDetectionOnOff).ToString() + " | ";
                        sPayloadInterpreation += "Burn time: " + sMsg.ucRemainingBurnTime;
                        break;
                    }

                case ClassMsgEnum.teMessageId.eMsgCurrentTime:
                    {
                        sID = "Current Time";

                        MsgStructureCasted.tMsgCurrentTimeCS sMsg;
                        MsgLib_GetCurrentTime(aucPayload, out sMsg);

                        sPayloadInterpreation = "Ticks: " + sMsg.ulTicks + " | ";
                        sPayloadInterpreation += "Hours: " + sMsg.ucHour + " | ";
                        sPayloadInterpreation += "Minutes: " + sMsg.ucMinutes;
                        break;
                    }

                case ClassMsgEnum.teMessageId.eMsgOutputState:
                    {
                        sID = "Outputs status response ";

                        MsgStructureCasted.tsMsgOutputStateResponseCS sMsg;
                        MsgLib_GetMsgOutputStatusResponse(aucPayload, out sMsg);

                        sPayloadInterpreation = "Voltage: " + sMsg.uiVoltage + "mV" + " | ";
                        sPayloadInterpreation += "Current: " + sMsg.uiCurrent + "mA" + " | ";

                        string Temp = Convert.ToString(sMsg.siTemperature);
                        Temp = Temp.Insert((Temp.Length - 1), ".");
                        sPayloadInterpreation += "Temp: " + Temp + "°C";
                        break;
                    }

                case ClassMsgEnum.teMessageId.eMsgAutoInitHardware:
                    {
                        sID = "Automatic hardware init";
                        break;
                    }

                case ClassMsgEnum.teMessageId.eMsgErrorCode:
                    {
                        sID = "Error code";
                        break;
                    }
                    
                case ClassMsgEnum.teMessageId.eMsgLastEntry:
                    {
                        sID = "Last entry";
                        break;
                    }

                case ClassMsgEnum.teMessageId.eMsgManualInitHardware:
                    {
                        sID = "Manual hardware init";
                        break;
                    }

                case ClassMsgEnum.teMessageId.eMsgManualInitHwDone:
                    {
                        sID = "Manual hardware init done";
                        break;
                    }

                case ClassMsgEnum.teMessageId.eMsgSleep:
                    {
                        sID = "Sleep";
                        break;
                    }

                case ClassMsgEnum.teMessageId.eMsgSystemStarted:
                    {
                        sID = "System started";
                        break;
                    }

                case ClassMsgEnum.teMessageId.eMsgUserTimer:
                    {
                        sID = "User Timer";

                        MsgStructureCasted.tsMsgUserTimerCS sMsg;
                        MsgLib_GetMsgUserTimer(aucPayload, out sMsg);

                        sPayloadInterpreation = "Timer Idx: " + sMsg.b7TimerIdx + " | ";
                        sPayloadInterpreation += "Start: " + sMsg.ucStartHour + ":" + sMsg.ucStartMin + " | ";
                        sPayloadInterpreation += "Stop: " + sMsg.ucStopHour + ":" + sMsg.ucStopMin;
                        break;
                    }

                case ClassMsgEnum.teMessageId.eMsgVersion:
                    {
                        sID = "Version";
                        break;
                    }

                case ClassMsgEnum.teMessageId.eMsgWakeUp:
                    {
                        sID = "Wake up";
                        break;
                    }

                case ClassMsgEnum.teMessageId.eMsgNoId:
                default:
                    break;
            }

            return sID;
        }

        /****************************************************
         * @brief: Interprates the address and returns a string
         * @param: ucAddress - Address as a byte
         * @return: sAddress - Interprated address as a string
         ***************************************************/
        public string InteprateAddress(byte ucAddress)
        {
            string sAddress = "No address found";

            switch (ucAddress)
            {
                case ClassMsgEnum.ADDRESS_MASTER:
                {
                    sAddress = "Cypress";
                    break;
                }

                case ClassMsgEnum.ADDRESS_SLAVE1:
                {
                    sAddress = "ESP";
                    break;
                }

                default:
                    break;
            }

            return sAddress;
        }

        /****************************************************
         * @brief: Interprates the message frame and re-writes the given parameter.
         * @param: sMsgFrame - The message frame to interprate
         * @return: none
         ***************************************************/
        public void InteprateMessageFrame(Class_Message.tsMessageFrame sMsgFrame, ref string sDestAddr, ref string sSourceAddr, ref string sType,
                                            ref string sCmd, ref string sID, byte[] aucPayload, ref string sPayloadInterpreation)
        {
            bool bInterpretationNeeded = true;

            sDestAddr = InteprateAddress(sMsgFrame.sHeader.ucDestAddress);
            sSourceAddr = InteprateAddress(sMsgFrame.sHeader.ucSourceAddress);
            sType = InteprateType(sMsgFrame.sHeader.ucMsgType, ref bInterpretationNeeded);
            sID = InteprateIdAndPayload(sMsgFrame.sPayload.ucMsgId, sMsgFrame.sPayload.ucCommand, aucPayload, ref sPayloadInterpreation);

            if (bInterpretationNeeded == true)
            {
                sCmd = InteprateCommand(sMsgFrame.sPayload.ucCommand);
            }
            else
            {
                sPayloadInterpreation = " ";
            }
        }
    }
}
