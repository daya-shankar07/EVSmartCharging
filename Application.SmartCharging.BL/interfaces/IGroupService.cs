using Application.SmartCharging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SmartCharging.BL
{
  public interface IGroupService
    {
        public Task<IEnumerable<GroupResponse>> GetAllGroupAsync();
        public Task<GroupResponse> GetGroupAsync(string id);
        public Task<GroupResponse> PostGroupAsync(GroupRequest item);
        public Task<GroupResponse> UpdateGroupAsync(GroupRequest item, string groupId);
        public Task<GroupResponse> DeleteGroupAsync(string id);
    }
}
