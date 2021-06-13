using System;
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

        [DllImport("MessageLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void MsgLib_GetMsgInitOutputStatus(byte[] ucPayloadArray, out MsgStructureCasted.tMsgRequestOutputStateCS psMsgInitOutputStatusCS);

        [DllImport("MessageLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void MsgLib_GetStillAliveMessage(byte[] ucPayloadArry, out MsgStructureCasted.tsMsgStillAliveCS psMsgStillAlive);

        [DllImport("MessageLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void MsgLib_GetErrorCode(byte[] ucPayloadArray, out MsgStructureCasted.tsMsgErrorCode psMsgFaultMessage);


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
        public string InteprateIdAndPayload(byte ucID, byte ucCommand, byte[] aucPayload, ref string sPayloadInterpreation, bool bInterpretationNeeded)
        {
            string sID = "No Cmd found";

            switch ((ClassMsgEnum.teMessageId)ucID)
            {
                case ClassMsgEnum.teMessageId.eMsgRequestOutputStatus:
                    {
                        sID = "Outputs request status";

                        if (bInterpretationNeeded)
                        {
                            MsgStructureCasted.tMsgRequestOutputStateCS sMsg;
                            MsgLib_GetMsgOutputStatus(aucPayload, out sMsg);

                            sPayloadInterpreation = "Output: " + sMsg.b3OutputIdx + " | ";
                            sPayloadInterpreation += "Brightness: " + sMsg.b7Brightness + "%" + " | ";
                            sPayloadInterpreation += "LED-Status: " + Convert.ToBoolean(sMsg.bLedStatus).ToString() + " | ";
                            sPayloadInterpreation += "Automatic Mode: " + Convert.ToBoolean(sMsg.bAutomaticModeActive).ToString() + " | ";

                            if (sMsg.b3OutputIdx < Form1.ucUsedLables)
                            {
                                Form1._labels[sMsg.b3OutputIdx].szBrightnessReq = sMsg.b7Brightness + "%";
                                Form1._labels[sMsg.b3OutputIdx].szReqLed = Convert.ToBoolean(sMsg.bLedStatus) ? "ON" : "OFF";
                                Form1._labels[sMsg.b3OutputIdx].szAutoReq = Convert.ToBoolean(sMsg.bAutomaticModeActive) ? "ON" : "OFF";
                            }

                            if (Convert.ToBoolean(sMsg.bInitMenuActive) == true && Convert.ToBoolean(sMsg.bInitMenuActiveInv) == false)
                            {
                                sPayloadInterpreation += "Init Menu active: true";
                            }
                            else
                            {
                                sPayloadInterpreation += "Init Menu active: false";
                            }

                            sPayloadInterpreation += "Night Mode: " + Convert.ToBoolean(sMsg.bNightModeOnOff).ToString() + " | ";
                            sPayloadInterpreation += "PIR Mode: " + Convert.ToBoolean(sMsg.bMotionDetectionOnOff).ToString() + " | ";
                            sPayloadInterpreation += "Burntime :" + Convert.ToBoolean(sMsg.ucBurnTime).ToString();
                        }
                        break;
                    }

                case ClassMsgEnum.teMessageId.eMsgUpdateOutputStatus:
                    {
                        sID = "Output update status";

                        if (bInterpretationNeeded)
                        {
                            MsgStructureCasted.tMsgUpdateOutputStateCS sMsg;
                            MsgLib_GetMsgUpdateOutputStatus(aucPayload, out sMsg);

                            sPayloadInterpreation = "Output: " + sMsg.b3OutputIndex + " | ";
                            sPayloadInterpreation += "Brightness: " + sMsg.b7Brightness + "%" + " | ";
                            sPayloadInterpreation += "LED-Status: " + Convert.ToBoolean(sMsg.bLedStatus).ToString() + " | ";

                            if(sMsg.b3OutputIndex < Form1.ucUsedLables)
                            {
                                Form1._labels[sMsg.b3OutputIndex].szBrightnessRes = sMsg.b7Brightness + "%";
                                Form1._labels[sMsg.b3OutputIndex].szResLed = Convert.ToBoolean(sMsg.bLedStatus) ? "ON" : "OFF";
                                Form1._labels[sMsg.b3OutputIndex].szAutoRes = "N/A";
                            }

                            sPayloadInterpreation += "Nightmode: " + Convert.ToBoolean(sMsg.bNightModeOnOff).ToString() + " | ";
                            sPayloadInterpreation += "PIR detected: " + Convert.ToBoolean(sMsg.bMotionDetectionOnOff).ToString() + " | ";
                            sPayloadInterpreation += "Burn time: " + sMsg.slRemainingBurnTime;
                        }
                        break;
                    }

                case ClassMsgEnum.teMessageId.eMsgCurrentTime:
                    {
                        sID = "Current Time";

                        if (bInterpretationNeeded)
                        {
                            MsgStructureCasted.tMsgCurrentTimeCS sMsg;
                            MsgLib_GetCurrentTime(aucPayload, out sMsg);

                            sPayloadInterpreation = "Ticks: " + sMsg.ulTicks + " | ";
                            sPayloadInterpreation += "Hours: " + sMsg.ucHour + " | ";
                            sPayloadInterpreation += "Minutes: " + sMsg.ucMinutes;
                        }
                        break;
                    }

                case ClassMsgEnum.teMessageId.eMsgOutputState:
                    {
                        sID = "Outputs status response ";

                        if (bInterpretationNeeded)
                        {
                            MsgStructureCasted.tsMsgOutputStateResponseCS sMsg;
                            MsgLib_GetMsgOutputStatusResponse(aucPayload, out sMsg);

                            sPayloadInterpreation = "Output: " + sMsg.ucOutputIndex + " | ";
                            sPayloadInterpreation += "Voltage: " + sMsg.ulVoltage + "mV" + " | ";                            
                            sPayloadInterpreation += "Current: " + sMsg.uiCurrent + "mA" + " | ";
                            string Temp = Convert.ToString(sMsg.siTemperature);
                            Temp = Temp.Insert((Temp.Length - 1), ".");
                            sPayloadInterpreation += "Temp: " + Temp + "°C";

                            if (sMsg.ucOutputIndex < Form1.ucUsedLables)
                            {
                                Form1._labels[sMsg.ucOutputIndex].szCurrent = sMsg.uiCurrent + "mA";
                                Form1._labels[sMsg.ucOutputIndex].szVoltage = sMsg.ulVoltage + "mV";
                                Form1._labels[sMsg.ucOutputIndex].szPower = Convert.ToString((sMsg.ulVoltage * sMsg.uiCurrent) / 1000) + "W";
                                Form1._labels[sMsg.ucOutputIndex].szTemp = Temp + "°C";
                            }
                        }
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

                        MsgStructureCasted.tsMsgErrorCode sMsg;
                        MsgLib_GetErrorCode(aucPayload, out sMsg);

                        Int32 slCode = sMsg.uiErrorCode & 0xFFFF;

                        switch (slCode)
                        {
                            case 0x0000: sPayloadInterpreation = "eNoError"; break;
                            case 0x1000: sPayloadInterpreation = "ePinFault"; break;
                            case 0x1001: sPayloadInterpreation = "ePmwFault"; break;
                            case 0x1002: sPayloadInterpreation = "eInputVoltageFault"; break;
                            case 0x1004: sPayloadInterpreation = "eCommunicationFault"; break;
                            case 0x1006: sPayloadInterpreation = "eMessageCrcFault"; break;
                            case 0x1008: sPayloadInterpreation = "eOverTemperatureFault"; break;
                            case 0x100B: sPayloadInterpreation = "eUnknownReturnValue"; break;
                            case 0x100C: sPayloadInterpreation = "eInvalidPointerAccess"; break;
                            case 0x1010: sPayloadInterpreation = "eFlashCRCInvalid"; break;
                            case 0x1011: sPayloadInterpreation = "eFlashDataToBig"; break;
                            case 0x1012: sPayloadInterpreation = "eFlashProtected"; break;
                            case 0x1013: sPayloadInterpreation = "eFlashAddrInvalid"; break;
                            case 0x1020: sPayloadInterpreation = "eTimerInvalidTimer"; break;
                            case 0x1021: sPayloadInterpreation = "eTimerInvalid"; break;
                            case 0x1022: sPayloadInterpreation = "eStateMachineError_EntryNxtState"; break;
                            case 0x1023: sPayloadInterpreation = "eStateMachineError_RootNxtState"; break;
                            case 0x1024: sPayloadInterpreation = "eStateMachineError_ExitNxtState"; break;
                            case 0x1025: sPayloadInterpreation = "eStateMachineError_NoState"; break;
                            case 0x1026: sPayloadInterpreation = "eStateMachineError_InvalidRequest"; break;
                            case 0x1027: sPayloadInterpreation = "eSoftwareTimer_TimerLimit"; break;
                            case 0x1028: sPayloadInterpreation = "eSoftwareTimer_InvalidRequest"; break;
                            case 0x1029: sPayloadInterpreation = "eEventError_NotProcessed"; break;
                            case 0x102A: sPayloadInterpreation = "eOsTimerCreateFault"; break;
                            case 0x102B: sPayloadInterpreation = "eOsTimerDeleteFault"; break;
                            case 0xA001: sPayloadInterpreation = "eOutputVoltageFault_0"; break;
                            case 0xA002: sPayloadInterpreation = "eOutputVoltageFault_1"; break;
                            case 0xA003: sPayloadInterpreation = "eOutputVoltageFault_2"; break;
                            case 0xA004: sPayloadInterpreation = "eOutputVoltageFault_3"; break;
                            case 0xA005: sPayloadInterpreation = "eOverCurrentFault_0"; break;
                            case 0xA006: sPayloadInterpreation = "eOverCurrentFault_1"; break;
                            case 0xA007: sPayloadInterpreation = "eOverCurrentFault_2"; break;
                            case 0xA008: sPayloadInterpreation = "eOverCurrentFault_3"; break;
                            case 0xA009: sPayloadInterpreation = "eLoadMissingFault_0"; break;
                            case 0xA00A: sPayloadInterpreation = "eLoadMissingFault_1"; break;
                            case 0xA00B: sPayloadInterpreation = "eLoadMissingFault_2"; break;
                            case 0xA00C: sPayloadInterpreation = "eLoadMissingFault_3"; break;
                            case 0xA010: sPayloadInterpreation = "eOverTemperatureFault_0"; break;
                            case 0xA011: sPayloadInterpreation = "eOverTemperatureFault_1"; break;
                            case 0xA012: sPayloadInterpreation = "eOverTemperatureFault_2"; break;
                            case 0xA013: sPayloadInterpreation = "eOverTemperatureFault_3"; break;
                            case 0xA014: sPayloadInterpreation = "eCommunicationTimeoutFault"; break;
                            default:
                                sPayloadInterpreation = "Unknown error";
                                break;
                        }

                        sPayloadInterpreation += " | Old Error: ";

                        switch(slCode)
                        {
                            case 0x0000: sPayloadInterpreation += "eNoError"; break;
                            case 0xA000: sPayloadInterpreation += "ePinFault"; break;
                            case 0xA002: sPayloadInterpreation += "eInputVoltageFault"; break;
                            case 0xA004: sPayloadInterpreation += "eOutputVoltageFault_0"; break;
                            case 0xA005: sPayloadInterpreation += "eOutputVoltageFault_1"; break;
                            case 0xA006: sPayloadInterpreation += "eOutputVoltageFault_2"; break;
                            case 0xA008: sPayloadInterpreation += "eCommunicationFault"; break;
                            case 0xA00A: sPayloadInterpreation += "eMessageCrcFault"; break;
                            case 0xA00C: sPayloadInterpreation += "eOverCurrentFault_0"; break;
                            case 0xA00D: sPayloadInterpreation += "eOverCurrentFault_1"; break;
                            case 0xA00E: sPayloadInterpreation += "eOverCurrentFault_2"; break;
                            case 0xA010: sPayloadInterpreation += "eLoadMissingFault_0"; break;
                            case 0xA011: sPayloadInterpreation += "eLoadMissingFault_1"; break;
                            case 0xA012: sPayloadInterpreation += "eLoadMissingFault_2"; break;
                            case 0xA014: sPayloadInterpreation += "eOverTemperatureFault"; break;
                            case 0xA016: sPayloadInterpreation += "eCommunicationTimeoutFault"; break;
                            case 0xFFFF: sPayloadInterpreation += "eErrorListLastEntry"; break;
                            default:    break;
                        }
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

                        if (bInterpretationNeeded)
                        {
                            MsgStructureCasted.tsMsgUserTimerCS sMsg;
                            MsgLib_GetMsgUserTimer(aucPayload, out sMsg);

                            sPayloadInterpreation = "Timer Idx: " + sMsg.b7TimerIdx + " | ";
                            sPayloadInterpreation += "Start: " + sMsg.ucStartHour + ":" + sMsg.ucStartMin + " | ";
                            sPayloadInterpreation += "Stop: " + sMsg.ucStopHour + ":" + sMsg.ucStopMin;
                        }
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

                case ClassMsgEnum.teMessageId.eMsgInitOutputStatus:
                    {
                        sID = "Init output";

                        if (bInterpretationNeeded)
                        {
                            MsgStructureCasted.tMsgRequestOutputStateCS sMsg;
                            MsgLib_GetMsgInitOutputStatus(aucPayload, out sMsg);

                            sPayloadInterpreation = "Output: " + sMsg.b3OutputIdx + " | ";
                            sPayloadInterpreation += "Brightness: " + sMsg.b7Brightness + "%" + " | ";
                            sPayloadInterpreation += "LED-Status: " + Convert.ToBoolean(sMsg.bLedStatus).ToString() + " | ";
                            sPayloadInterpreation += "Automatic Mode: " + Convert.ToBoolean(sMsg.bAutomaticModeActive).ToString() + " | ";
                            sPayloadInterpreation += "Night Mode: " + Convert.ToBoolean(sMsg.bNightModeOnOff).ToString() + " | ";
                            sPayloadInterpreation += "PIR Mode: " + Convert.ToBoolean(sMsg.bMotionDetectionOnOff).ToString() + " | ";
                            sPayloadInterpreation += "Burntime :" + Convert.ToBoolean(sMsg.ucBurnTime).ToString();
                        }
                        break;
                    }

                case ClassMsgEnum.teMessageId.eMsgStillAlive:
                    {
                        sID = "Still alive";

                        MsgStructureCasted.tsMsgStillAliveCS sMsg;
                        MsgLib_GetStillAliveMessage(aucPayload, out sMsg);

                        sPayloadInterpreation = "Req: " + sMsg.bRequest.ToString("X2") + " | ";
                        sPayloadInterpreation += "Res: " + sMsg.bResponse.ToString("X2");
                        break;
                    }

                case ClassMsgEnum.teMessageId.eMsgInitDone:
                    {
                        sID = "User Settings Done";
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
            sID = InteprateIdAndPayload(sMsgFrame.sPayload.ucMsgId, sMsgFrame.sPayload.ucCommand, aucPayload, ref sPayloadInterpreation, bInterpretationNeeded);

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
