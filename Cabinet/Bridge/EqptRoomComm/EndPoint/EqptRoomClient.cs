using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Bridge.Tcp.EndPoint;
using Cabinet.Utility;
using Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity;
using Cabinet.Bridge.EqptRoomComm.Protocol.Message;
using Cabinet.Bridge.EqptRoomComm.Protocol.Parser;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Bridge.EqptRoomComm.EndPoint
{
    public class EqptRoomClient : EqptRoomClientMessageExchanger, TcpEndPointObserver
    {
        private EqptRoomClientObserver eqptRoomClientObserver { get; set; }
        private TcpClient tcpClient { get; set; }
        private MessageBusinessHandler messageHandler { get; set; }
        public EqptRoomClient(EqptRoomClientObserver eqptRoomClientObserver,
            string clientIpAddress, int clientPort,
            string serverIpAddress, int serverPort)
        {
            this.eqptRoomClientObserver = eqptRoomClientObserver;
            tcpClient = new TcpClient(clientIpAddress, clientPort,
                serverIpAddress, serverPort, this);
            messageHandler = new MessageBusinessHandler(this);
        }

        public void start()
        {
            Logger.debug("EqptRoomClient: starting...");
            tcpClient.start();
            Logger.debug("EqptRoomClient: start.");
        }

        public void stop()
        {
            Logger.debug("EqptRoomClient: stopping...");
            tcpClient.stop();
            Logger.debug("EqptRoomClient: stop.");
        }

        void TcpEndPointObserver.onTcpConnected(Guid sessionId)
        {
            if (eqptRoomClientObserver != null)
            {
                eqptRoomClientObserver.onEqptRoomHubConnected();
            }
            
        }

        void TcpEndPointObserver.onTcpData(Guid sessionId, Descriptor descriptor)
        {
            messageHandler.handleMessage(sessionId, descriptor);
        }

        void TcpEndPointObserver.onTcpDisconnected(Guid sessionId)
        {
            if (eqptRoomClientObserver != null)
            {
                eqptRoomClientObserver.onEqptRoomHubDisconnected();
            }
        }

        void TcpEndPointObserver.onTcpError(Guid sessionId, string errorMessage)
        {
            if (eqptRoomClientObserver != null)
            {
                eqptRoomClientObserver.onEqptRoomHubCommunicationError(errorMessage);
            }
        }

        protected override void onAcknowledgeMessage(Acknowledge acknowledge)
        {
            if (eqptRoomClientObserver != null)
            {
                eqptRoomClientObserver.onAcknowledge(acknowledge);
            }
        }

        protected override void onWorkInstrucionDelivery(WorkInstructionDeliveryVO workInstructionDeliveryVO)
        {
            if(eqptRoomClientObserver != null)
            {
                eqptRoomClientObserver.onWorkInstrucionDelivery(workInstructionDeliveryVO);
            }
        }

        protected override void onDeliveryCabinetList(DeliveryCabinetListVO deliveryCabinetListVO)
        {
            if (eqptRoomClientObserver != null)
            {
                eqptRoomClientObserver.onDeliveryCabinetList(deliveryCabinetListVO);
            }
        }

        protected override void onDeliverySystemUpdate(DeliverySystemUpdateVO deliverySystemUpdateVO)
        {
            if (eqptRoomClientObserver != null)
            {
                eqptRoomClientObserver.onDeliverySystemUpdate(deliverySystemUpdateVO);
            }
        }


        public override sealed Guid doRegister(Guid eqptRoomGuid)
        {
            Register registerEntity = new Register();
            registerEntity.eqptRoomGuid = eqptRoomGuid;
            RegisterMessage registerMessage = new RegisterMessage(registerEntity);
            byte[] registerBytes = registerMessage.rawBytes();
            tcpClient.send(registerBytes, 0, registerBytes.Length);
            return registerEntity.transactionGuid;
        }

        public override sealed Guid doUpdateWiStatus(Guid eqptRoomGuid, UpdateWiStatusVO updateWiStatusVO)
        {
            UpdateWiStatusTransactionVO updateWiStatusTransactionVO = new UpdateWiStatusTransactionVO();
            updateWiStatusTransactionVO.eqptRoomGuid = eqptRoomGuid;
            updateWiStatusTransactionVO.updateWiStatusVO = updateWiStatusVO;
            UpdateWiStatusMessage workInstructionReportMessage = new UpdateWiStatusMessage(updateWiStatusTransactionVO);
            byte[] updateWiStatusBytes = workInstructionReportMessage.rawBytes();
            tcpClient.send(updateWiStatusBytes, 0, updateWiStatusBytes.Length);
            return updateWiStatusTransactionVO.transactionGuid;
        }



        public override sealed Guid doReportWiProcedureResult(Guid eqptRoomGuid, ReportWiProcedureResultVO reportWiProcedureResultVO)
        {
            ReportWiProcedureResultTransactionVO reportWiProcedureResultTransactionVO = new ReportWiProcedureResultTransactionVO();
            reportWiProcedureResultTransactionVO.eqptRoomGuid = eqptRoomGuid;
            reportWiProcedureResultTransactionVO.reportWiProcedureResultVO = reportWiProcedureResultVO;
            ReportWiProcedureResultMessage workInstructionProcedureReportMessage = new ReportWiProcedureResultMessage(reportWiProcedureResultTransactionVO);
            byte[] reportWiProcedureResultBytes = workInstructionProcedureReportMessage.rawBytes();
            tcpClient.send(reportWiProcedureResultBytes, 0, reportWiProcedureResultBytes.Length);
            return reportWiProcedureResultTransactionVO.transactionGuid;
        }

        public override sealed Guid doUpdateCabinetStatus(Guid eqptRoomGuid, UpdateCabinetStatusVO updateCabinetStatusVO)
        {
            UpdateCabinetStatusTransactionVO updateCabinetStatusTransactionVO = new UpdateCabinetStatusTransactionVO();
            updateCabinetStatusTransactionVO.eqptRoomGuid = eqptRoomGuid;
            updateCabinetStatusTransactionVO.updateCabinetStatusVO = updateCabinetStatusVO;
            UpdateCabinetStatusMessage updateCabinetStatusMessage = new UpdateCabinetStatusMessage(updateCabinetStatusTransactionVO);
            byte[] updateCabinetStatusBytes = updateCabinetStatusMessage.rawBytes();
            tcpClient.send(updateCabinetStatusBytes, 0, updateCabinetStatusBytes.Length);
            return updateCabinetStatusTransactionVO.transactionGuid;
        }

        public override sealed Guid doSendCabinetAuthorizationLog(Guid eqptRoomGuid, SendCabinetAuthorizationLogVO sendCabinetAuthorizationLogVO)
        {
            SendCabinetAuthorizationLogTransactionVO sendCabinetAuthorizationLogTransactionVO = new SendCabinetAuthorizationLogTransactionVO();
            sendCabinetAuthorizationLogTransactionVO.eqptRoomGuid = eqptRoomGuid;
            sendCabinetAuthorizationLogTransactionVO.sendCabinetAuthorizationLogVO = sendCabinetAuthorizationLogVO;
            SendCabinetAuthorizationLogMessage sendCabinetAuthorizationLogMessage = new SendCabinetAuthorizationLogMessage(sendCabinetAuthorizationLogTransactionVO);
            byte[] sendCabinetAuthorizationLogBytes = sendCabinetAuthorizationLogMessage.rawBytes();
            tcpClient.send(sendCabinetAuthorizationLogBytes, 0, sendCabinetAuthorizationLogBytes.Length);
            return sendCabinetAuthorizationLogTransactionVO.transactionGuid;
        }

        public override sealed Guid doRequestForCabinetList(Guid eqptRoomGuid)
        {
            RequestForCabinetListTransactionVO requestForCabinetListTransactionVO = new RequestForCabinetListTransactionVO();
            requestForCabinetListTransactionVO.eqptRoomGuid = eqptRoomGuid;
            RequestForCabinetListMessage requestForCabinetListMessage = new RequestForCabinetListMessage(requestForCabinetListTransactionVO);
            byte[] requestForCabinetListBytes = requestForCabinetListMessage.rawBytes();
            tcpClient.send(requestForCabinetListBytes, 0, requestForCabinetListBytes.Length);
            return requestForCabinetListTransactionVO.transactionGuid;
        }



    }
}
