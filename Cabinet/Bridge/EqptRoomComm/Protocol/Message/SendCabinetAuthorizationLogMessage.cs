using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity;

namespace Cabinet.Bridge.EqptRoomComm.Protocol.Message
{
    class SendCabinetAuthorizationLogMessage : MessageBase
    {
        public SendCabinetAuthorizationLogMessage(SendCabinetAuthorizationLogTransactionVO sendCabinetAuthorizationLogTransactionVO)
        {
            verb = "sendCabinetAuthorizationLog";
            payload = sendCabinetAuthorizationLogTransactionVO.toJson();
        }
    }
}
