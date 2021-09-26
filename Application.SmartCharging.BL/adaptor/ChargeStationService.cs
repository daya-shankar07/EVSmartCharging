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
  public class ChargeStationService : IChargeStationService
    {
        private readonly IConfiguration _configuration;
        private readonly ITelemetryAdaptor _telemetryAdaptor;
        private readonly IChargeStationRepository _cStationRepository;
        private readonly IMapper _mapper;

        public ChargeStationService(IConfiguration configuration, ITelemetryAdaptor telemetryAdaptor, IChargeStationRepository cStationRepository, IMapper mapper)
        {
            _configuration = configuration;
            _telemetryAdaptor = telemetryAdaptor;
            _cStationRepository = cStationRepository;
            _mapper= mapper;

        }
        public async Task<CstationResponse> DeleteAsync(string stationId)
        {
            var result = await _cStationRepository.DeleteAsync(stationId);
            var response = _mapper.Map<CstationResponse>(result);
            return response;
        }

        public async Task<IEnumerable<CstationResponse>> GetAllAsync()
        {
            var result = await _cStationRepository.GetAllAsync();
            var response = _mapper.Map<IEnumerable<CstationResponse>>(result);
            return response;
        }

        public async Task<CstationResponse> GetStationAsync(string id)
        {
            var result = await _cStationRepository.GetStationAsync(id);
            var response = _mapper.Map<CstationResponse>(result);
            return response;
        }

        public async Task<CstationResponse> PostAsync(CStationRequest item, string groupId)
        {

            var itemToPost = _mapper.Map<Cstation>(item);
            var result = await _cStationRepository.PostAsync(itemToPost, groupId);
            var response = _mapper.Map<CstationResponse>(result);
            return response;
        }

        public async Task<CstationResponse> UpdateAsync(CStationRequest item, string groupId)
        {
            var itemToUpdate = _mapper.Map<Cstation>(item);
            var result = await _cStationRepository.UpdateAsync(itemToUpdate, groupId);
            var response = _mapper.Map<CstationResponse>(result);
            return response;

        }
    }
}
