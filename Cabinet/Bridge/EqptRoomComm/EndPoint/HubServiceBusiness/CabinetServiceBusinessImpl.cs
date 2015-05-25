using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;
using Cabinet.Utility;
using Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity;

namespace Cabinet.Bridge.EqptRoomComm.EndPoint
{
    class CabinetServiceBusinessImpl : EqptRoomHubBusinessBase
    {
        public CabinetServiceBusinessImpl()
        {
            baseRequest.business = "cabinet";
        }

        public void updateCabinetStatus(UpdateCabinetStatusTransactionVO updateCabinetStatusTransactionVO)
        {
            baseRequest.method = "updateCabinetStatus";
            Logger.debug("EqptRoomHub: comming request = {0}/{1} wiObj = {2}",
                baseRequest.business, baseRequest.method, updateCabinetStatusTransactionVO.toJson());

            baseRequest.param.Add(updateCabinetStatusTransactionVO.transactionGuid);
            baseRequest.param.Add(updateCabinetStatusTransactionVO.eqptRoomGuid);
            baseRequest.param.Add(updateCabinetStatusTransactionVO.updateCabinetStatusVO);

            commit();


        }

        public void sendCabinetAuthorizationLog(SendCabinetAuthorizationLogTransactionVO sendCabinetAuthorizationLogTransactionVO)
        {
            baseRequest.method = "sendCabinetAuthorizationLog";
            Logger.debug("EqptRoomHub: comming request = {0}/{1} wiObj = {2}",
                baseRequest.business, baseRequest.method, sendCabinetAuthorizationLogTransactionVO.toJson());

            baseRequest.param.Add(sendCabinetAuthorizationLogTransactionVO.transactionGuid);
            baseRequest.param.Add(sendCabinetAuthorizationLogTransactionVO.eqptRoomGuid);
            baseRequest.param.Add(sendCabinetAuthorizationLogTransactionVO.sendCabinetAuthorizationLogVO);

            commit();
        }
    }
}
