using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSchedulingApp.Identity.Users;
using SmartSchedulingApp.Doctors;

namespace Volo.Abp.Identity.Web.Pages.Identity.Users;

public class MyCreateModalModel : CreateModalModel
{
    [BindProperty]
    public CreatePersonViewModel Person { get; set; }

    public static readonly List<string> PersonTypes = new() { "Doctor", "Staff", "Manager" };
    public static readonly List<string> Genders = new() { "Male", "Female" };

    private readonly IMyIdentityUserAppService _myIdentityUserAppService;

    public MyCreateModalModel(
        IIdentityUserAppService identityUserAppService,
        IMyIdentityUserAppService myIdentityUserAppService
        )
        : base(identityUserAppService)
    {
        _myIdentityUserAppService = myIdentityUserAppService;
    }

    public override async Task<IActionResult> OnGetAsync()
    {
        UserInfo = new UserInfoViewModel();

        var roleDtoList = (await IdentityUserAppService.GetAssignableRolesAsync()).Items;

        Roles = ObjectMapper.Map<IReadOnlyList<IdentityRoleDto>, AssignedRoleViewModel[]>(roleDtoList);

        foreach (var role in Roles)
        {
            role.IsAssigned = role.IsDefault;
        }

        return Page();
    }

    public override async Task<NoContentResult> OnPostAsync()
    {
        ValidateModel();

        var input = ObjectMapper.Map<UserInfoViewModel, IdentityUserCreateDto>(UserInfo);
        input.RoleNames = Roles.Where(r => r.IsAssigned).Select(r => r.Name).ToArray();
        var person = ObjectMapper.Map<CreatePersonViewModel, PersonCreateDto>(Person);
        person.IdentityUser = input;

        await _myIdentityUserAppService.CreateAsync(person);

        return NoContent();
    }

    public class CreatePersonViewModel
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
    }
}
