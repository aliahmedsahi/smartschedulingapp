using SmartSchedulingApp.Doctors;
using SmartSchedulingApp.Persons;
using System;
using System.Threading.Tasks;

namespace SmartSchedulingApp.Web.Pages;

public class IndexModel : SmartSchedulingAppPageModel
{
    private readonly IPersonAppService _personAppService;
    public PersonDto Person { get; set; }

    public IndexModel(
        IPersonAppService personAppService
        )
    {
        _personAppService = personAppService;
        Person = new();
    }

    public async Task OnGetAsync()
    {
        var isAdmin = CurrentUser?.UserName?.ToLower() == "admin";
        if (CurrentUser?.UserName != null && !isAdmin)
        {
            await GetPerson(CurrentUser.Id.Value);
        }
    }

    async Task GetPerson(Guid id)
    {
        Person = await _personAppService.GetByUserIdAsync(id);
    }
}
