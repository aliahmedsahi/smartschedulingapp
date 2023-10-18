using SmartSchedulingApp.Doctors;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace SmartSchedulingApp.Persons
{
    public interface IPersonAppService : IApplicationService
    {
        Task<PersonDto> GetByUserIdAsync(Guid id);
    }
}
