using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Bridge.EqptRoomComm.Protocol.Message
{
    class DeliverySystemUpdateMessage: MessageBase
    {
        public DeliverySystemUpdateMessage(DeliverySystemUpdateVO deliverySystemUpdateVO)
        {
            verb = "deliverySystemUpdate";
            payload = deliverySystemUpdateVO.toJson();
        }
    }
}
