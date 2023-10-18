using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace SmartSchedulingApp.Pages;

public class Index_Tests : SmartSchedulingAppWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
