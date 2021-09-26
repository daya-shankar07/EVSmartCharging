using Application.SmartCharging.EFCore.Models;
using Application.SmartCharging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SmartCharging.DL
{
    public class ConnectorRepository : IConnectorRepository
    {
        public async Task<ConnectorResponse> DeleteAsync(string stationId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ConnectorResponse>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ConnectorResponse> GetConnectorAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<ConnectorResponse> PostAsync(Connector item, string stationId)
        {
            throw new NotImplementedException();
        }

        public async Task<ConnectorResponse> UpdateAsync(Connector item, string stationId)
        {
            throw new NotImplementedException();
        }
    }
}
