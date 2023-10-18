using SmartSchedulingApp.Notifications;
using SmartSchedulingApp.Schedules;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace SmartSchedulingApp.Doctors;

public class PersonDto : EntityDto<Guid>
{

    public string Gender { get; set; }

    public int Age { get; set; }

    public Guid UserId { get; set; }

    public IdentityUserDto? User { get; set; }

    public List<ScheduleDto> Schedules { get; set; }

    public List<NotificationDto> Notifications { get; set; }
}

public class DoctorDto : PersonDto
{
    public string Specialization { get; set; }
}

public class StaffDto : PersonDto
{
    public string Department { get; set; }
}

public class ManagerDto : PersonDto
{
    public string Team { get; set; }
}