using SmartSchedulingApp.Timeslots;
using System;

namespace SmartSchedulingApp.Schedules
{
    public class ScheduleDto
    {
        public DateTime Date { get; set; }
        public TimeslotDto Timeslot { get; set; }
        public bool IsAvailable { get; set; }
    }
}
