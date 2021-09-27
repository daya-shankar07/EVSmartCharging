using Application.SmartCharging.Common;
using Application.SmartCharging.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SmartCharging.DL
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ITelemetryAdaptor _telemetry;
        public GroupRepository(ITelemetryAdaptor telemetryAdaptor)
        {
            _telemetry = telemetryAdaptor;

        }
        public async Task<Group> DeleteAsync(string id)
        {
            Group gp = new Group();
            try
            {
                await
                using (var context = new evsolutionContext())
                {
                        context.Remove(context.Groups.Single(x => x.Id.ToString() == id));
                        context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _telemetry.TrackException(ex);
                throw;
            }
            return gp;
        }

        public async Task<IEnumerable<Group>> GetAllAsync()
        {
            List<Group> groups = new List<Group>();
            try
            {
                await
                using(var context =  new evsolutionContext())
                {
                   groups = context.Groups.Include(x => x.Cstations).ToList();
                }
            }
            catch (Exception ex)
            {
                _telemetry.TrackException(ex);
                throw;
            }
            return groups;
        }

        public async Task<Group> GetGroupAsync(string id)
        {
            Group group = new Group();
            try
            {
                await
                using (var context = new evsolutionContext())
                {
                        group = context.Groups.Where(x=>x.Id.ToString().Equals(id)).Include(x => x.Cstations).FirstOrDefault();
                      
                }
            }
            catch (Exception ex)
            {
                _telemetry.TrackException(ex);
                throw;
            }
            return group;
        }

        public async Task<Group> PostAsync(Group item)
        {
            Group gp = new Group();

            try
            {
                await
                using (var context = new evsolutionContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        context.Groups.Add(item);
                        context.SaveChanges();
                        gp = item;
                        transaction.Commit();
                    }
                       
                }
            }
            catch (Exception ex)
            {
                _telemetry.TrackException(ex);
                throw;
            }
            return gp;
        }

        public async Task<Group> UpdateAsync(Group item)
        {
            Group gp = new Group();
            try
            {
                await
                using (var context = new evsolutionContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        var res = context.Groups.Where(x=>x.Id== item.Id).FirstOrDefault();
                        if(res !=null)
                        {
                            List<Cstation> cStationList = res.Cstations != null ? res.Cstations.ToList() : new List<Cstation>();
                            
                            if(item.Cstations.Count>0)
                            {
                                cStationList.AddRange(item.Cstations);
                            }
                            res.Name = item.Name !=null ? item.Name : res.Name;
                            res.Capacity = item.Capacity !=0 ? item.Capacity : res.Capacity;
                            res.Cstations = cStationList;
                            context.Update(res);
                            gp = res;
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

            return gp;
        }

        public async Task<Cstation> GetGroupDataWithConnectorAsync(string groupId)
        {
            Cstation cs = new Cstation();
            Group   group = new Group();
            try
            {
                await
                using (var context = new evsolutionContext())
                {
                    group = context.Groups.Where(x => x.Id.ToString().Equals(groupId)).Include(x => x.Cstations).FirstOrDefault();

                    foreach (var station in group.Cstations)
                    {
                        cs = context.Cstations.Where(x => x.StationId.ToString().Equals(station.StationId)).Include(x => x.Connectors).FirstOrDefault();

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
