using Application.SmartCharging.BL.Validations;
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
        private readonly CommonValidations _commonValidation;

        public ChargeStationService(IConfiguration configuration, ITelemetryAdaptor telemetryAdaptor, IChargeStationRepository cStationRepository, IMapper mapper)
        {
            _configuration = configuration;
            _telemetryAdaptor = telemetryAdaptor;
            _cStationRepository = cStationRepository;
            _mapper= mapper;
            _commonValidation = new CommonValidations(telemetryAdaptor);
        }
        public async Task<CstationResponse> DeleteAsync(string stationId)
        {
            CstationResponse response = new();
            _telemetryAdaptor.TrackEvent(String.Format("DeleteAsync Started for stationId {0}", stationId));
            try
            {
                var result = await _cStationRepository.DeleteAsync(stationId);
                 response = _mapper.Map<CstationResponse>(result);
            }
            catch (Exception ex)
            {
                _telemetryAdaptor.TrackException(ex);
                throw;
            }
            _telemetryAdaptor.TrackEvent(String.Format("DeleteAsync Completed for stationId {0}", stationId));
            return response;
        }

        public async Task<IEnumerable<CstationResponse>> GetAllAsync()
        {
            List<CstationResponse> response = new List<CstationResponse>();
            _telemetryAdaptor.TrackEvent(String.Format("GetAllAsync Started"));
            try
            {
                var result = await _cStationRepository.GetAllAsync();
                response = _mapper.Map<IEnumerable<CstationResponse>>(result).ToList();
            }
            catch (Exception ex)
            {
                _telemetryAdaptor.TrackException(ex);
                throw;
            }
            _telemetryAdaptor.TrackEvent(String.Format("GetAllAsync Completed"));
            return response;
        }

        public async Task<CstationResponse> GetStationAsync(string id)
        {
            _telemetryAdaptor.TrackEvent(String.Format("GetStationAsync Started for Station {0}", id));
            CstationResponse response = new();
            try
            {
                var result = await _cStationRepository.GetStationAsync(id);
                response = _mapper.Map<CstationResponse>(result);
            }
            catch (Exception ex)
            {
                _telemetryAdaptor.TrackException(ex);
                throw;
            }
            _telemetryAdaptor.TrackEvent(String.Format("GetStationAsync Completed for Station {0}", id));
            return response;
        }

        public async Task<CstationResponse> PostAsync(CStationRequest item, string groupId)
        {
            _telemetryAdaptor.TrackEvent(String.Format("PostAsync Started"));
            CstationResponse response = new();
            double totalGroupConnectorsNewCurrent = 0.0;
            try
            {
                // business validation
                var groupConnectorsRunningSum = await _commonValidation.GetGroupMaxCurrentFromGroup(groupId);
                var groupCurrentCapacity = await _commonValidation.GetGroupCurrent(groupId);
                totalGroupConnectorsNewCurrent = groupConnectorsRunningSum + item.Connectors.Count>0 ? item.Connectors.Sum(x => x.MaxCurrent) : 0;
                if (groupCurrentCapacity <= totalGroupConnectorsNewCurrent) return response;

                var itemToPost = _mapper.Map<Cstation>(item);
                itemToPost.GroupId = Guid.Parse(groupId);
                itemToPost.StationId = Guid.NewGuid();
                var result = await _cStationRepository.PostAsync(itemToPost);
                response = _mapper.Map<CstationResponse>(result);
                _telemetryAdaptor.TrackEvent(String.Format("PostAsync Completed for StationId {0}", itemToPost.StationId.ToString()));
            }
            catch (Exception ex)
            {
                _telemetryAdaptor.TrackException(ex);
                throw;
            }
            return response;
        }

        public async Task<CstationResponse> UpdateAsync(CStationRequest item, string groupId)
        {
            CstationResponse response = new();
            _telemetryAdaptor.TrackEvent(String.Format("UpdateAsync Started for group {0} " , groupId));
            try
            {
                var itemToUpdate = _mapper.Map<Cstation>(item);
                itemToUpdate.GroupId = Guid.Parse(groupId);
                var result = await _cStationRepository.UpdateAsync(itemToUpdate);
                 response = _mapper.Map<CstationResponse>(result);
            }
            catch (Exception ex)
            {
                _telemetryAdaptor.TrackException(ex);
                throw;
            }
            _telemetryAdaptor.TrackEvent(String.Format("UpdateAsync Completed for group {0} ", groupId));
            return response;

        }
    }
}
