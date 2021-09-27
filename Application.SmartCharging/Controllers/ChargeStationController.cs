using Application.SmartCharging.BL;
using Application.SmartCharging.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
        public async Task<IEnumerable<CstationResponse>> GetAllStations()
        {
            var result = await _cStationService.GetAllAsync();
            return result;
        }


        [HttpGet("/station/{id}")]
        [ActionName("getStation")]
        [Produces("application/json", Type = typeof(CstationResponse))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<CstationResponse> GetChargeStation(string id)
        {
            var result = await _cStationService.GetStationAsync(id);
            return result;
        }


        [HttpDelete("/station/{id}")]
        [ActionName("/deleteStation")]
        [Produces("application/json", Type = typeof(CstationResponse))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<CstationResponse> DeleteChargeStation(string id)
        {
            var result = await _cStationService.DeleteAsync(id);
            return result;
        }


        [HttpPost("/station/{groupId}")]
        [ActionName("/createGroup")]
        [Produces("application/json", Type = typeof(CstationResponse))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CstationResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<CstationResponse> CreateChargeStation([FromBody]CStationRequest item, string groupId)
        {
            var result = await _cStationService.PostAsync(item, groupId);
            return result;
        }


        [HttpPut("/station/{id}")]
        [ActionName("/updateStation")]
        [Produces("application/json", Type = typeof(CstationResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CstationResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<CstationResponse> UpdatChargeStation([FromBody]CStationRequest item, string groupId)
        {
            var result = await _cStationService.UpdateAsync(item, groupId);
            return result;
        }
    }
}
