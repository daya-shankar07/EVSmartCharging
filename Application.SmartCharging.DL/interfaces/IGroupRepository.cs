using Application.SmartCharging.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SmartCharging.DL
{
   public interface IGroupRepository
    {
        public Task<IEnumerable<Group>> GetAllAsync();
        public Task<Group> GetGroupAsync(string id);
        public Task<Group> PostAsync(Group item);
        public Task<Group> UpdateAsync(Group item);
        public Task<Group> DeleteAsync(string id);
        public Task<Cstation> GetGroupDataWithConnectorAsync(string groupId);
    }
}
