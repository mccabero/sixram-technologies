using Microsoft.AspNetCore.Components;

namespace Sixram.Web.Common
{
    public class RedirectToLogin : ComponentBase
    {
        [Inject]
        protected NavigationManager NavigationManager { get; set; } = default!;

        protected override void OnAfterRender(bool firstRender)
        {
            NavigationManager.NavigateTo("/login", true);
        }
    }
}
