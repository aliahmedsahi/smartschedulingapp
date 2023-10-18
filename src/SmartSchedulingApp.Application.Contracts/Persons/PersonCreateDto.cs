using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;
using Volo.Abp.Identity;

namespace SmartSchedulingApp.Doctors
{
    public class PersonCreateDto
    {
        [Required]
        [DisplayName("Person Type")]
        public string PersonType { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public int Age { get; set; }

        public Guid UserId { get; set; }
        public string? Specialization { get; set; }
        public string? Department { get; set; }
        public string? Team { get; set; }

        public IdentityUserCreateDto IdentityUser { get; set; }
    }
}
