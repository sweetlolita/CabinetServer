using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;

namespace Cabinet.Framework.CommonEntity
{
    public class UpdateWiStatusVO : Jsonable
    {
        public const string proceeding = "proceeding";
        public const string complete = "complete";
        public const string fail = "fail";
        public const string delivered = "delivered";
        public const string checked_ = "checked";
        public const string internalServerError = "internalServerError";
        public Guid workInstructionGuid { get; set; }
        public string status { get; set; }
    }
}
