using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Cabinet.Bridge.WcfService
{
    class EqptRoomService : ServiceBase, IEqptRoomService
    {
        public string deliveryCabinetList(string deliveryCabinetListObject)
        {
            return service(() => new EqptRoomServiceBusinessImpl().deliveryCabinetList(deliveryCabinetListObject));
        }

        public string deliverySystemUpdate(string deliverySystemUpdateObject)
        {
            return service(() => new EqptRoomServiceBusinessImpl().deliverySystemUpdate(deliverySystemUpdateObject));
        }

        public string wiDelivery(string wiDeliveryObject)
        {
            return service(() => new WorkInstructionServiceBusinessImpl().delivery(wiDeliveryObject));
        }
    }
}
