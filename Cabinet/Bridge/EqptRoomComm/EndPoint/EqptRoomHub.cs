using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Bridge.Tcp.EndPoint;
using Cabinet.Utility;
using Cabinet.Bridge.EqptRoomComm.Protocol.Parser;
using Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity;
using Cabinet.Bridge.EqptRoomComm.Protocol.Message;
using Cabinet.Framework.CommonEntity;
using Cabinet.Framework.CommonModuleEntry;

namespace Cabinet.Bridge.EqptRoomComm.EndPoint
{
    public class EqptRoomHub : EqptRoomHubMessageExchanger, TcpEndPointObserver, EqptRoomCommModuleEntry
    {
        private TcpServer tcpServer { get; set; }
        private EqptRoomClientMap eqptRoomClientMap { get; set; }
        private MessageBusinessHandler messageHandler { get; set; }
        public EqptRoomHub(string ipAddress, int port)
        {
            tcpServer = new TcpServer(ipAddress, port, this);
            eqptRoomClientMap = new EqptRoomClientMap();
            messageHandler = new MessageBusinessHandler(this);
        }
        void TcpEndPointObserver.onTcpConnected(Guid sessionId)
        {
            Logger.debug("session {0} connected but not registered yet.", sessionId);
        }


        void TcpEndPointObserver.onTcpData(Guid sessionId, Descriptor descriptor)
        {
            messageHandler.handleMessage(sessionId, descriptor);
        }

        void TcpEndPointObserver.onTcpDisconnected(Guid sessionId)
        {
            Logger.debug("session {0} disconnected.", sessionId);
            eqptRoomClientMap.removeBySessionGuid(sessionId);
        }

        void TcpEndPointObserver.onTcpError(Guid sessionId, string errorMessage)
        {
            Logger.error("session {0} encountered an error : {1}.", sessionId, errorMessage);
        }

        public void start()
        {
            Logger.debug("EqptRoomHub: starting...");
            tcpServer.start();
            Logger.debug("EqptRoomHub: start.");
        }

        public void stop()
        {
            Logger.debug("EqptRoomHub: stopping...");
            tcpServer.stop();
            Logger.debug("EqptRoomHub: stop.");
        }

        public void deliveryWorkInstrucion(WorkInstructionDeliveryVO workInstructionDeliveryVO)
        {
            doDelivery(workInstructionDeliveryVO);
        }

        public void deliveryCabinetList(DeliveryCabinetListVO deliveryCabinetListVO)
        {
            doDeliveryCabinetList(deliveryCabinetListVO);
        }

        public void deliverySystemUpdate(DeliverySystemUpdateVO deliverySystemUpdateVO)
        {
            doDeliverySystemUpdate(deliverySystemUpdateVO);
        }

        public void acknowledge(Guid transactionGuid, Guid eqptRoomGuid, int statusCode, string message)
        {
            Acknowledge acknowledgeEntity = new Acknowledge(transactionGuid);
            acknowledgeEntity.eqptRoomGuid = eqptRoomGuid;
            acknowledgeEntity.statusCode = statusCode;
            acknowledgeEntity.message = message;
            AcknowledgeMessage acknowledgeMessage = new AcknowledgeMessage(acknowledgeEntity);
            byte[] acknowledgeMessageBytes = acknowledgeMessage.rawBytes();
            dispatchDataByEqptRoomGuid(eqptRoomGuid, acknowledgeMessageBytes, 0, acknowledgeMessageBytes.Length);
        }

        public void dispatchDataByEqptRoomGuid(Guid eqptRoomGuid, byte[] data, int offset, int count)
        {
            Guid sessionGuid = eqptRoomClientMap.searchSessionGuid(eqptRoomGuid);
            if (sessionGuid == Guid.Empty)
            {
                throw new EqptRoomCommException("cannot find session, target is probably not registered.");
            }
            else
            {
                dispatchDataBySessionGuid(sessionGuid, data, offset, count);
            }
        }
        
