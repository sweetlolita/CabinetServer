
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
    public abstract class EqptRoomHubMessageExchanger : MessageObserver
    {
        void MessageObserver.onAcknowledge(Guid sessionId, Acknowledge acknowledge)
        {
            throw new EqptRoomCommException("server not supported.");
        }

        void MessageObserver.onRegister(Guid sessionId, Register register)
        {
            onRegisterMessage(sessionId, register);
        }

        void MessageObserver.onDelivery(Guid sessionId, WorkInstructionDeliveryVO workInstructionDeliveryVO)
        {
            throw new EqptRoomCommException("server not supported.");
        }

        void MessageObserver.onReportWiProcedureResult(Guid sessionId, ReportWiProcedureResultTransactionVO reportWiProcedureResultTransactionVO)
        {
            onReportWiProcedureResult(sessionId, reportWiProcedureResultTransactionVO);
        }

        void MessageObserver.onUpdateWiStatus(Guid sessionId, UpdateWiStatusTransactionVO updateWiStatusTransactionVO)
        {
            onUpdateWiStatus(sessionId, updateWiStatusTransactionVO);
        }

        void MessageObserver.onUpdateCabinetStatus(Guid sessionId, UpdateCabinetStatusTransactionVO updateCabinetStatusTransactionVO)
        {
            onUpdateCabinetStatus(sessionId, updateCabinetStatusTransactionVO);
        }

        void MessageObserver.onSendCabinetAuthorizationLog(Guid sessionId, SendCabinetAuthorizationLogTransactionVO sendCabinetAuthorizationLogTransactionVO)
        {
            onSendCabinetAuthorizationLog(sessionId, sendCabinetAuthorizationLogTransactionVO);
        }

        void MessageObserver.onRequestForCabinetList(Guid sessionId, RequestForCabinetListTransactionVO requestForCabinetListTransactionVO)
        {
            onRequestForCabinetList(sessionId, requestForCabinetListTransactionVO);
        }

        void MessageObserver.onDeliveryCabinetList(Guid sessionId, DeliveryCabinetListVO deliveryCabinetListVO)
        {
            throw new EqptRoomCommException("server not supported.");
        }

        void MessageObserver.onDeliverySystemUpdate(Guid sessionId, DeliverySystemUpdateVO deliverySystemUpdateVO)
        {
            throw new EqptRoomCommException("server not supported.");
        }

        protected abstract void doDelivery(WorkInstructionDeliveryVO workInstructionDeliveryVO);
        protected abstract void doDeliveryCabinetList(DeliveryCabinetListVO deliveryCabinetListVO);
        protected abstract void doDeliverySystemUpdate(DeliverySystemUpdateVO deliverySystemUpdateVO);
   
        protected abstract void onRegisterMessage(Guid sessionId, Register register);

        protected abstract void onReportWiProcedureResult(Guid sessionId, ReportWiProcedureResultTransactionVO reportWiProcedureResultTransactionVO);

        protected abstract void onUpdateWiStatus(Guid sessionId, UpdateWiStatusTransactionVO updateWiStatusTransactionVO);

        protected abstract void onUpdateCabinetStatus(Guid sessionId, UpdateCabinetStatusTransactionVO updateCabinetStatusTransactionVO);

        protected abstract void onSendCabinetAuthorizationLog(Guid sessionId, SendCabinetAuthorizationLogTransactionVO sendCabinetAuthorizationLogTransactionVO);

        protected abstract void onRequestForCabinetList(Guid sessionId, RequestForCabinetListTransactionVO requestForCabinetListTransactionVO);





    }
}
