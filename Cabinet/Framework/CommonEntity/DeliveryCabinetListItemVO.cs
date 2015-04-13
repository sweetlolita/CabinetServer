using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;

namespace Cabinet.Framework.CommonEntity
{
    public class DeliveryCabinetListItemVO : Jsonable
    {
        public Guid cabGuid { get; set; }
        public string cabNum { get; set; }
    }
}
