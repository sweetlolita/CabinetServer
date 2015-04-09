using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity;

namespace Cabinet.Bridge.EqptRoomComm.Protocol.Message
{
    class RequestForCabinetListMessage : MessageBase
    {
        public RequestForCabinetListMessage(RequestForCabinetListTransactionVO requestForCabinetListTransactionVO)
        {
            verb = "requestForCabinetList";
            payload = requestForCabinetListTransactionVO.toJson();
        }
    }
}
