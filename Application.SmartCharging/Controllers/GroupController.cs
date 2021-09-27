using Application.SmartCharging.BL;
using Application.SmartCharging.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Application.SmartCharging.Controllers
{
    [ExcludeFromCodeCoverage]
    [ApiController]
    [Route("[api/v1/group]")]
    public class GroupController : ControllerBase
    {
      
        private readonly IGroupService _groupService;
        public GroupController( IGroupService groupService)
        {
            _groupService = groupService;
        }

       
        [HttpGet("/group")]
        [ActionName("/getGroups")]
        [Produces("application/json", Type = typeof(IEnumerable<GroupResponse>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IEnumerable<GroupResponse>> GetAllGroups()
        {
            var result = await _groupService.GetAllGroupAsync();
            return result;
        }

      
        [HttpGet("/group/{id}")]
        [ActionName("getGroup")]
        [Produces("application/json", Type = typeof(GroupResponse))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<GroupResponse> GetGroup(string id)
        {
            var result = await _groupService.GetGroupAsync(id);
            return result;
        }

        
        [HttpDelete("/group/{id}")]
        [ActionName("/deleteGroup")]
        [Produces("application/json", Type = typeof(GroupResponse))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<GroupResponse> DeleteGroup(string id)
        {
            var result = await _groupService.DeleteGroupAsync(id);
            return result;
        }

       
        [HttpPost("/group")]
        [ActionName("/createGroup")]
        [Produces("application/json", Type = typeof(GroupResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GroupResponse))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GroupResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<GroupResponse> CreateGroup([FromBody]GroupRequest groupItem)
        {
            
            var result = await _groupService.PostGroupAsync(groupItem);
            return result;
        }

      
        [HttpPut("/group/{groupId}")]
        [ActionName("/updateGroup")]
        [Produces("application/json", Type = typeof(GroupResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GroupResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<GroupResponse> UpdateGroup([FromBody]GroupRequest item, string groupId)
        {
            var result = await _groupService.UpdateGroupAsync(item, groupId);
            return result;
        }

    }
}
