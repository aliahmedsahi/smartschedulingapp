using SmartSchedulingApp.Doctors;
using System.Threading.Tasks;
using System;
using Volo.Abp.Domain.Services;
using System.Diagnostics.CodeAnalysis;
using SmartSchedulingApp.Timeslots;
using SmartSchedulingApp.Notifications;
using SmartSchedulingApp.Schedules;
using System.Linq;
using Volo.Abp;

namespace SmartSchedulingApp.Persons
{
    public class StaffManager : DomainService
    {
        public async Task<Staff> CreateAsync(
            [NotNull] Guid userId,
            [NotNull] int age,
            [NotNull] string gender,
            [NotNull] string department
            )
        {
            var id = GuidGenerator.Create();

            return new Staff(id, userId, gender, age, department);
        }

        /// <summary>
        /// Domain Service is preffered over application service, when we need to implement logic,
        /// related to Domain like Data validation, Data integration 
        /// By using this logic in Domain we can ensure that the business rules are enforced 
        /// independently of application layer
        /// </summary>
        public Schedule AddSchedule(
            Staff staff,
            DateTime date,
            DateTime startTime,
            DateTime endTime,
            bool isAvailable
            )
        {
            if (staff.Schedules.Any(s => s.Date.Date == date.Date
            && ((s.Timeslot.StartTime.TimeOfDay >= startTime.TimeOfDay && s.Timeslot.StartTime.TimeOfDay < endTime.TimeOfDay)
            || (s.Timeslot.EndTime.TimeOfDay > startTime.TimeOfDay && s.Timeslot.EndTime.TimeOfDay <= endTime.TimeOfDay)
            || (s.Timeslot.StartTime.TimeOfDay <= startTime.TimeOfDay && s.Timeslot.EndTime.TimeOfDay >= endTime.TimeOfDay)
            || (s.Timeslot.StartTime.TimeOfDay > startTime.TimeOfDay && s.Timeslot.EndTime.TimeOfDay < endTime.TimeOfDay))))
            {
                throw new BusinessException("Schedule Collides With Existing Schedules");
            }
            return staff.AddSchedule(date, new Timeslot(startTime, endTime), isAvailable);
        }

        public void AddNotification(
            Staff staff,
            Schedule schedule
            )
        {
            string text = $"Your schedule is booked for {schedule.Date:dd/MM/yyyy}, From {schedule.Timeslot.StartTime:hh:mm tt} To {schedule.Timeslot.EndTime:hh:mm tt}";
            staff.AddNotification(text, NotificationType.Info, staff.Id);
        }
    }
}
