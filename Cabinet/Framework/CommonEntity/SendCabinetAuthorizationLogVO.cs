using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;

namespace Cabinet.Framework.CommonEntity
{
    public class SendCabinetAuthorizationLogVO : Jsonable
    {
        public Guid cabinetGuid { get; set; }
        public List<CabinetAuthorizationLogItemVO> cabinetAuthorizationLogList { get; set; }

        public string getListJsonForWebService()
        {
            return objectToJson(cabinetAuthorizationLogList);
        }
    }
}
