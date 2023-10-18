using SmartSchedulingApp.Doctors;
using SmartSchedulingApp.Notifications;
using SmartSchedulingApp.Persons;
using SmartSchedulingApp.Schedules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace SmartSchedulingApp.Staffs
{
    public class StaffAppService : ApplicationService, IStaffAppService
    {
        private readonly IPersonRepository _personRepository;
        private readonly StaffManager _staffManager;

        public StaffAppService(
            IPersonRepository personRepository,
            StaffManager staffManager
            )
        {
            _personRepository = personRepository;
            _staffManager = staffManager;
        }

        public async Task CreateAsync(PersonCreateDto input)
        {
            var staff = await _staffManager.CreateAsync(
                                input.UserId,
                                input.Age,
                                input.Gender,
                                input.Department
                                );

            await _personRepository.InsertAsync(staff);
        }

        public async Task<StaffDto> GetAsync(Guid id)
        {
            var doctor = await _personRepository.GetAsync(id);
            await _personRepository.EnsureCollectionLoadedAsync(doctor, d => d.Schedules);

            return ObjectMapper.Map<Person, StaffDto>(doctor);
        }

        public async Task<PagedResultDto<StaffDto>> GetListAsync()
        {
            var staffs = (await _personRepository.GetListAsync())
                            .OfType<Staff>()
                            .ToList();

            return new PagedResultDto<StaffDto>
            {
                TotalCount = staffs.Count,
                Items = ObjectMapper.Map<List<Staff>, List<StaffDto>>(staffs)
            };
        }

        public async Task AddScheduleAsync(Guid id, ScheduleCreateDto schedule)
        {
            var person = await _personRepository.GetAsync(id);
            await _personRepository.EnsureCollectionLoadedAsync(person, d => d.Schedules);
            var staff = ObjectMapper.Map<Person, Staff>(person);
            var newSchedule = _staffManager.AddSchedule(staff, schedule.Date, schedule.Timeslot.StartTime, schedule.Timeslot.EndTime, schedule.IsAvailable);

            await SendNotificationAsync(new NotificationInput { Person = staff, Schedule = newSchedule });
            await _personRepository.UpdateAsync(staff);
        }

        public Task SendNotificationAsync(NotificationInput input)
        {
            var staff = input.Person as Staff;
            _staffManager.AddNotification(staff, input.Schedule);

            // Send Email Notification
            return Task.CompletedTask;
        }
    }
}
