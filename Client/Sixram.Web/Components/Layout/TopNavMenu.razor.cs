
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Sixram.Web.Services;
using System.Security.Claims;

namespace Sixram.Web.Components.Layout
{
    public partial class TopNavMenu
    {
        [CascadingParameter]
        protected Task<AuthenticationState> AuthState { get; set; }
        [Inject]
        private NavigationManager _navigationManager { get; set; }
        [Inject]
        private AccountService _accountService { get; set; }
        [Inject]
        private AuthenticationStateProvider _authenticationStateProvider { get; set; }
        
        private ClaimsPrincipal UserClaims { get; set; } = new();
        private string FullName { get; set; }
        private string PrimaryRole { get; set; }
        private int RoleCount { get; set; }

        protected override async Task OnInitializedAsync()
        {
            UserClaims = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;

            if (!UserClaims.Identity!.IsAuthenticated)
                return;
            
            var rolesClaim = UserClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (rolesClaim != null)
            {
                var roles = JsonConvert.DeserializeObject<List<string>>(rolesClaim.Value);

                PrimaryRole = roles!.FirstOrDefault()!;
                FullName = UserClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)!.Value;
                RoleCount = roles!.Count;
            }

            await base.OnInitializedAsync();
        }

        public async Task OnLogoutClick()
        {
            await _accountService.Logout();

            _navigationManager.NavigateTo("/login", true);
        }
    }
}