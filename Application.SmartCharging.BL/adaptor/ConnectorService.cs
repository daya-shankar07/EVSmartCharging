﻿using Application.SmartCharging.Common;
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
            ConnectorResponse response = new();
            _telemetryAdaptor.TrackEvent(String.Format("DeleteConnectorAsync Started for StationId {0} ConnectorId {1} ", stationId, connectorId));
            try
            {
                var result = await _connectorRepository.DeleteAsync(connectorId, stationId);
                response = _mapper.Map<ConnectorResponse>(result);
            }
            catch (Exception ex)
            {
                _telemetryAdaptor.TrackException(ex);
            }
            _telemetryAdaptor.TrackEvent(String.Format("DeleteConnectorAsync Completed for StationId {0} ConnectorId {1} ", stationId, connectorId));

            return response;

        }

        public async Task<IEnumerable<ConnectorResponse>> GetAllConnectorAsync()
        {
            List<ConnectorResponse> response = new();
            _telemetryAdaptor.TrackEvent(String.Format("GetAllConnectorAsync Started"));
            try
            {
                var result = await _connectorRepository.GetAllAsync();
                response = _mapper.Map<IEnumerable<ConnectorResponse>>(result).ToList();
            }
            catch (Exception ex)
            {
                _telemetryAdaptor.TrackException(ex);
            }
            _telemetryAdaptor.TrackEvent(String.Format("GetAllConnectorAsync Completed"));
            return response;
        }

        public async Task<ConnectorResponse> GetConnectorAsync(string connectorId, string stationId)
        {
            _telemetryAdaptor.TrackEvent(String.Format("GetConnectorAsync Started"));
            ConnectorResponse response = new();
            try
            {
                var result = await _connectorRepository.GetConnectorAsync(connectorId, stationId);
                response = _mapper.Map<ConnectorResponse>(result);
            }
            catch (Exception ex)
            {

                _telemetryAdaptor.TrackException(ex);
            }
            _telemetryAdaptor.TrackEvent(String.Format("GetConnectorAsync Completed"));
            return response;
        }

        public async Task<ConnectorResponse> PostConnectorAsync(ConnectorRequest item, string stationId)
        {
            ConnectorResponse response = new();
            _telemetryAdaptor.TrackEvent(String.Format("PostConnectorAsync Started"));
            try
            {
                var itemToPost = _mapper.Map<Connector>(item);
                itemToPost.CstationId = Guid.Parse(stationId);
                var result = await _connectorRepository.PostAsync(itemToPost);
                 response = _mapper.Map<ConnectorResponse>(result);
            }
            catch (Exception ex)
            {
                _telemetryAdaptor.TrackException(ex);
            }
            _telemetryAdaptor.TrackEvent(String.Format("PostConnectorAsync Completed"));
            return response;

        }

        public async Task<ConnectorResponse> UpdateConnectorAsync(ConnectorRequest item, string stationId)
        {
            ConnectorResponse response = new();
            _telemetryAdaptor.TrackEvent(String.Format("UpdateConnectorAsync Started"));
            try
            {
                var itemToUpdate = _mapper.Map<Connector>(item);
                itemToUpdate.CstationId = Guid.Parse(stationId);
                var result = await _connectorRepository.UpdateAsync(itemToUpdate);
                response = _mapper.Map<ConnectorResponse>(result);

            }
            catch (Exception ex)
            {

                throw;
            }
            _telemetryAdaptor.TrackEvent(String.Format("UpdateConnectorAsync Exited"));
            return response;
        }
    }
}
