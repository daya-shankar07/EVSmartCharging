using Application.SmartCharging.BL.Validations;
using Application.SmartCharging.Common;
using Application.SmartCharging.DL;
using Application.SmartCharging.EFCore.Models;
using Application.SmartCharging.Models;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SmartCharging.BL
{
    public class GroupService : IGroupService
    {

        private readonly IGroupRepository _groupRepository;
        private readonly IConfiguration _configuration;
        private readonly ITelemetryAdaptor _telemetryAdaptor;
        private readonly IMapper _mapper;  

        public GroupService(IConfiguration configuration, ITelemetryAdaptor telemetryAdaptor , IMapper mapper ,IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
            _configuration = configuration;
            _telemetryAdaptor = telemetryAdaptor;
            _mapper = mapper;
        }

        public async Task<GroupResponse> DeleteGroupAsync(string id)
        {
            var result = await _groupRepository.DeleteAsync(id);
            var response = _mapper.Map<GroupResponse>(result);
            return response;
        }

        public async Task<IEnumerable<GroupResponse>> GetAllGroupAsync()
        {
           var result =  await _groupRepository.GetAllAsync();
           var response = _mapper.Map<IEnumerable<GroupResponse>>(result);
            return response;
        }

        public async Task<GroupResponse> GetGroupAsync(string id)
        {
            var result = await _groupRepository.GetGroupAsync(id);
            var response = _mapper.Map<GroupResponse>(result);
            return response;
        }

        public async Task<GroupResponse> PostGroupAsync(GroupRequest item)
        {
            var itemToPost = _mapper.Map<Group>(item);
            var result = await _groupRepository.PostAsync(itemToPost);
            var response = _mapper.Map<GroupResponse>(result);
            return response;
        }

        public async Task<GroupResponse> UpdateGroupAsync(GroupRequest item)
        {
            
            var itemToUpdate = _mapper.Map<Group>(item);
            var result = await _groupRepository.UpdateAsync(itemToUpdate);
            var response = _mapper.Map<GroupResponse>(result);
            return response;
        }
    }
}