        public void dispatchDataBySessionGuid(Guid sessionGuid, byte[] data, int offset, int count)
        {
            if (sessionGuid == Guid.Empty)
            {
                throw new EqptRoomCommException("error session guid.");
            }
            else
            {
                tcpServer.send(sessionGuid, data, offset, count);
            }
        }

        protected sealed override void onRegisterMessage(Guid sessionId, Register register)
        {
            Logger.debug("EqptRoomHubBusiness: eqpt room guid {0} request register.",
                register.eqptRoomGuid);
            eqptRoomClientMap.put(register.eqptRoomGuid, sessionId);
            acknowledge(register.transactionGuid, register.eqptRoomGuid, 200, "OK");
            Logger.debug("EqptRoomHubBusiness: eqpt room guid {0} register complete.",
                register.eqptRoomGuid);
        }

        protected sealed override void doDelivery(WorkInstructionDeliveryVO workInstructionDeliveryVO)
        {
            WorkInstructionDeliveryMessage workInstructionDeliveryMessage = new WorkInstructionDeliveryMessage(workInstructionDeliveryVO);
            byte[] workInstructionDeliveryBytes = workInstructionDeliveryMessage.rawBytes();
            dispatchDataByEqptRoomGuid(workInstructionDeliveryVO.eqptRoomGuid, workInstructionDeliveryBytes, 0, workInstructionDeliveryBytes.Length);
        }
        protected sealed override void doDeliveryCabinetList(DeliveryCabinetListVO deliveryCabinetListVO)
        {
            DeliveryCabinetListMessage deliveryCabinetListMessage = new DeliveryCabinetListMessage(deliveryCabinetListVO);
            byte[] deliveryCabinetListBytes = deliveryCabinetListMessage.rawBytes();
            dispatchDataByEqptRoomGuid(deliveryCabinetListVO.eqptRoomGuid, deliveryCabinetListBytes, 0, deliveryCabinetListBytes.Length);
        
        }

        protected sealed override void doDeliverySystemUpdate(DeliverySystemUpdateVO deliverySystemUpdateVO)
        {
            DeliverySystemUpdateMessage deliverySystemUpdateMessage = new DeliverySystemUpdateMessage(deliverySystemUpdateVO);
            byte[] deliverySystemUpdateBytes = deliverySystemUpdateMessage.rawBytes();
            dispatchDataByEqptRoomGuid(deliverySystemUpdateVO.eqptRoomGuid, deliverySystemUpdateBytes, 0, deliverySystemUpdateBytes.Length);
        
        }
   

        protected sealed override void onReportWiProcedureResult(Guid sessionId, ReportWiProcedureResultTransactionVO reportWiProcedureResultTransactionVO)
        {
            new WorkInstrucionServiceBusinessImpl().reportWiProcedureResult(reportWiProcedureResultTransactionVO);
        }

        protected sealed override void onUpdateWiStatus(Guid sessionId, UpdateWiStatusTransactionVO updateWiStatusTransactionVO)
        {
            new WorkInstrucionServiceBusinessImpl().updateWiStatus(updateWiStatusTransactionVO);
        }

        protected sealed override void onUpdateCabinetStatus(Guid sessionId, UpdateCabinetStatusTransactionVO updateCabinetStatusTransactionVO)
        {
            new CabinetServiceBusinessImpl().updateCabinetStatus(updateCabinetStatusTransactionVO);
        }

        protected override void onSendCabinetAuthorizationLog(Guid sessionId, SendCabinetAuthorizationLogTransactionVO sendCabinetAuthorizationLogTransactionVO)
        {
            new CabinetServiceBusinessImpl().sendCabinetAuthorizationLog(sendCabinetAuthorizationLogTransactionVO);
        }

        protected override void onRequestForCabinetList(Guid sessionId, RequestForCabinetListTransactionVO requestForCabinetListTransactionVO)
        {
            new EqptRoomServiceBusinessImpl().requestForCabinetList(requestForCabinetListTransactionVO);
        }



    }
}
