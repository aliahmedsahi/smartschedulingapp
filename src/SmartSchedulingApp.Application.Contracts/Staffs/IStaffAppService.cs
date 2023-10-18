using SmartSchedulingApp.Doctors;
using SmartSchedulingApp.Schedules;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SmartSchedulingApp.Staffs
{
    public interface IStaffAppService : IApplicationService
    {
        Task CreateAsync(PersonCreateDto input);
        Task<PagedResultDto<StaffDto>> GetListAsync();
        Task<StaffDto> GetAsync(Guid id);
        Task AddScheduleAsync(Guid id, ScheduleCreateDto schedule);
    }
}
