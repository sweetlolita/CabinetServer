using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Bridge.EqptRoomComm.Protocol.Message
{
    class DeliveryCabinetListMessage: MessageBase
    {
        public DeliveryCabinetListMessage(DeliveryCabinetListVO deliveryCabinetListVO)
        {
            verb = "deliveryCabinetList";
            payload = deliveryCabinetListVO.toJson();
        }
    }
}
