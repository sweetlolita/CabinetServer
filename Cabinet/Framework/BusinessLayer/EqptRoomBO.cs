using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;
using Cabinet.Framework.CommonModuleEntry;
using Cabinet.Utility;

namespace Cabinet.Framework.BusinessLayer
{
    class EqptRoomBO: BOBase
    {
        public EqptRoomBO(BusinessContext businessContext)
            : base(businessContext)
        {

        }

        public override void handleBusiness()
        {
            switch (context.request.method)
            {
                case "requestForCabinetList":
                    doRequestForCabinetList();
                    break;
                case "deliveryCabinetList":
                    doDeliveryCabinetList();
                    break;
                case "deliverySystemUpdate":
                    doDeliverySystemUpdate();
                    break;
                default:
                    break;
            }
        }


        void doRequestForCabinetList()
        {
            Logger.debug("BusinessServer: business eqptRoom/requestForCabinetList starts.");
            logOnValidatingParams();
            validateParamCount(2);
            validateParamAsSpecificType(0, typeof(Guid));
            Guid transactionGuid = (Guid)context.request.param.ElementAt<object>(0);
            validateParamAsSpecificType(1, typeof(Guid));
            Guid eqptRoomGuid = (Guid)context.request.param.ElementAt<object>(1);

            try
            {
                WcfServiceModuleEntry wcfServiceModuleEntry = CommonModuleGateway.getInstance().wcfServiceModuleEntry;

                string result = wcfServiceModuleEntry.requestForCabinetList(eqptRoomGuid);

                Logger.debug("BusinessServer: got list from webservice : {0} .", result);

                DeliveryCabinetListVO deliveryCabinetListVO = DeliveryCabinetListVO.fromJson<DeliveryCabinetListVO>(result);

                deliveryCabinetListVO.eqptRoomGuid = eqptRoomGuid;

                Logger.debug("BusinessServer: list parsed, send to client");

                CommonModuleGateway.getInstance().eqptRoomCommModuleEntry.deliveryCabinetList(deliveryCabinetListVO);
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
            Logger.debug("BusinessServer: eqptRoom {0} requests cabinet list...", eqptRoomGuid);
            Logger.debug("BusinessServer: feed OK back to EqptRoomComm.");
            CommonModuleGateway.getInstance().eqptRoomCommModuleEntry.acknowledge(
                transactionGuid,
                eqptRoomGuid,
                200,
                "OK");
            Logger.debug("BusinessServer: business eqptRoom/requestForCabinetList ends.");
        }

        //deprecated
        void doDeliveryCabinetList()
        {
            Logger.debug("BusinessServer: business eqptRoom/deliveryCabinetList starts.");
            logOnValidatingParams();
            validateParamCount(1);
            validateParamAsSpecificType(0, typeof(DeliveryCabinetListVO));
            DeliveryCabinetListVO deliveryCabinetListVO = (DeliveryCabinetListVO)context.request.param.ElementAt<object>(0);

            CommonModuleGateway.getInstance().eqptRoomCommModuleEntry.deliveryCabinetList(deliveryCabinetListVO);
            Logger.debug("BusinessServer: business eqptRoom/deliveryCabinetList ends.");
        }

        void doDeliverySystemUpdate()
        {
            Logger.debug("BusinessServer: business eqptRoom/systemUpdate starts.");
            logOnValidatingParams();
            validateParamCount(1);
            validateParamAsSpecificType(0, typeof(DeliverySystemUpdateVO));
            DeliverySystemUpdateVO deliverySystemUpdateVO = (DeliverySystemUpdateVO)context.request.param.ElementAt<object>(0);

            CommonModuleGateway.getInstance().eqptRoomCommModuleEntry.deliverySystemUpdate(deliverySystemUpdateVO);
            Logger.debug("BusinessServer: business eqptRoom/systemUpdate ends.");
        }


    }
}
