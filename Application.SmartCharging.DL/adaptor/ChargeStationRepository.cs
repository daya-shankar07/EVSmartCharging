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
            Cstation cs = new Cstation();
            try
            {
                await
                using (var context = new evsolutionContext())
                {
                    context.Remove(context.Cstations.Single(x => x.StationId.ToString() == id));
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _telemetry.TrackException(ex);
                throw;
            }
            return cs;
        }

        public async Task<IEnumerable<Cstation>> GetAllAsync()
        {
            List<Cstation> cStations = new List<Cstation>();
            try
            {
                await
                using (var context = new evsolutionContext())
                {
                    
                        cStations = context.Cstations.Include(x => x.Connectors).ToList();
                       
                }
            }
            catch (Exception ex)
            {
                _telemetry.TrackException(ex);
                throw;
            }
            return cStations;
        }

        public async Task<Cstation> GetStationAsync(string id)
        {
            Cstation station = new Cstation();
            try
            {
                await
                using (var context = new evsolutionContext())
                {
                    station = context.Cstations.Where(x => x.StationId.Equals(id)).Include(x => x.Connectors).FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                _telemetry.TrackException(ex);
                throw;
            }
            return station;
        }

        public async Task<Cstation> PostAsync(Cstation item, string groupId)
        {
            Cstation cs = new Cstation();

            var sId = Guid.NewGuid();
            item.StationId = sId;
            try
            {
                await
                using (var context = new evsolutionContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        context.Cstations.Add(item);
                        context.SaveChanges();
                        cs = await GetStationAsync(sId.ToString());
                        transaction.Commit();
                    }
                }

            }
            catch (Exception ex)
            {
                _telemetry.TrackException(ex);
                throw;
            }
            return cs;
        }

        public async Task<Cstation> UpdateAsync(Cstation item, string groupId)
        {
            Cstation cs = new Cstation();
            try
            {
                await
                using (var context = new evsolutionContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        var res = context.Cstations.First<Cstation>();
                        res.Name = item.Name;
                        res.Connectors = item.Connectors;

                        context.Update(res);
                        context.SaveChanges();
                        cs = await GetStationAsync(res.StationId.ToString());
                        transaction.Commit();
                    }
                }

            }
            catch (Exception ex)
            {
                _telemetry.TrackException(ex);
                throw;
            }

            return cs;
        }
    }
}
