﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity
{
    public class SendCabinetAuthorizationLogTransactionVO : TransactionEntityBase
    {
        public SendCabinetAuthorizationLogVO sendCabinetAuthorizationLogVO { get; set; }
    }
}
