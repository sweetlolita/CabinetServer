using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;

namespace Cabinet.Framework.CommonEntity
{
    public class UpdateCabinetStatusVO : Jsonable
    {
        public const string idle = "idle";
        public const string busy = "busy";
        public const string ready = "ready";
        public const string error = "error";
        public Guid cabinetGuid { get; set; }
        public string status { get; set; }
    }
}
