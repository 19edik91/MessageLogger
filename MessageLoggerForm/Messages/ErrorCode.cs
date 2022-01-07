using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLoggerForm.Messages
{
    class ErrorCode
    {
        public enum teFaultCodes
        {
            eNoError = 0x0000,
            ePinFault = 0x1000,
            ePmwFault = 0x1001,
            eInputVoltageFault = 0x1002,
            eCommunicationFault = 0x1004,
            eMessageCrcFault = 0x1006,
            eOverTemperatureFault = 0x1008,
            eUnknownReturnValue = 0x100B,
            eInvalidPointerAccess = 0x100C,
            eFlashCRCInvalid = 0x1010,
            eFlashDataToBig = 0x1011,
            eFlashProtected = 0x1012,
            eFlashAddrInvalid = 0x1013,
            eTimerInvalidTimer = 0x1020,
            eTimerInvalid = 0x1021,
            eStateMachineError_EntryNxtState = 0x1022,
            eStateMachineError_RootNxtState = 0x1023,
            eStateMachineError_ExitNxtState = 0x1024,
            eStateMachineError_NoState = 0x1025,
            eStateMachineError_InvalidRequest = 0x1026,
            eSoftwareTimer_TimerLimit = 0x1027,
            eSoftwareTimer_InvalidRequest = 0x1028,
            eEventError_NotProcessed = 0x1029,
            eOsTimerCreateFault = 0x102A,
            eOsTimerDeleteFault = 0x102B,
            eOsComm_ResenBufferFault = 0x102C,
            eOsComm_SendBufferFault = 0x102D,
            eOsMemory_InvalidFree = 0x102E,
            eOsMemory_InvalidCreate = 0x102F,
            eOutputVoltageFault_0 = 0xA001,
            eOutputVoltageFault_1 = 0xA002,
            eOutputVoltageFault_2 = 0xA003,
            eOutputVoltageFault_3 = 0xA004,
            eOverCurrentFault_0 = 0xA005,
            eOverCurrentFault_1 = 0xA006,
            eOverCurrentFault_2 = 0xA007,
            eOverCurrentFault_3 = 0xA008,
            eLoadMissingFault_0 = 0xA009,
            eLoadMissingFault_1 = 0xA00A,
            eLoadMissingFault_2 = 0xA00B,
            eLoadMissingFault_3 = 0xA00C,
            eOverTemperatureFault_0 = 0xA010,
            eOverTemperatureFault_1 = 0xA011,
            eOverTemperatureFault_2 = 0xA012,
            eOverTemperatureFault_3 = 0xA013,
            eCommunicationTimeoutFault = 0xA014,
            eMuxInvalid = 0xA015
        }

        public class ceFaultCodes : Class_Helper.EnumBase<teFaultCodes>
        {
            public ceFaultCodes() { }
        }
    }
}
