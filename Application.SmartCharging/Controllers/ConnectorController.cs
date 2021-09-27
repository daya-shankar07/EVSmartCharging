using Application.SmartCharging.BL;
using Application.SmartCharging.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Application.SmartCharging.Service.Controllers
{
    [ExcludeFromCodeCoverage]
    [Route("api/v1/connector")]
    [ApiController]
    public class ConnectorController : ControllerBase
    {
        private readonly IConnectorService _connectorService;
        public ConnectorController(IConnectorService connectorService)
        {
            _connectorService = connectorService;
        }


        [HttpGet("/connector")]
        [ActionName("/getConnectors")]
        [Produces("application/json", Type = typeof(IEnumerable<ConnectorResponse>))]
        public async Task<IActionResult> GetAllConnectors()
        {
            try
            {
                var result = await _connectorService.GetAllConnectorAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
           
        }


        [HttpGet("/connector/{id}")]
        [ActionName("getConnector")]
        [Produces("application/json", Type = typeof(ConnectorResponse))]
        public async Task<IActionResult> GetConnector(string id, string stationId)
        {
            if (id == null || stationId == null) return BadRequest(String.Format("Please check input {0} & {1}", id, stationId));
            try
            {
                var result = await _connectorService.GetConnectorAsync(id, stationId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete("/connector/{id}")]
        [ActionName("/deleteConnector")]
        [Produces("application/json", Type = typeof(ConnectorResponse))]
        public async Task<IActionResult> DeleteConnector(string id, string stationId)
        {
            if (id == null || stationId == null) return BadRequest(String.Format("Please check input {0} & {1}", id, stationId));
            try
            {
                var result = await _connectorService.DeleteConnectorAsync(id, stationId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("/connector/{stationId}")]
        [ActionName("/createConnector")]
        [Produces("application/json", Type = typeof(ConnectorResponse))]
        public async Task<IActionResult> CreateConnector([FromBody] ConnectorRequest connectorItem , string stationId)
        {
            if (connectorItem == null || stationId == null) return BadRequest(String.Format("Please check input {0} & {1}", connectorItem, stationId));
            try
            {
                var result = await _connectorService.PostConnectorAsync(connectorItem, stationId);
                return Created($"/{result.CStationId}", result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("/connector/{stationId}")]
        [ActionName("/updateConnector")]
        [Produces("application/json", Type = typeof(ConnectorResponse))]
        public async Task<IActionResult> UpdateConnector([FromBody]ConnectorRequest item, string stationId)
        {
            if (item == null || stationId == null) return BadRequest(String.Format("Please check input {0} & {1}", item, stationId));
            try
            {
                var result = await _connectorService.UpdateConnectorAsync(item, stationId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
