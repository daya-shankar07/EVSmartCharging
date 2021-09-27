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
                        group = context.Groups.Where(x=>x.Id.Equals(id)).Include(x => x.Cstations).FirstOrDefault();
                      
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

            var gId = Guid.NewGuid();
            item.Id = gId;
            try
            {
                await
                using (var context = new evsolutionContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        context.Groups.Add(item);
                        context.SaveChanges();
                        gp = await GetGroupAsync(gId.ToString());
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
                        var res = context.Groups.First<Group>();
                        res.Name = item.Name;
                        res.Capacity = item.Capacity;

                        context.Update(res);
                        context.SaveChanges();
                        gp = await GetGroupAsync(res.Id.ToString());
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
    }
}
