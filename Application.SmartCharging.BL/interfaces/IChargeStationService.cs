using Application.SmartCharging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SmartCharging.BL
{
  public  interface IChargeStationService
    {
        public Task<IEnumerable<CstationResponse>> GetAllAsync();
        public Task<CstationResponse> GetStationAsync(string id);
        public Task<CstationResponse> PostAsync(CStationRequest item, string groupId);
        public Task<CstationResponse> UpdateAsync(CStationRequest item, string StationId);
        public Task<CstationResponse> DeleteAsync(string stationId);
    }
}
