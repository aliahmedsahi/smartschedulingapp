using SmartSchedulingApp.Timeslots;
using System;

namespace SmartSchedulingApp.Schedules
{
    public class ScheduleCreateDto
    {
        public DateTime Date { get; set; }
        public TimeslotDto Timeslot { get; set; }
        public bool IsAvailable { get; set; }
    }
}
