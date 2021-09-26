using Application.SmartCharging.Common;
using Application.SmartCharging.EFCore.Models;
using Application.SmartCharging.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.SmartCharging.DL
{
    public class ChargeStationRepository : IChargeStationRepository
    {
        private readonly ITelemetryAdaptor _telemetry;
        public ChargeStationRepository(ITelemetryAdaptor telemetryAdaptor)
        {
            _telemetry = telemetryAdaptor;

        }
        public async Task<Cstation> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Cstation>> GetAllAsync()
        {
            List<Cstation> cStations = new List<Cstation>();
            try
            {
                using (var context = new evsolutionContext())
                {
                    
                        cStations = context.Cstations.Include(x => x.Connectors).ToList();
                       
                }
            }
            catch (Exception)
            {

                throw;
            }
            return cStations;
        }

        public async Task<Cstation> GetStationAsync(string id)
        {
            Cstation cStation = new Cstation();
            try
            {
                using (var context = new evsolutionContext())
                {
                   // cStation = context.Groups.Include(x => x.Cstations).FirstOrDefault();

                }
            }
            catch (Exception)
            {

                throw;
            }
            return cStation;
        }

        public async Task<Cstation> PostAsync(Cstation item, string groupId)
        {
            Cstation cs = new Cstation();

            var sId = Guid.NewGuid();
            item.StationId = sId;
            try
            {
                using (var context = new evsolutionContext())
                {
                    context.Cstations.Add(item);
                    context.SaveChanges();
                    cs = await GetStationAsync(sId.ToString());
                }

            }
            catch (Exception)
            {

                throw;
            }
            return cs;
        }

        public async Task<Cstation> UpdateAsync(Cstation item, string groupId)
        {
            Cstation cs = new Cstation();
            try
            {
                using (var context = new evsolutionContext())
                {
                    var res = context.Cstations.ToString(); //.First<Cstation>();
                  //  res.Name = item.Name;

                    context.SaveChanges();
                    cs = await GetStationAsync(""); //GetStationAsync(res.Id.ToString());
                }

            }
            catch (Exception)
            {

                throw;
            }

            return cs;
        }
    }
}
