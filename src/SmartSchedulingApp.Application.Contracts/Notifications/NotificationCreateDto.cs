using SmartSchedulingApp.Doctors;
using SmartSchedulingApp.Schedules;

namespace SmartSchedulingApp.Notifications
{
    public class NotificationCreateDto
    {
        public PersonDto Person { get; set; }
        public ScheduleDto Schedule { get; set; }
    }
}
