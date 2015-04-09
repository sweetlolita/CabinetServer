using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;
using Cabinet.Framework.CommonModuleEntry;
using Cabinet.Utility;

namespace Cabinet.Framework.BusinessLayer
{
    class CabinetBO : BOBase
    {
        public CabinetBO(BusinessContext businessContext)
            : base(businessContext)
        {

        }

        public override void handleBusiness()
        {
            switch (context.request.method)
            {
                case "updateCabinetStatus":
                    doUpdateCabinetStatus();
                    break;
                case "sendCabinetAuthorizationLog":
                    doSendCabinetAuthorizationLog();
                    break;
                default:
                    break;
            }
        }



        void doUpdateCabinetStatus()
        {
            Logger.debug("BusinessServer: business cabinet/updateCabinetStatus starts.");
            logOnValidatingParams();
            validateParamCount(3);
            validateParamAsSpecificType(0, typeof(Guid));
            Guid transactionGuid = (Guid)context.request.param.ElementAt<object>(0);
            validateParamAsSpecificType(1, typeof(Guid));
            Guid eqptRoomGuid = (Guid)context.request.param.ElementAt<object>(1);
            validateParamAsSpecificType(2, typeof(UpdateCabinetStatusVO));
            UpdateCabinetStatusVO updateCabinetStatusVO = context.request.param.ElementAt<object>(2) as UpdateCabinetStatusVO;

            try
            {
                WcfServiceModuleEntry wcfServiceModuleEntry = CommonModuleGateway.getInstance().wcfServiceModuleEntry;

                switch (updateCabinetStatusVO.status)
                {
                    case UpdateCabinetStatusVO.idle:
                        wcfServiceModuleEntry.updateCabinetStatusAsIdle(updateCabinetStatusVO.cabinetGuid);
                        break;
                    case UpdateCabinetStatusVO.busy:
                        wcfServiceModuleEntry.updateCabinetStatusAsBusy(updateCabinetStatusVO.cabinetGuid);
                        break;
                    case UpdateCabinetStatusVO.ready:
                        wcfServiceModuleEntry.updateCabinetStatusAsReady(updateCabinetStatusVO.cabinetGuid);
                        break;
                    case UpdateCabinetStatusVO.error:
                        wcfServiceModuleEntry.updateCabinetStatusAsError(updateCabinetStatusVO.cabinetGuid);
                        break;
                }
            }
            catch (System.Exception ex)
            {
                Logger.debug("BusinessServer: feed ERROR back to EqptRoomComm.");
                CommonModuleGateway.getInstance().eqptRoomCommModuleEntry.acknowledge(
                    transactionGuid,
                    eqptRoomGuid,
                    500,
                    ex.Message);
                throw ex;
            }


            logOnFillingResult();
            Logger.debug("BusinessServer: reported cabinet {0} as {1}...", updateCabinetStatusVO.cabinetGuid, updateCabinetStatusVO.status);
            Logger.debug("BusinessServer: feed OK back to EqptRoomComm.");
            CommonModuleGateway.getInstance().eqptRoomCommModuleEntry.acknowledge(
                transactionGuid,
                eqptRoomGuid,
                200,
                "OK");
            Logger.debug("BusinessServer: business cabinet/updateCabinetStatus ends.");
        }

        void doSendCabinetAuthorizationLog()
        {
            Logger.debug("BusinessServer: business cabinet/sendCabinetAuthorizationLog starts.");
            logOnValidatingParams();
            validateParamCount(3);
            validateParamAsSpecificType(0, typeof(Guid));
            Guid transactionGuid = (Guid)context.request.param.ElementAt<object>(0);
            validateParamAsSpecificType(1, typeof(Guid));
            Guid eqptRoomGuid = (Guid)context.request.param.ElementAt<object>(1);
            validateParamAsSpecificType(2, typeof(SendCabinetAuthorizationLogVO));
            SendCabinetAuthorizationLogVO sendCabinetAuthorizationLogVO = context.request.param.ElementAt<object>(2) as SendCabinetAuthorizationLogVO;

            try
            {
                WcfServiceModuleEntry wcfServiceModuleEntry = CommonModuleGateway.getInstance().wcfServiceModuleEntry;

                wcfServiceModuleEntry.sendCabinetAuthorizationLog(sendCabinetAuthorizationLogVO);
            }
            catch (System.Exception ex)
            {
                Logger.debug("BusinessServer: feed ERROR back to EqptRoomComm.");
                CommonModuleGateway.getInstance().eqptRoomCommModuleEntry.acknowledge(
                    transactionGuid,
                    eqptRoomGuid,
                    500,
                    ex.Message);
                throw ex;
            }


            logOnFillingResult();
            Logger.debug("BusinessServer: log sent: {0}...", sendCabinetAuthorizationLogVO.toJson());
            Logger.debug("BusinessServer: feed OK back to EqptRoomComm.");
            CommonModuleGateway.getInstance().eqptRoomCommModuleEntry.acknowledge(
                transactionGuid,
                eqptRoomGuid,
                200,
                "OK");
            Logger.debug("BusinessServer: business cabinet/sendCabinetAuthorizationLog ends.");
        }

    }
}
