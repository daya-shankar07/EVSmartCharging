using Application.SmartCharging.Common;
using Application.SmartCharging.EFCore.Models;
using Application.SmartCharging.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SmartCharging.DL
{
    public class ConnectorRepository : IConnectorRepository
    {
        private readonly ITelemetryAdaptor _telemetry;
        public ConnectorRepository(ITelemetryAdaptor telemetryAdaptor)
        {
            _telemetry = telemetryAdaptor;
        }

        public async Task<Connector> DeleteAsync(string connectorId, string stationId)
        {
            Connector connector = new Connector();
            try
            {
                await 
                using (var context = new evsolutionContext())
                {
                    context.Remove(context.Connectors.Where(x => x.Id.ToString() ==connectorId && x.CstationId.ToString().Equals(stationId)));
                    context.SaveChanges();
                };
            }
            catch (Exception ex)
            {
                _telemetry.TrackException(ex);
                throw;
            }
            return connector;
        }

        public async Task<IEnumerable<Connector>> GetAllAsync()
        {
            List<Connector> connectorsList = new List<Connector>();
            try
            {
                await
                using (var context = new evsolutionContext())
                {
                    connectorsList = context.Connectors.Include(y => y.Cstation).ToList();
                }
            }
            catch (Exception ex)
            {
                _telemetry.TrackException(ex);
                throw;
            }
            return connectorsList;
        }

        public async Task<Connector> GetConnectorAsync(string connectorId, string stationId)
        {
            Connector connector = new Connector();
            try
            {
                await
                using (var context = new evsolutionContext())
                {
                    connector = context.Connectors.Where(x => x.Id.ToString() == connectorId && x.CstationId.ToString().Equals(stationId)).First();


                }
            }
            catch (Exception ex)
            {
                _telemetry.TrackException(ex);
                throw;
            }
            return connector;
        }

        public async Task<Connector> PostAsync(Connector item)
        {
            Connector cs = new Connector();

            try
            {
                await
                using (var context = new evsolutionContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        context.Connectors.Add(item);
                        cs = item;
                        context.SaveChanges();
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

        public async Task<Connector> UpdateAsync(Connector item)
        {
            Connector cs = new Connector();
            try
            {
                await
                using (var context = new evsolutionContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        var res = context.Connectors.Where(x => x.Id == item.Id && x.CstationId.Equals(item.CstationId)).FirstOrDefault();
                        if(res != null)
                        {
                            res.MaxCurrent = item.MaxCurrent >0 ? item.MaxCurrent : res.MaxCurrent;
                            context.Update(res);
                            cs = res;
                            context.SaveChanges();
                        }
                       
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
