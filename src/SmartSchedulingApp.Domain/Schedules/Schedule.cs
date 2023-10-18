using SmartSchedulingApp.Timeslots;
using System;
using Volo.Abp.Domain.Entities;

namespace SmartSchedulingApp.Schedules;

public class Schedule : Entity<Guid>
{
    public DateTime Date { get; set; }

    /// <summary>
    /// Here, Timeslot is being treated as a Value Object
    /// </summary>
    public Timeslot Timeslot { get; set; }
    public bool IsAvailable { get; set; }

    private Schedule()
    {

    }

    public Schedule(
        DateTime date,
        Timeslot timeslot,
        bool isAvailable
        )
    {
        Date = date;
        Timeslot = timeslot;
        IsAvailable = isAvailable;
    }
}
