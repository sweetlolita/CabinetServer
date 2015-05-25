using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;
using Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Bridge.EqptRoomComm.Protocol.Parser
{
    class MessageBusinessHandler
    {
        private MessageObserver messageHandlerObserver { get; set; }

        public MessageBusinessHandler(MessageObserver messageHandlerObserver)
        {
            if(messageHandlerObserver == null)
            {
                throw new EqptRoomCommException("messageHandlerObserver is null.");
            }
            this.messageHandlerObserver = messageHandlerObserver;
        }

        internal void handleMessage(Guid sessionId, Descriptor descriptor)
        {
            MessageFormatParser parser = new MessageFormatParser(descriptor);

            try
            {
                while(parser.parseIfHasNext())
                {
                    switch (parser.verb())
                    {
                        case "acknowledge":
                            {
                                Acknowledge acknowledge = parser.parseAs<Acknowledge>();
                                messageHandlerObserver.onAcknowledge(sessionId, acknowledge);
                                break;
                            }
                        case "register":
                            {
                                Register register = parser.parseAs<Register>();
                                messageHandlerObserver.onRegister(sessionId, register);
                                break;
                            }
                        case "delivery":
                            {
                                WorkInstructionDeliveryVO workInstructionDeliveryVO = parser.parseAs<WorkInstructionDeliveryVO>();
                                messageHandlerObserver.onDelivery(sessionId, workInstructionDeliveryVO);
                                break;
                            }
                        case "reportWiProcedureResult":
                            {
                                ReportWiProcedureResultTransactionVO reportWiProcedureResultTransactionVO = parser.parseAs<ReportWiProcedureResultTransactionVO>();
                                messageHandlerObserver.onReportWiProcedureResult(sessionId, reportWiProcedureResultTransactionVO);
                                break;
                            }
                        case "updateWiStatus":
                            {
                                UpdateWiStatusTransactionVO updateWiStatusTransactionVO = parser.parseAs<UpdateWiStatusTransactionVO>();
                                messageHandlerObserver.onUpdateWiStatus(sessionId, updateWiStatusTransactionVO);
                                break;
                            }
                        case "updateCabinetStatus":
                            {
                                UpdateCabinetStatusTransactionVO updateCabinetStatusTransactionVO = parser.parseAs<UpdateCabinetStatusTransactionVO>();
                                messageHandlerObserver.onUpdateCabinetStatus(sessionId, updateCabinetStatusTransactionVO);
                                break;
                            }
                        case "sendCabinetAuthorizationLog":
                            {
                                SendCabinetAuthorizationLogTransactionVO sendCabinetAuthorizationLogTransactionVO = parser.parseAs<SendCabinetAuthorizationLogTransactionVO>();
                                messageHandlerObserver.onSendCabinetAuthorizationLog(sessionId, sendCabinetAuthorizationLogTransactionVO);
                                break;
                            }
                        case "requestForCabinetList":
                            {
                                RequestForCabinetListTransactionVO requestForCabinetListTransactionVO = parser.parseAs<RequestForCabinetListTransactionVO>();
                                messageHandlerObserver.onRequestForCabinetList(sessionId, requestForCabinetListTransactionVO);
                                break;
                            }
                        case "deliveryCabinetList":
                            {
                                DeliveryCabinetListVO deliveryCabinetListVO = parser.parseAs<DeliveryCabinetListVO>();
                                messageHandlerObserver.onDeliveryCabinetList(sessionId, deliveryCabinetListVO);
                                break;
                            }
                        case "deliverySystemUpdate":
                            {
                                DeliverySystemUpdateVO deliverySystemUpdateVO = parser.parseAs<DeliverySystemUpdateVO>();
                                messageHandlerObserver.onDeliverySystemUpdate(sessionId, deliverySystemUpdateVO);
                                break;
                            }
                        default:
                            throw new EqptRoomCommException("verb error");

                    }
                }      
            }
            catch (System.Exception ex)
            {
                Logger.error("EqptRoomHub: corrupted data {0}, from session {1}, error: {2}",
                    BitConverter.ToString(descriptor.des, 0, descriptor.desLength),
                    sessionId,
                    ex.Message);
                
            }
        }
    }
}
