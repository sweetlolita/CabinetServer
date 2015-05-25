using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;

namespace Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity
{
    public class TransactionEntityBase : Jsonable
    {
        public Guid transactionGuid { get; set; }
        public Guid eqptRoomGuid { get; set; }
        public TransactionEntityBase()
        {
            this.transactionGuid = Guid.NewGuid();
        }
    }
}
