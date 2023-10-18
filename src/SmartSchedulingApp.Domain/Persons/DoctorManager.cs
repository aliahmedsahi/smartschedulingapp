using System.Threading.Tasks;
using System;
using Volo.Abp.Domain.Services;
using System.Diagnostics.CodeAnalysis;
using SmartSchedulingApp.Timeslots;
using SmartSchedulingApp.Schedules;
using SmartSchedulingApp.Notifications;

namespace SmartSchedulingApp.Doctors
{
    public class DoctorManager : DomainService
    {
        public async Task<Doctor> CreateAsync(
            [NotNull] Guid userId,
            [NotNull] int age,
            [NotNull] string gender,
            [NotNull] string specialization
            )
        {
            var id = GuidGenerator.Create();
            
            return new Doctor(id, userId, gender, age, specialization);
        }

        public Schedule AddSchedule(
            Doctor doctor,
            DateTime date,
            DateTime startTime,
            DateTime endTime,
            bool isAvailable
            )
        {
            return doctor.AddSchedule(date, new Timeslot(startTime, endTime), isAvailable);
        }

        public void AddNotification(
            Doctor doctor,
            Schedule schedule
            )
        {
            string text = $"Your schedule is booked for {schedule.Date:dd/MM/yyyy}, From {schedule.Timeslot.StartTime:hh:mm tt} To {schedule.Timeslot.EndTime:hh:mm tt}";
            doctor.AddNotification(text, NotificationType.Info, doctor.Id);
        }
    }
}
