using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity;

namespace Cabinet.Bridge.EqptRoomComm.Protocol.Message
{
    class UpdateCabinetStatusMessage : MessageBase
    {
        public UpdateCabinetStatusMessage(UpdateCabinetStatusTransactionVO updateCabinetStatusTransactionVO)
        {
            verb = "updateCabinetStatus";
            payload = updateCabinetStatusTransactionVO.toJson();
        }

    }
}
