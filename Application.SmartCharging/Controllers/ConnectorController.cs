using Application.SmartCharging.BL;
using Application.SmartCharging.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Application.SmartCharging.Service.Controllers
{
    [ExcludeFromCodeCoverage]
    [Route("api/v1/connector")]
    [ApiController]
    public class ConnectorController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IConnectorService _connectorService;
        public ConnectorController(ILogger logger,IConnectorService connectoryService)
        {
            _logger = logger;
            _connectorService = connectoryService;
        }


        [HttpGet("/connector")]
        [ActionName("/getConnectors")]
        [Produces("application/json", Type = typeof(IEnumerable<ConnectorResponse>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IEnumerable<ConnectorResponse>> GetAllConnectors()
        {
            var result = await _connectorService.GetAllConnectorAsync();
            return result;
        }


        [HttpGet("/connector/{id}")]
        [ActionName("getConnector")]
        [Produces("application/json", Type = typeof(ConnectorResponse))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<ConnectorResponse> GetConnector(string id, string stationId)
        {
            var result = await _connectorService.GetConnectorAsync(id,stationId);
            return result;
        }


        [HttpDelete("/connector/{id}")]
        [ActionName("/deleteConnector")]
        [Produces("application/json", Type = typeof(ConnectorResponse))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<ConnectorResponse> DeleteConnector(string id, string stationId)
        {
            var result = await _connectorService.DeleteConnectorAsync(id,stationId);
            return result;
        }


        [HttpPost("/connector/{stationId}")]
        [ActionName("/createConnector")]
        [Produces("application/json", Type = typeof(ConnectorResponse))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ConnectorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<ConnectorResponse> CreateGroup([FromBody] ConnectorRequest connectorItem , string stationId)
        {
            var result = await _connectorService.PostConnectorAsync(connectorItem, stationId);
            return result;
        }


        [HttpPut("/connector/{id}")]
        [ActionName("/updateConnector")]
        [Produces("application/json", Type = typeof(ConnectorResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ConnectorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<ConnectorResponse> UpdateGroup([FromBody] ConnectorRequest item, string stationId)
        {
            var result = await _connectorService.UpdateConnectorAsync(item, stationId);
            return result;
        }
    }
}
