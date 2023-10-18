using SmartSchedulingApp.Schedules;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SmartSchedulingApp.Doctors
{
    public interface IDoctorAppService : IApplicationService
    {
        Task CreateAsync(PersonCreateDto input);
        Task<PagedResultDto<DoctorDto>> GetListAsync();
        Task<DoctorDto> GetAsync(Guid id);
        Task AddScheduleAsync(Guid id, ScheduleCreateDto schedule);
    }
}
