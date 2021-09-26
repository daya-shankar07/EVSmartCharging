using Application.SmartCharging.EFCore.Models;
using Application.SmartCharging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SmartCharging.DL
{
   public interface IConnectorRepository
    {
        public Task<IEnumerable<ConnectorResponse>> GetAllAsync();
        public Task<ConnectorResponse> GetConnectorAsync(string id);
        public Task<ConnectorResponse> PostAsync(Connector item, string stationId);
        public Task<ConnectorResponse> UpdateAsync(Connector item, string stationId);
        public Task<ConnectorResponse> DeleteAsync(string connectorId);
    }
}
