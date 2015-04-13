using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;

namespace Cabinet.Framework.CommonEntity
{
    public class CabinetAuthorizationLogItemVO : Jsonable
    {
        public const string OK = "OK";
        public const string error = "error";
        public DateTime timeStamp { get; set; }
        public string cardNumber { get; set; }
        public string status { get; set; }
    }
}
