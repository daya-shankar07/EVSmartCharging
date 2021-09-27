using Application.SmartCharging.BL;
using Application.SmartCharging.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<IActionResult> GetAllGroups()
        {
            try
            {
                var result = await _groupService.GetAllGroupAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

      
        [HttpGet("/group/{id}")]
        [ActionName("getGroup")]
        [Produces("application/json", Type = typeof(GroupResponse))]
        public async Task<IActionResult> GetGroup(string id)
        {
            if (id == null) return BadRequest(String.Format("Please check input {0}", id));
            try
            {
                var result = await _groupService.GetGroupAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        
        [HttpDelete("/group/{id}")]
        [ActionName("/deleteGroup")]
        [Produces("application/json", Type = typeof(GroupResponse))]
        public async Task<IActionResult> DeleteGroup(string id)
        {
            if (id == null) return BadRequest(String.Format("Please check input {0}", id));
            try
            {
                var result = await _groupService.DeleteGroupAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

       
        [HttpPost("/group")]
        [ActionName("/createGroup")]
        [Produces("application/json", Type = typeof(GroupResponse))]
        public async Task<IActionResult> CreateGroup([FromBody]GroupRequest groupItem)
        {
            if (groupItem == null) return BadRequest(String.Format("Please check input {0}", groupItem));
            try
            {
                var result = await _groupService.PostGroupAsync(groupItem);
                return Created($"/{result.Id}", result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

      
        [HttpPut("/group/{groupId}")]
        [ActionName("/updateGroup")]
        [Produces("application/json", Type = typeof(GroupResponse))]
        public async Task<IActionResult> UpdateGroup([FromBody]GroupRequest item, string groupId)
        {
            if (item == null || groupId == null) return BadRequest(String.Format("Please check input {0} & {1}", item, groupId));
            try
            {
                var result = await _groupService.UpdateGroupAsync(item, groupId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
    }
}
