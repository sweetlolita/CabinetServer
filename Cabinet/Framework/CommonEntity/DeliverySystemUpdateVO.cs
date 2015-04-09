using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;

namespace Cabinet.Framework.CommonEntity
{
    public class DeliverySystemUpdateVO : Jsonable
    {
        public Guid eqptRoomGuid { get; set; }
        public string encryptedBinaryStream { get; set; }
    }
}
