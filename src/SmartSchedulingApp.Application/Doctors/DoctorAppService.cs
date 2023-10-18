using SmartSchedulingApp.Notifications;
using SmartSchedulingApp.Schedules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace SmartSchedulingApp.Doctors
{
    public class DoctorAppService : ApplicationService, IDoctorAppService
    {
        private readonly IPersonRepository _personRepository;
        private readonly DoctorManager _doctorManager;
        
        /// <summary>
        /// This is an Example of Dependency Injection
        /// </summary>
        /// <param name="personRepository"></param>
        /// <param name="doctorManager"></param>
        public DoctorAppService(
            IPersonRepository personRepository,
            DoctorManager doctorManager
            )
        {
            _personRepository = personRepository;
            _doctorManager = doctorManager;
        }

        public async Task CreateAsync(PersonCreateDto input)
        {
            var doctor = await _doctorManager.CreateAsync(
                                input.UserId,
                                input.Age,
                                input.Gender,
                                input.Specialization
                                );

            await _personRepository.InsertAsync(doctor);
        }

        public async Task<DoctorDto> GetAsync(Guid id)
        {
            var doctor = await _personRepository.GetAsync(id);
            await _personRepository.EnsureCollectionLoadedAsync(doctor, d => d.Schedules);

            return ObjectMapper.Map<Person, DoctorDto>(doctor);
        }

        public async Task<PagedResultDto<DoctorDto>> GetListAsync()
        {
            var doctors = (await _personRepository.GetListAsync())
                            .OfType<Doctor>()
                            .ToList();

            return new PagedResultDto<DoctorDto>
            {
                TotalCount = doctors.Count,
                Items = ObjectMapper.Map<List<Doctor>, List<DoctorDto>>(doctors)
            };
        }

        public async Task AddScheduleAsync(Guid id, ScheduleCreateDto schedule)
        {
            var person = await _personRepository.GetAsync(id);
            await _personRepository.EnsureCollectionLoadedAsync(person, d => d.Schedules);
            var doctor = ObjectMapper.Map<Person, Doctor>(person);
            var newSchedule = _doctorManager.AddSchedule(doctor, schedule.Date, schedule.Timeslot.StartTime, schedule.Timeslot.EndTime, schedule.IsAvailable);

            await SendNotificationAsync(new NotificationInput { Person = doctor, Schedule = newSchedule});
            await _personRepository.UpdateAsync(doctor);
        }

        public Task SendNotificationAsync(NotificationInput input)
        {
            var doctor = input.Person as Doctor;
            _doctorManager.AddNotification(doctor, input.Schedule);

            // Send Email Notification
            return Task.CompletedTask;
        }
    }
}
