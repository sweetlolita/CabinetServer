﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity;

namespace Cabinet.Bridge.EqptRoomComm.Protocol.Message
{
    class UpdateWiStatusMessage : MessageBase
    {
        public UpdateWiStatusMessage(UpdateWiStatusTransactionVO updateWiStatusTransactionVO)
        {
            verb = "updateWiStatus";
            payload = updateWiStatusTransactionVO.toJson();
        }
    }
}
