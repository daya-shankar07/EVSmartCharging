using Application.SmartCharging.EFCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.SmartCharging.DL
{
    public interface IConnectorRepository
    {
        public Task<IEnumerable<Connector>> GetAllAsync();
        public Task<Connector> GetConnectorAsync(string connectorId, string stationId);
        public Task<Connector> PostAsync(Connector item, string stationId);
        public Task<Connector> UpdateAsync(Connector item, string stationId);
        public Task<Connector> DeleteAsync(string connectorId, string StationId);
    }
}
