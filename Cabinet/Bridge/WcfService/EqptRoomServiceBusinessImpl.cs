using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;
using Cabinet.Framework.CommonEntity;
using Cabinet.Bridge.WcfService.CommonEntity;

namespace Cabinet.Bridge.WcfService
{
    class EqptRoomServiceBusinessImpl : ServiceBusinessImplBase
    {
        public EqptRoomServiceBusinessImpl()
        {
            baseRequest.business = "eqptRoom";
        }

        public string deliveryCabinetList(string deliveryCabinetListObject)
        {
            baseRequest.method = "deliveryCabinetList";
            Logger.debug("WcfServer: comming request = {0}/{1} wiObj = {2}",
                baseRequest.business, baseRequest.method, deliveryCabinetListObject);
            logOnPreparingRequest();

            DeliveryCabinetListVO vo = DeliveryCabinetListVO.fromJson<DeliveryCabinetListVO>(deliveryCabinetListObject);

            baseRequest.param.Add(vo);
            commitAndWait();
            if (baseResponse.isSuccess == false)
            {
                Logger.debug("WcfServer: business server returns error: {0}", baseResponse.errorMessage);
                return new WSResponseErrorBase(baseResponse.errorMessage).toJson();
            }
            Logger.debug("WcfServer: business server returns success.");
            logOnParsingResponse();
            WSResponseSuccessBase response = new WSResponseSuccessBase();
            return response.toJson();
        }

        public string deliverySystemUpdate(string deliverySystemUpdateObject)
        {
            baseRequest.method = "deliveryCabinetList";
            Logger.debug("WcfServer: comming request = {0}/{1} wiObj = {2}",
                baseRequest.business, baseRequest.method, deliverySystemUpdateObject);
            logOnPreparingRequest();

            DeliverySystemUpdateVO vo = DeliverySystemUpdateVO.fromJson<DeliverySystemUpdateVO>(deliverySystemUpdateObject);

            baseRequest.param.Add(vo);
            commitAndWait();
            if (baseResponse.isSuccess == false)
            {
                Logger.debug("WcfServer: business server returns error: {0}", baseResponse.errorMessage);
                return new WSResponseErrorBase(baseResponse.errorMessage).toJson();
            }
            Logger.debug("WcfServer: business server returns success.");
            logOnParsingResponse();
            WSResponseSuccessBase response = new WSResponseSuccessBase();
            return response.toJson();
        }
    }
}
