using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Bridge.EqptRoomComm.Protocol.Parser;
using Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity;
using Cabinet.Framework.CommonEntity;
using Cabinet.Utility;

namespace Cabinet.Bridge.EqptRoomComm.EndPoint
{
    public abstract class EqptRoomClientMessageExchanger : MessageObserver
    {
        void MessageObserver.onAcknowledge(Guid sessionId, Acknowledge acknowledge)
        {
            onAcknowledgeMessage(acknowledge);
        }
        void MessageObserver.onRegister(Guid sessionId, Register register)
        {
            throw new EqptRoomCommException("client not supported.");
        }

        void MessageObserver.onDelivery(Guid sessionId, WorkInstructionDeliveryVO workInstructionDeliveryVO)
        {
            onWorkInstrucionDelivery(workInstructionDeliveryVO);
        }

        void MessageObserver.onReportWiProcedureResult(Guid sessionId, ReportWiProcedureResultTransactionVO reportWiProcedureResultTransactionVO)
        {
            throw new EqptRoomCommException("client not supported.");
        }

        void MessageObserver.onUpdateWiStatus(Guid sessionId, UpdateWiStatusTransactionVO updateWiStatusTransactionVO)
        {
            throw new EqptRoomCommException("client not supported.");
        }

        void MessageObserver.onUpdateCabinetStatus(Guid sessionId, UpdateCabinetStatusTransactionVO updateCabinetStatusTransactionVO)
        {
            throw new EqptRoomCommException("client not supported.");
        }

        void MessageObserver.onSendCabinetAuthorizationLog(Guid sessionId, SendCabinetAuthorizationLogTransactionVO sendCabinetAuthorizationLogTransactionVO)
        {
            throw new EqptRoomCommException("client not supported.");
        }

        void MessageObserver.onRequestForCabinetList(Guid sessionId, RequestForCabinetListTransactionVO requestForCabinetListTransactionVO)
        {
            throw new EqptRoomCommException("client not supported.");
        }

        void MessageObserver.onDeliveryCabinetList(Guid sessionId, DeliveryCabinetListVO deliveryCabinetListVO)
        {
            onDeliveryCabinetList(deliveryCabinetListVO);
        }

        void MessageObserver.onDeliverySystemUpdate(Guid sessionId, DeliverySystemUpdateVO deliverySystemUpdateVO)
        {
            onDeliverySystemUpdate(deliverySystemUpdateVO);
        }

        public abstract Guid doRegister(Guid eqptRoomGuid);

        public abstract Guid doUpdateWiStatus(Guid eqptRoomGuid, UpdateWiStatusVO updateWiStatusVO);

        public abstract Guid doReportWiProcedureResult(Guid eqptRoomGuid, ReportWiProcedureResultVO reportWiProcedureResultVO);

        public abstract Guid doUpdateCabinetStatus(Guid eqptRoomGuid, UpdateCabinetStatusVO updateCabinetStatusVO);

        public abstract Guid doSendCabinetAuthorizationLog(Guid eqptRoomGuid, SendCabinetAuthorizationLogVO sendCabinetAuthorizationLogVO);
        
        public abstract Guid doRequestForCabinetList(Guid eqptRoomGuid);


        protected abstract void onAcknowledgeMessage(Acknowledge acknowledge);

        protected abstract void onWorkInstrucionDelivery(WorkInstructionDeliveryVO workInstructionDeliveryVO);

        protected abstract void onDeliveryCabinetList(DeliveryCabinetListVO deliveryCabinetListVO);

        protected abstract void onDeliverySystemUpdate(DeliverySystemUpdateVO deliverySystemUpdateVO);


    }
}
