using Application.SmartCharging.Common;
using Application.SmartCharging.DL;
using System;
using System.Threading.Tasks;

namespace Application.SmartCharging.BL.Validations
{
    public class CommonValidations 
    {
        private readonly IGroupRepository _groupService ;
        private readonly ITelemetryAdaptor _telemetryAdaptor ;

        public CommonValidations(IGroupRepository groupRepository, ITelemetryAdaptor telemetryAdaptor)
        {
            _groupService = groupRepository;
            _telemetryAdaptor = telemetryAdaptor;
        }

        public async Task<double> ValidateGroupMaxCurrentLimit(string groupId)
        {
            _telemetryAdaptor.TrackEvent(String.Format("ValidateGroupMaxCurrentLimit Started for GroupId {0}", groupId));
            double connectorMaxCurrentSum = 0;
            try
            {
                if (string.IsNullOrEmpty(groupId)) return connectorMaxCurrentSum;

                var groupData = await _groupService.GetGroupDataWithConnectorAsync(groupId);
                foreach (var conn in groupData.Connectors)
                {
                    connectorMaxCurrentSum = (double)(connectorMaxCurrentSum + conn.MaxCurrent);
                }
            }
            catch (Exception ex)
            {
                _telemetryAdaptor.TrackException(ex);
                throw;
            }
            _telemetryAdaptor.TrackEvent(String.Format("ValidateGroupMaxCurrentLimit Completed for GroupId {0}", groupId));
            return connectorMaxCurrentSum;

        }
    }
}
