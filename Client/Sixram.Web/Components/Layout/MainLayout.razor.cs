
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Sixram.Web.Services;
using System.Security.Claims;

namespace Sixram.Web.Components.Layout
{
    public partial class MainLayout
    {
        [CascadingParameter]
        protected Task<AuthenticationState> AuthState { get; set; }
        [Inject]
        public AccountService _accountService { get; set; }
        [Inject]
        private NavigationManager _navigationManager { get; set; }
        [Inject]
        ILocalStorageService LocalStorage { get; set; }

        public bool IsLoading { get; set; }
        private ClaimsPrincipal User { get; set; }

        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;

            User = await GetUserPrincipal();

            if (User.Identity.IsAuthenticated)
            {
                var fullName = User.Identity.Name;

                // Sample code to get uder details from local storage
                //var user = await LocalStorage.GetItemAsync<PezaUser>(Utilities.Constant.LocalUserDetails);
            }
            else
            {
                _navigationManager.NavigateTo($"login?returnUrl={Uri.EscapeDataString(_navigationManager.Uri)}");
            }

            IsLoading = false;
            await base.OnInitializedAsync();
        }

        async Task<ClaimsPrincipal> GetUserPrincipal()
        {
            if (User == null)
            {
                User = (await AuthState).User;
            }

            return User;
        }

        async Task LogoutUser()
        {
            await _accountService.Logout();
            _navigationManager.NavigateTo("/Login");
        }
    }
}
