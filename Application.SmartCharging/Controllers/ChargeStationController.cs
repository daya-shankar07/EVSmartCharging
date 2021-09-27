using Application.SmartCharging.BL;
using Application.SmartCharging.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Application.SmartCharging.Service.Controllers
{
    [ExcludeFromCodeCoverage]
    [Route("api/v1/chargeStation")]
    [ApiController]
    public class ChargeStationController : ControllerBase
    {
        private readonly IChargeStationService _cStationService;
        public ChargeStationController(IChargeStationService cStationService)
        {
            _cStationService = cStationService;
        }


        [HttpGet("/station")]
        [ActionName("/getStations")]
        [Produces("application/json", Type = typeof(IEnumerable<CstationResponse>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAllStations()
        {
            try
            {
                var result = await _cStationService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("/station/{id}")]
        [ActionName("getStation")]
        [Produces("application/json", Type = typeof(CstationResponse))]
        public async Task<IActionResult> GetChargeStation(string id)
        {
            if (id == null) return BadRequest(String.Format("Please check input {0}", id));
            try
            {
                var result = await _cStationService.GetStationAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete("/station/{id}")]
        [ActionName("/deleteStation")]
        [Produces("application/json", Type = typeof(CstationResponse))]
        public async Task<IActionResult> DeleteChargeStation(string id)
        {
            if (id == null) return BadRequest(String.Format("Please check input {0}", id));
            try
            {
                var result = await _cStationService.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("/station/{groupId}")]
        [ActionName("/createGroup")]
        [Produces("application/json", Type = typeof(CstationResponse))]
        public async Task<IActionResult> CreateChargeStation([FromBody]CStationRequest item, string groupId)
        {
            if (item == null || groupId == null) return BadRequest(String.Format("Please check input {0} & {1}", item, groupId));
            try
            {
                var result = await _cStationService.PostAsync(item, groupId);
                return Created($"/{result.StationId}", result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("/station/{id}")]
        [ActionName("/updateStation")]
        [Produces("application/json", Type = typeof(CstationResponse))]
        public async Task<IActionResult> UpdatChargeStation([FromBody]CStationRequest item, string groupId)
        {
            if (item == null || groupId == null) return BadRequest(String.Format("Please check input {0} & {1}", item, groupId));
            try
            {
                var result = await _cStationService.UpdateAsync(item, groupId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
