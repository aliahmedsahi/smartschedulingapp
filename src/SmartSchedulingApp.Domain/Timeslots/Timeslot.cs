using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Values;

namespace SmartSchedulingApp.Timeslots;

public class Timeslot : ValueObject
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    private Timeslot()
    {

    }

    public Timeslot(
        DateTime startTime,
        DateTime endTime
        )
    {
        StartTime = startTime;
        EndTime = endTime;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return new object[] { StartTime, EndTime };
    }
}
