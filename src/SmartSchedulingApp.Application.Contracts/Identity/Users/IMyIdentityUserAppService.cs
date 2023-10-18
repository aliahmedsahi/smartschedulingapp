using SmartSchedulingApp.Doctors;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace SmartSchedulingApp.Identity.Users
{
    public interface IMyIdentityUserAppService : IIdentityUserAppService
    {
        Task<IdentityUserDto> CreateAsync(PersonCreateDto input);
    }
}
