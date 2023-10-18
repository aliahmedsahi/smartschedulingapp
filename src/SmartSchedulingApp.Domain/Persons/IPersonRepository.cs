using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace SmartSchedulingApp.Doctors
{
    public interface IPersonRepository : IRepository<Person, Guid>
    {
        Task<List<Person>> GetListAsync();
        Task<Person> GetByUserIdAsync(Guid id);
    }
}
