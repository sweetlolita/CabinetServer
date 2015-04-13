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

        public string version { get; set; }

        public string encryptedBinary { get; set; }

        public byte[] getBinary()
        {
            return Convert.FromBase64String(encryptedBinary);
        }
    }
}
