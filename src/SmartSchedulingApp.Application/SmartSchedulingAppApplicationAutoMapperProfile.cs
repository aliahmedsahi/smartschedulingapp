using AutoMapper;
using SmartSchedulingApp.Doctors;
using SmartSchedulingApp.Notifications;
using SmartSchedulingApp.Schedules;
using SmartSchedulingApp.Timeslots;

namespace SmartSchedulingApp;

public class SmartSchedulingAppApplicationAutoMapperProfile : Profile
{
    public SmartSchedulingAppApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Person, PersonDto>();
        CreateMap<Doctor, DoctorDto>();
        CreateMap<Person, DoctorDto>();
        CreateMap<Person, StaffDto>();
        CreateMap<Staff, StaffDto>();
        CreateMap<Schedule, ScheduleDto>();
        CreateMap<Timeslot, TimeslotDto>();
        CreateMap<Notification, NotificationDto>();
    }
}
