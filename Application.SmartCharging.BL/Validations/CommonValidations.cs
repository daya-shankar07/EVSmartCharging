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
        private readonly IConnectorRepository _connectorRepository;
        private readonly IChargeStationRepository _chargeStationRepository;

        public CommonValidations(ITelemetryAdaptor telemetryAdaptor)
        {
            _telemetryAdaptor = telemetryAdaptor;
            _groupService = new GroupRepository(telemetryAdaptor);
            _connectorRepository = new ConnectorRepository(telemetryAdaptor);
            _chargeStationRepository = new ChargeStationRepository(telemetryAdaptor);
        }

        public async Task<double> GetGroupMaxCurrentFromGroup(string groupId)
        {
            _telemetryAdaptor.TrackEvent(String.Format("ValidateGroupMaxCurrentLimit Started for GroupId {0}", groupId));
            double connectorMaxCurrentSum = 0;
            try
            {
                if (string.IsNullOrEmpty(groupId)) return connectorMaxCurrentSum;

                var connectorList = await _groupService.GetGroupDataWithConnectorAsync(groupId);
                foreach (var conn in connectorList)
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

        public async Task<double> GetGroupMaxCurrentFromConnector(string stationId)
        {
            double groupMaxCurrent =0.0;
            try
            {
                var station= await _chargeStationRepository.GetStationAsync(stationId);
                if(station != null)
                {
                    groupMaxCurrent = await GetGroupMaxCurrentFromGroup(station.GroupId.ToString());
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return groupMaxCurrent;
        }
    }
}
