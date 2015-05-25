using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;
using Cabinet.Utility;
using Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity;

namespace Cabinet.Bridge.EqptRoomComm.EndPoint
{
    class EqptRoomServiceBusinessImpl : EqptRoomHubBusinessBase
    {
        public EqptRoomServiceBusinessImpl()
        {
            baseRequest.business = "eqptRoom";
        }

        public void requestForCabinetList(RequestForCabinetListTransactionVO requestForCabinetListTransactionVO)
        {
            baseRequest.method = "requestForCabinetList";
            Logger.debug("EqptRoomHub: comming request = {0}/{1} wiObj = {2}",
                baseRequest.business, baseRequest.method, requestForCabinetListTransactionVO.toJson());

            baseRequest.param.Add(requestForCabinetListTransactionVO.transactionGuid);
            baseRequest.param.Add(requestForCabinetListTransactionVO.eqptRoomGuid);

            commit();
        }
    }
}
