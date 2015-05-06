using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Cabinet.Utility;
using Cabinet.Framework.CommonEntity;
using Cabinet.Framework.CommonModuleEntry;

namespace Cabinet.Bridge.WcfService
{
    public class WcfServer : WcfServiceModuleEntry
    {
        private ServiceHost serviceHostWorkInstructionService;
        private ServiceHost serviceHostEqptRoom;
        private static int contractedWiStatusProceeding = 1;
        private static int contractedWiStatusComplete = 2;
        private static int contractedWiStatusFail = 3;
        private static int contractedWiStatusChecked = 4;
        private static int contractedWiStatusDelivered = 5;
        private static int contractedWiStatusInternalServerError = 6;

        private static int contractedCabinetStatusIdle = 1;
        private static int contractedCabinetStatusTimeLimitExceeeded = 2;
        private static int contractedCabinetStatusInExecution = 3;
        private static int contractedCabinetStatusError = 4;
        public WcfServer()
        {
            //serviceHostWorkInstructionService = new ServiceHost(typeof(WorkInstructionService));
            serviceHostEqptRoom = new ServiceHost(typeof(EqptRoomService));
        }
        public void start()
        {
            Logger.debug("WcfServer: starting...");
            try
            {
                //serviceHostWorkInstructionService.Open();
                serviceHostEqptRoom.Open();
            }
            catch (System.Exception ex)
            {
                Logger.debug("WcfServer: error on start. {0}.", ex.Message);
            }
            Logger.debug("WcfServer: start.");
        }

        public void stop()
        {
            Logger.debug("WcfServer: stopping...");
            try
            {
                //serviceHostWorkInstructionService.Close();
                serviceHostEqptRoom.Close();
            }
            catch (System.Exception ex)
            {
                Logger.debug("WcfServer: error on start. {0}.", ex.Message);
            }
            Logger.debug("WcfServer: stop.");

        }



        public void reportWiProcedureResult(ReportWiProcedureResultVO reportWiProcedureResultVO)
        {
            Logger.info("WcfServer: AxisServer =====> WcfServer.");
            Logger.info("WcfServer: WcfServer - - -> Webservice.");
            WebComm.WebServerService webComm = new WebComm.WebServerService();
            string isSuccessString = reportWiProcedureResultVO.isSuccess ? "true" : "false";
            webComm.executeResultInfo(reportWiProcedureResultVO.procedureGuid.ToString(), isSuccessString);
            Logger.info("WcfServer: <3<3<3 Wcf Client Transaction Completed.");
            Logger.info("WcfServer: WcfServer =====> Webservice.");
        }

        private void reportWiStatus(Guid wiGuid, int wiStatus)
        {
            Logger.info("WcfServer: AxisServer =====> WcfServer.");
            Logger.info("WcfServer: WcfServer - - -> Webservice.");
            WebComm.WebServerService webComm = new WebComm.WebServerService();
            webComm.updateWorkInstrStatus(wiGuid.ToString(), wiStatus);
            Logger.info("WcfServer: <3<3<3 Wcf Client Transaction Completed.");
            Logger.info("WcfServer: WcfServer =====> Webservice.");
        }

        public void updateWiStatusAsProceeding(Guid wiGuid)
        {
            reportWiStatus(wiGuid, contractedWiStatusProceeding);
        }

        public void updateWiStatusAsComplete(Guid wiGuid)
        {
            reportWiStatus(wiGuid, contractedWiStatusComplete);
        }

        public void updateWiStatusAsFail(Guid wiGuid)
        {
            reportWiStatus(wiGuid, contractedWiStatusFail);
        }


        public void updateWiStatusAsDelivered(Guid wiGuid)
        {
            reportWiStatus(wiGuid, contractedWiStatusDelivered);
        }

        public void updateWiStatusAsChecked(Guid wiGuid)
        {
            reportWiStatus(wiGuid, contractedWiStatusChecked);
        }

        public void updateWiStatusAsInternalServerError(Guid wiGuid)
        {
            reportWiStatus(wiGuid, contractedWiStatusInternalServerError);
        }

        private void reportCabinetStatus(Guid cabinetGuid, int cabinetStatus)
        {
            Logger.info("WcfServer: AxisServer =====> WcfServer.");
            Logger.info("WcfServer: WcfServer - - -> Webservice.");
            WebComm.WebServerService webComm = new WebComm.WebServerService();
            webComm.updateCabStatus(cabinetGuid.ToString(), cabinetStatus);
            Logger.info("WcfServer: <3<3<3 Wcf Client Transaction Completed.");
            Logger.info("WcfServer: WcfServer =====> Webservice.");
        }

        public void updateCabinetStatusAsIdle(Guid cabinetGuid)
        {
            reportCabinetStatus(cabinetGuid, contractedCabinetStatusIdle);
        }

        public void updateCabinetStatusAsTimeLimitExceeeded(Guid cabinetGuid)
        {
            reportCabinetStatus(cabinetGuid, contractedCabinetStatusTimeLimitExceeeded);
        }

        public void updateCabinetStatusAsInExecution(Guid cabinetGuid)
        {
            reportCabinetStatus(cabinetGuid, contractedCabinetStatusInExecution);
        }

        public void updateCabinetStatusAsError(Guid cabinetGuid)
        {
            reportCabinetStatus(cabinetGuid, contractedCabinetStatusError);
        }

        public void requestCabinetListByEqptRoom(Guid eqptRoomGuid)
        {
            Logger.info("WcfServer: AxisServer =====> WcfServer.");
            Logger.info("WcfServer: WcfServer - - -> Webservice.");
            WebComm.WebServerService webComm = new WebComm.WebServerService();
            webComm.getCabInfoItem(eqptRoomGuid.ToString());
            Logger.info("WcfServer: <3<3<3 Wcf Client Transaction Completed.");
            Logger.info("WcfServer: WcfServer =====> Webservice.");
        }


        public void sendCabinetAuthorizationLog(SendCabinetAuthorizationLogVO sendCabinetAuthorizationLogVO)
        {
            Logger.info("WcfServer: AxisServer =====> WcfServer.");
            Logger.info("WcfServer: WcfServer - - -> Webservice.");
            WebComm.WebServerService webComm = new WebComm.WebServerService();
            webComm.updateCabCardInfo(sendCabinetAuthorizationLogVO.cabinetGuid.ToString(),
                sendCabinetAuthorizationLogVO.getListJsonForWebService());
            Logger.info("WcfServer: <3<3<3 Wcf Client Transaction Completed.");
            Logger.info("WcfServer: WcfServer =====> Webservice.");
        }


        public string requestForCabinetList(Guid eqptRoomGuid)
        {
            Logger.info("WcfServer: AxisServer =====> WcfServer.");
            Logger.info("WcfServer: WcfServer - - -> Webservice.");
            WebComm.WebServerService webComm = new WebComm.WebServerService();
            string result =  webComm.getCabInfoItem(eqptRoomGuid.ToString()) as string;
            Logger.info("WcfServer: <3<3<3 Wcf Client Transaction Completed.");
            Logger.info("WcfServer: WcfServer =====> Webservice.");
            return result;
        }
    }
}
