using AutoMapper;
using SmartSchedulingApp.Doctors;
using SmartSchedulingApp.Schedules;
using SmartSchedulingApp.Timeslots;
using SmartSchedulingApp.Web.Pages.Persons;
using static Volo.Abp.Identity.Web.Pages.Identity.Users.MyCreateModalModel;

namespace SmartSchedulingApp.Web;

public class SmartSchedulingAppWebAutoMapperProfile : Profile
{
    public SmartSchedulingAppWebAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Web project.
        CreateMap<CreatePersonViewModel, PersonCreateDto>();
        CreateMap<DoctorDto, PersonDto>();
        CreateMap<StaffDto, PersonDto>();
        CreateMap<ScheduleViewModel, ScheduleCreateDto>();
        CreateMap<TimeslotViewModel, TimeslotDto>();
    }
}
