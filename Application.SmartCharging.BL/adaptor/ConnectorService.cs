using Application.SmartCharging.Common;
using Application.SmartCharging.DL;
using Application.SmartCharging.EFCore.Models;
using Application.SmartCharging.Models;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SmartCharging.BL
{
    public class ConnectorService : IConnectorService
    {
        private readonly IConfiguration _configuration;
        private readonly ITelemetryAdaptor _telemetryAdaptor;
        private readonly IConnectorRepository _connectorRepository;
        private readonly IMapper _mapper;
        public ConnectorService(IConfiguration configuration, ITelemetryAdaptor telemetryAdaptor, IConnectorRepository connectorRepository, IMapper mapper)
        {
            _configuration = configuration;
            _telemetryAdaptor = telemetryAdaptor;
            _connectorRepository = connectorRepository;
            _mapper = mapper;

        }
        public async Task<ConnectorResponse> DeleteConnectorAsync(string connectorId, string stationId)
        {

            var result = await _connectorRepository.DeleteAsync(connectorId,stationId);
            var response = _mapper.Map<ConnectorResponse>(result);
            return response;
         
        }

        public async Task<IEnumerable<ConnectorResponse>> GetAllConnectorAsync()
        {
            var result = await _connectorRepository.GetAllAsync();
            var response = _mapper.Map<IEnumerable<ConnectorResponse>>(result);
            return response;
        }

        public async Task<ConnectorResponse> GetConnectorAsync(string connectorId, string stationId)
        {
            var result = await _connectorRepository.GetConnectorAsync(connectorId,stationId);
            var response = _mapper.Map<ConnectorResponse>(result);
            return response;
        }

        public async Task<ConnectorResponse> PostConnectorAsync(ConnectorRequest item, string stationId)
        {

            var itemToPost = _mapper.Map<Connector>(item);
            var result = await _connectorRepository.PostAsync(itemToPost, stationId);
            var response = _mapper.Map<ConnectorResponse>(result);
            return response;

        }

        public async Task<ConnectorResponse> UpdateConnectorAsync(ConnectorRequest item, string stationId)
        {
            var itemToUpdate = _mapper.Map<Connector>(item);
            var result = await _connectorRepository.UpdateAsync(itemToUpdate, stationId);
            var response = _mapper.Map<ConnectorResponse>(result);
            return response;
        }
    }
}
