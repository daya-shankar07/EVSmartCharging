using Application.SmartCharging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SmartCharging.BL
{
   public interface IConnectorService
    {
        public Task<IEnumerable<ConnectorResponse>> GetAllConnectorAsync();
        public Task<ConnectorResponse> GetConnectorAsync(string id);
        public Task<ConnectorResponse> PostConnectorAsync(ConnectorRequest item, string stationId);
        public Task<ConnectorResponse> UpdateConnectorAsync(ConnectorRequest item, string StationId);
        public Task<ConnectorResponse> DeleteConnectorAsync(string item);
    }
}
