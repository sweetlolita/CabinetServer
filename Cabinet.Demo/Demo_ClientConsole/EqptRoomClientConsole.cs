using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;
using Cabinet.Bridge.EqptRoomComm.EndPoint;
using Cabinet.Framework.CommonEntity;
using Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity;
using System.Threading;

namespace Cabinet.Demo.ClientConsole
{
    class EqptRoomClientConsole : EqptRoomClientObserver
    {
        EqptRoomClient s;
        Guid eqptRoomGuid;
        public void entry()
        {
            eqptRoomGuid = new Guid("53840397-DD43-4CD9-9852-0BBA443FF6CD");
            s = new EqptRoomClient(this,
                "127.0.0.1", 6382, "127.0.0.1", 8135);
            s.start();

            ConsoleKeyInfo ch;
            do
            {
                ch = Console.ReadKey();
                switch (ch.Key)
                {
                    case ConsoleKey.S:
                        {
                            Guid logUsedGuid = s.doRegister(eqptRoomGuid);
                            Logger.debug("a doRegister submitted with transId {0}.", logUsedGuid);
                            break;
                        }
                    case ConsoleKey.C:
                        {
                            s.start();
                            break;
                        }
                    case ConsoleKey.D:
                        {
                            s.stop();
                            break;
                        }
                    case ConsoleKey.L:
                        {
                            s.doRequestForCabinetList(eqptRoomGuid);
                            break;
                        }
                    case ConsoleKey.U:
                        {
                            var o = new UpdateCabinetStatusVO{
                                cabinetGuid = new Guid(
                                    "AABF79CB-46E2-4B62-8B28-39D74A70CCA1"),
                                status = UpdateCabinetStatusVO.idle
                            };
                            s.doUpdateCabinetStatus(eqptRoomGuid, o);
                            break;
                        }
                    case ConsoleKey.A:
                        {
                            var o = new SendCabinetAuthorizationLogVO
                            {
                                cabinetGuid = new Guid(
                                    "AABF79CB-46E2-4B62-8B28-39D74A70CCA1"),
                                cabinetAuthorizationLogList = new List<CabinetAuthorizationLogItemVO> {
                                    new CabinetAuthorizationLogItemVO
                                    {    
                                         timeStamp = DateTime.Now,
                                         cardNumber = "123",
                                         status = CabinetAuthorizationLogItemVO.OK
                                    },
                                    new CabinetAuthorizationLogItemVO
                                    {    
                                         timeStamp = DateTime.Now,
                                         cardNumber = "456",
                                         status = CabinetAuthorizationLogItemVO.error
                                    }
                                }
                            };
                            s.doSendCabinetAuthorizationLog(eqptRoomGuid, o);
                            break;
                        }
                    default:
                        break;
                }
            } while (ch.Key != ConsoleKey.Q);
            s.stop();
        }

        public void onAcknowledge(Acknowledge acknowledge)
        {
            Logger.debug("transaction {0} returns result {1} with message {2} in EqptRoom {3}",
                acknowledge.transactionGuid,
                acknowledge.statusCode,
                acknowledge.message,
                acknowledge.eqptRoomGuid);
        }

        public void onWorkInstrucionDelivery(WorkInstructionDeliveryVO workInstructionDeliveryVO)
        {
            Guid logUsedGuid;

            UpdateWiStatusVO workInstructionReportVO1 = new UpdateWiStatusVO();
            workInstructionReportVO1.workInstructionGuid = workInstructionDeliveryVO.wiGuid;
            workInstructionReportVO1.status = UpdateWiStatusVO.proceeding;
            logUsedGuid = s.doUpdateWiStatus(eqptRoomGuid, workInstructionReportVO1);
            Logger.debug("a doUpdateWiStatus submitted with transId {0}.",
                logUsedGuid);

            //no longer needed
            /*
            foreach (WorkInstructionProcedureVO workInstructionProcedureVO in workInstructionDeliveryVO.procedureList)
            {
                ReportWiProcedureResultVO workInstructionProcedureReportVO = new ReportWiProcedureResultVO();
                workInstructionProcedureReportVO.procedureGuid = workInstructionProcedureVO.procedureGuid;
                workInstructionProcedureReportVO.isSuccess = true;
                logUsedGuid = s.doReportWiProcedureResult(eqptRoomGuid, workInstructionProcedureReportVO);
                Logger.debug("a doReportWiProcedureResult submitted with transId {0}.",
                    logUsedGuid);
            }
            */

            UpdateWiStatusVO workInstructionReportVO2 = new UpdateWiStatusVO();
            workInstructionReportVO2.workInstructionGuid = workInstructionDeliveryVO.wiGuid;
            workInstructionReportVO2.status = UpdateWiStatusVO.complete;
            logUsedGuid = s.doUpdateWiStatus(eqptRoomGuid, workInstructionReportVO2);
            Logger.debug("a doUpdateWiStatus submitted with transId {0}.",
                logUsedGuid);
        }

        public void onEqptRoomHubConnected()
        {
            Logger.debug("server connected!!!");
            //s.stop();
        }

        public void onEqptRoomHubDisconnected()
        {
            Logger.debug("server disconnected!!!");
            //s.start();
        }


        public void onEqptRoomHubCommunicationError(string errorMessage)
        {
            Logger.error("server error!!!  {0}", errorMessage);

        }


        public void onDeliveryCabinetList(DeliveryCabinetListVO deliveryCabinetListVO)
        {
            Logger.info("onDeliveryCabinetList!!!  {0}", deliveryCabinetListVO.toJson());
        }

        public void onDeliverySystemUpdate(DeliverySystemUpdateVO deliverySystemUpdateVO)
        {
            Logger.info("onDeliverySystemUpdate!!!  {0}", deliverySystemUpdateVO.toJson());
        }
    }

    
}
