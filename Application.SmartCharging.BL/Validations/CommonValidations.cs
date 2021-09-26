using Application.SmartCharging.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SmartCharging.BL.Validations
{
    public class CommonValidations 
    {
        private readonly IGroupService _groupService ;
        private readonly IChargeStationService _chargeStationService;
        private readonly ITelemetryAdaptor telemetryAdaptor ;

        public CommonValidations(IGroupService groupService, IChargeStationService chargeStationService)
        {
            _groupService = groupService;
            _chargeStationService = chargeStationService;
        }

        public async Task<double> ValidateGroupMaxCurrentLimit(string groupId)
        {
          //  bool isValid = false;
            double connectorMaxCurrentSum = 0;
            try
            {
                if (string.IsNullOrEmpty(groupId)) return connectorMaxCurrentSum;

                var groupData = await _groupService.GetGroupAsync(groupId);
                
                if(groupData != null)
                {
                    var cStationIdList = groupData.ChargingStation.Select(x => x.StationId).ToList();

                    foreach (var id in cStationIdList)
                    {
                        var cResponse = await _chargeStationService.GetStationAsync(id);
                         connectorMaxCurrentSum = connectorMaxCurrentSum + cResponse.Connectors.Select(y => y.MaxCurrent).Sum();
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
            return connectorMaxCurrentSum;

        }
    }
}
