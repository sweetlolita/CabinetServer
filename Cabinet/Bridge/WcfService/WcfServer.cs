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
        private ServiceHost serviceHost;

        private static int contractedWiStatusProceeding = 1;
        private static int contractedWiStatusComplete = 2;
        private static int contractedWiStatusFail = 3;
        private static int contractedWiStatusDelivered = 4;
        private static int contractedWiStatusChecked = 5;
        private static int contractedWiStatusInternalServerError = 6;

        private static int contractedCabinetStatusIdle = 1;
        private static int contractedCabinetStatusBusy = 3;
        private static int contractedCabinetStatusReady = 2;
        private static int contractedCabinetStatusError = 4;
        public WcfServer()
        {
            serviceHost = new ServiceHost(typeof(WorkInstructionService));
        }
        public void start()
        {
            Logger.debug("WcfServer: starting...");
            try
            {
                serviceHost.Open();
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
                serviceHost.Close();
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

        public void updateCabinetStatusAsBusy(Guid cabinetGuid)
        {
            reportCabinetStatus(cabinetGuid, contractedCabinetStatusBusy);
        }

        public void updateCabinetStatusAsReady(Guid cabinetGuid)
        {
            reportCabinetStatus(cabinetGuid, contractedCabinetStatusReady);
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


        public void requestForCabinetList(Guid eqptRoomGuid)
        {
            Logger.info("WcfServer: AxisServer =====> WcfServer.");
            Logger.info("WcfServer: WcfServer - - -> Webservice.");
            WebComm.WebServerService webComm = new WebComm.WebServerService();
            webComm.getCabInfoItem(eqptRoomGuid.ToString());
            Logger.info("WcfServer: <3<3<3 Wcf Client Transaction Completed.");
            Logger.info("WcfServer: WcfServer =====> Webservice.");
        }
    }
}
