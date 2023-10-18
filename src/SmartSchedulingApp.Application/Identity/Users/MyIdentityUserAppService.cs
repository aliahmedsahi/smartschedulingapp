using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SmartSchedulingApp.Doctors;
using SmartSchedulingApp.Staffs;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace SmartSchedulingApp.Identity.Users
{
    public class MyIdentityUserAppService : IdentityUserAppService, IMyIdentityUserAppService, ITransientDependency
    {
        private readonly IDoctorAppService _doctorAppService;
        private readonly IStaffAppService _staffAppService;


        /// <summary>
        /// This is an Example of Dependency Injection
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="userRepository"></param>
        /// <param name="roleRepository"></param>
        /// <param name="identityOptions"></param>
        /// <param name="doctorAppService"></param>
        /// <param name="staffAppService"></param>
        public MyIdentityUserAppService(
            IdentityUserManager userManager, 
            IIdentityUserRepository userRepository, 
            IIdentityRoleRepository roleRepository, 
            IOptions<IdentityOptions> identityOptions,
            IDoctorAppService doctorAppService,
            IStaffAppService staffAppService
            ) : base(userManager, userRepository, roleRepository, identityOptions)
        {
            _doctorAppService = doctorAppService;
            _staffAppService = staffAppService;
        }

        public async Task<IdentityUserDto> CreateAsync(PersonCreateDto input)
        {
            var user = await base.CreateAsync(input.IdentityUser);

            input.UserId = user.Id;

            if(input.PersonType == "Doctor")
            {
                await _doctorAppService.CreateAsync(input);
            }
            else
            {
                await _staffAppService.CreateAsync(input);  
            }
            return user;
        }
    }
}
