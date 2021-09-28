using Application.SmartCharging.Common;
using Application.SmartCharging.DL;
using System;
using System.Threading.Tasks;

namespace Application.SmartCharging.BL.Validations
{
    public class CommonValidations 
    {
        private readonly IGroupRepository _groupRepository ;
        private readonly ITelemetryAdaptor _telemetryAdaptor ;
        private readonly IConnectorRepository _connectorRepository;
        private readonly IChargeStationRepository _chargeStationRepository;
        private readonly BusinessValidation _business ;

        public CommonValidations(ITelemetryAdaptor telemetryAdaptor)
        {
            _telemetryAdaptor = telemetryAdaptor;
            _groupRepository = new GroupRepository(telemetryAdaptor);
            _connectorRepository = new ConnectorRepository(telemetryAdaptor);
            _chargeStationRepository = new ChargeStationRepository(telemetryAdaptor);
            _business = new BusinessValidation();
        }

        public async Task<double> GetGroupMaxCurrentFromGroup(string groupId)
        {
            _telemetryAdaptor.TrackEvent(String.Format("ValidateGroupMaxCurrentLimit Started for GroupId {0}", groupId));
            double connectorMaxCurrentSum = 0;
            try
            {
                if (string.IsNullOrEmpty(groupId)) return connectorMaxCurrentSum;

                var connectorList = await _groupRepository.GetGroupDataWithConnectorAsync(groupId);
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

        public async Task<BusinessValidation> GetGroupMaxCurrentFromConnector( string ConnectorId, string stationId)
        {
            try
            {
                var connectorInfo = await _connectorRepository.GetConnectorAsync(ConnectorId, stationId);
                var station= await _chargeStationRepository.GetStationAsync(stationId);
                if(station != null)
                {
                     var g = await _groupRepository.GetGroupAsync(station.GroupId.ToString());
                    _business.ConnectorTotalCapacity = await GetGroupMaxCurrentFromGroup(station.GroupId.ToString());
                    _business.GroupCapacity = g != null ? (double)g.Capacity : 0.0;
                    _business.ConnectorCurrentCapacity = connectorInfo !=null ? (double)connectorInfo.MaxCurrent : 0.00;
                }
            }
            catch (Exception ex)
            {
                _telemetryAdaptor.TrackException(ex);
                throw;
            }
            return _business;
        }


        public async Task<double> GetGroupCurrent(string groupId)
        {
            double groupCurrent = 0.0;
            var gData = await _groupRepository.GetGroupAsync(groupId);

            if(gData != null)
            {
                groupCurrent = (double)gData.Capacity;
            }

            return groupCurrent;
        }

    }

    public class BusinessValidation
    {
        public double GroupCapacity { get; set; }
        public double ConnectorTotalCapacity {  get; set; }
        public double ConnectorCurrentCapacity { get; set; }
    }
}
