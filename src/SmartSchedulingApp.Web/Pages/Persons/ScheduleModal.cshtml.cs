using Microsoft.AspNetCore.Mvc;
using SmartSchedulingApp.Doctors;
using SmartSchedulingApp.Schedules;
using SmartSchedulingApp.Staffs;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp;

namespace SmartSchedulingApp.Web.Pages.Persons
{
    public class ScheduleModalModel : SmartSchedulingAppPageModel
    {
        [BindProperty]
        public ScheduleViewModel Schedule { get; set; }
        public PersonDto Person { get; set; }
        public string PersonType { get; set; }
        public string ErrorMessage { get; set; }

        private readonly IDoctorAppService _doctorAppService;
        private readonly IStaffAppService _staffAppService;

        public ScheduleModalModel(
            IDoctorAppService doctorAppService,
            IStaffAppService staffAppService
            )
        {
            _doctorAppService = doctorAppService;
            _staffAppService = staffAppService;
            Person = new PersonDto();
        }

        public async Task OnGetAsync(Guid id, string personType)
        {
            PersonType = personType;

            if(PersonType == "Doctor")
            {
                var doctor = await _doctorAppService.GetAsync(id);
                Person = ObjectMapper.Map<DoctorDto, PersonDto>(doctor);
            }
            else if(PersonType == "Staff")
            {
                var staff = await _staffAppService.GetAsync(id);
                Person = ObjectMapper.Map<StaffDto, PersonDto>(staff);
            }
        }

        public async Task OnPostAsync(string id, string personType)
        {
            try
            {
                ValidateModel();
                var schedule = ObjectMapper.Map<ScheduleViewModel, ScheduleCreateDto>(Schedule);
                var personId = Guid.Parse(id);
                if (personType == "Doctor")
                {
                    await _doctorAppService.AddScheduleAsync(personId, schedule);
                }
                else if (personType == "Staff")
                {
                    await _staffAppService.AddScheduleAsync(personId, schedule);
                }
            }
            catch (BusinessException ex)
            {
                ErrorMessage = ex.GetType().ToString();
            }
        }
    }

    public class ScheduleViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public TimeslotViewModel Timeslot { get; set; }
        public bool IsAvailable { get; set; } = true;

    }

    public class TimeslotViewModel
    {
        [Required]
        [DataType(DataType.DateTime)]
        [DisplayName("Start Time")]
        public DateTime StartTime { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayName("End Time")]
        public DateTime EndTime { get; set; }
    }
}
