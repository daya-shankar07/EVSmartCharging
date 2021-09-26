using Application.SmartCharging.EFCore.Models;
using Application.SmartCharging.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.SmartCharging.DL
{
    public interface IChargeStationRepository
    {
        public Task<IEnumerable<Cstation>> GetAllAsync();
        public Task<Cstation> GetStationAsync(string id);
        public Task<Cstation> PostAsync(Cstation item, string groupId);
        public Task<Cstation> UpdateAsync(Cstation item, string groupId);
        public Task<Cstation> DeleteAsync(string id);

    }
}
