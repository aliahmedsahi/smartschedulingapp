using Microsoft.EntityFrameworkCore;
using SmartSchedulingApp.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SmartSchedulingApp.Doctors
{
    /// <summary>
    /// This is the Repository in DAL.
    /// Repositories in DAL (Data Access Layer) are responsible for communication with database,
    /// It Isolates the Data Access from Business Logic
    /// 
    /// Overall the repository provides modularity, and sepraetes away the underlying database implementations from Business Logic
    /// </summary>
    public class EfCorePersonRepository : EfCoreRepository<SmartSchedulingAppDbContext, Person, Guid>, IPersonRepository
    {
        public EfCorePersonRepository(
            IDbContextProvider<SmartSchedulingAppDbContext> dbContextProvider
            ) : base(dbContextProvider)
        {
        }

        public async Task<Person> GetByUserIdAsync(Guid id)
        {
            var person = await (await GetQueryableAsync())
                .Include(x => x.User)
                .Include(x => x.Notifications.OrderByDescending(n => n.CreationTime))
                .Include(x => x.Schedules.OrderByDescending(s => s.Timeslot.StartTime))
                .FirstOrDefaultAsync(x => x.UserId == id);
            return person;
        }

        public async Task<List<Person>> GetListAsync()
        {
            return await (await GetQueryableAsync())
                .Include(d => d.User)
                .ToListAsync();
        }
    }
}
