using SmartSchedulingApp.Notifications;
using SmartSchedulingApp.Schedules;
using SmartSchedulingApp.Timeslots;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace SmartSchedulingApp.Doctors;

/// <summary>
/// We have created a Person class as an abstract base class so we can inherit other classes like
/// Doctor, staff and Manger from it and they will inherit these properties and furhter define theirs
/// However, to keep things simple in database we have only one table AppPersons, that has all the required
/// fields and store the PersonType to discriminate between these.
/// This is an Aggregate and Root Object because it is the boundary of this Aggregate.
/// Anything inside Person can only be accessed through Person.
/// So avoid duplication we have further Overridden the Abp User Page and App Service so that,
/// we can create person in the same page as AbpUsers, and bind Person to User, to avoid duplication of
/// basic properties like FirstName, LastName and Login details etc.
/// </summary>

public abstract class Person : FullAuditedAggregateRoot<Guid>
{
    public string Gender { get; private set; }

    public int Age { get; private set; }

    public Guid UserId { get; private set; }

    [ForeignKey(nameof(UserId))]
    public IdentityUser? User { get; set; }

    /// <summary>
    /// This _schedules in following Encapsulation and it can only be modified
    /// within Person, the instance that is being returned and is Public is Readonly
    /// and Same goes for Notifications
    /// </summary>
    private readonly List<Schedule> _schedules = new();
    public IReadOnlyCollection<Schedule> Schedules => _schedules.AsReadOnly();

    private readonly List<Notification> _notifications = new();
    public IReadOnlyCollection<Notification> Notifications => _notifications.AsReadOnly();


    protected Person(
        Guid id,
        Guid userId,
        string gender,
        int age
        ) : base(id)
    {
        UserId = userId;
        Gender = gender;
        Age = age;
    }

    public Schedule AddSchedule(DateTime date, Timeslot timeslot, bool isAvailable)
    {
        var schedule = new Schedule(date, timeslot, isAvailable);
        _schedules.Add(schedule);
        return schedule;
    }

    public void AddNotification(string text, NotificationType notificationType, Guid personId)
    {
        var notification = new Notification(text, notificationType, personId);
        _notifications.Add(notification);
    }
}

public class Doctor : Person
{
    public string Specialization { get; set; }

    public Doctor(
        Guid id,
        Guid userId,
        string gender,
        int age,
        string specialization
        ) : base(id, userId, gender, age)
    {
        Specialization = specialization;
    }
}

public class Staff : Person
{
    public string Department { get; set; }

    public Staff(
        Guid id,
        Guid userId,
        string gender,
        int age,
        string department
        ) : base(id, userId, gender, age)
    {
        Department = department;
    }
}

public class Manager : Person
{
    public string Team { get; set; }

    public Manager(
        Guid id,
        Guid userId,
        string gender,
        int age,
        string team
        ) : base(id, userId, gender, age)
    {
        Team = team;
    }
}
