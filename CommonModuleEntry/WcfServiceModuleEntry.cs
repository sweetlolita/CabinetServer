using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Framework.CommonModuleEntry
{
    public interface WcfServiceModuleEntry
    {
        void reportWiProcedureResult(ReportWiProcedureResultVO reportWiProcedureResultVO);
        void updateWiStatusAsProceeding(Guid wiGuid);
        void updateWiStatusAsComplete(Guid wiGuid);
        void updateWiStatusAsFail(Guid wiGuid);
        void updateWiStatusAsDelivered(Guid wiGuid);
        void updateWiStatusAsChecked(Guid wiGuid);
        void updateWiStatusAsInternalServerError(Guid wiGuid);

        string requestForCabinetList(Guid eqptRoomGuid);

        void updateCabinetStatusAsIdle(Guid cabinetGuid);
        void updateCabinetStatusAsBusy(Guid cabinetGuid);
        void updateCabinetStatusAsReady(Guid cabinetGuid);
        void updateCabinetStatusAsError(Guid cabinetGuid);

        void sendCabinetAuthorizationLog(SendCabinetAuthorizationLogVO sendCabinetAuthorizationLogVO);
    }
}
