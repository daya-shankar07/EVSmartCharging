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
        private readonly CommonValidations _commonValidations;

        public GroupService(IConfiguration configuration, ITelemetryAdaptor telemetryAdaptor , IMapper mapper ,IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
            _configuration = configuration;
            _telemetryAdaptor = telemetryAdaptor;
            _mapper = mapper;
            _commonValidations = new CommonValidations(groupRepository, telemetryAdaptor);
        }

        public async Task<GroupResponse> DeleteGroupAsync(string id)
        {
            GroupResponse response = new();
            _telemetryAdaptor.TrackEvent(String.Format("DeleteGroupAsync Started for GroupId {0}", id));
            try
            {
                if (id == null) return null;
                var result = await _groupRepository.DeleteAsync(id);
                response = _mapper.Map<GroupResponse>(result);
            }
            catch (Exception ex)
            {
                _telemetryAdaptor.TrackException(ex);
            }
            _telemetryAdaptor.TrackEvent(String.Format("DeleteGroupAsync Completed for GroupId {0}", id));
            return response;
        }

        public async Task<IEnumerable<GroupResponse>> GetAllGroupAsync()
        {
            IEnumerable<GroupResponse> response = new List<GroupResponse>();
            _telemetryAdaptor.TrackEvent(String.Format("GetAllGroupAsync Started for GroupId"));
            try
            {
                var result = await _groupRepository.GetAllAsync();
                 response = _mapper.Map<IEnumerable<GroupResponse>>(result);
            }
            catch (Exception ex)
            {
                _telemetryAdaptor.TrackException(ex);
            }
            _telemetryAdaptor.TrackEvent(String.Format("GetAllGroupAsync Completed for GroupId"));
            return response;
        }

        public async Task<GroupResponse> GetGroupAsync(string id)
        {
            GroupResponse response = new();
            _telemetryAdaptor.TrackEvent(String.Format("GetGroupAsync Started for GroupId {0}", id));
            try
            {
                var result = await _groupRepository.GetGroupAsync(id);
                response = _mapper.Map<GroupResponse>(result);
            }
            catch (Exception ex)
            {

                _telemetryAdaptor.TrackException(ex);
            }
            _telemetryAdaptor.TrackEvent(String.Format("GetGroupAsync Completed for GroupId {0}", id));
            return response;
        }

        public async Task<GroupResponse> PostGroupAsync(GroupRequest item)
        {
            GroupResponse response = new();
            _telemetryAdaptor.TrackEvent(String.Format("PostGroupAsync Started"));
            try
            {
                var itemToPost = _mapper.Map<Group>(item);
                itemToPost.Id= Guid.NewGuid();
                var result = await _groupRepository.PostAsync(itemToPost);
                response = _mapper.Map<GroupResponse>(result);
            }
            catch (Exception ex)
            {
                _telemetryAdaptor.TrackException(ex);
            }
            _telemetryAdaptor.TrackEvent(String.Format("PostGroupAsync Completed"));
            return response;
        }

        public async Task<GroupResponse> UpdateGroupAsync(GroupRequest item, string groupId)
        {
            GroupResponse response = new();
            _telemetryAdaptor.TrackEvent(String.Format("UpdateGroupAsync Started for groupId {0}" , groupId));
            try
            {
                var itemToUpdate = _mapper.Map<Group>(item);
                itemToUpdate.Id = Guid.Parse(groupId);
                var result = await _groupRepository.UpdateAsync(itemToUpdate);
                 response = _mapper.Map<GroupResponse>(result);
            }
            catch (Exception ex)
            {
                _telemetryAdaptor.TrackException(ex);
            }
            _telemetryAdaptor.TrackEvent(String.Format("UpdateGroupAsync Completed for groupId {0}", groupId));
            return response;
           
        }
    }
}
