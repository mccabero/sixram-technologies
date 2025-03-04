using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using Sixram.Models.Request;
using Sixram.Models.Response;
using Sixram.Web.Services;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Web;

namespace Sixram.Web.Components.Pages
{
    public partial class Login
    {
        [CascadingParameter]
        protected Task<AuthenticationState> AuthState { get; set; }
        [Inject]
        public AccountService _accountService { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }
        [Inject]
        public ILocalStorageService _localStorageService { get; set; }

        private ClaimsPrincipal User { get; set; }
        public string ReturnUrl { get; set; }
        private LoginRequestModel LoginRequestModel { get; set; }
        private LoginResponseModel LoginResponseModel { get; set; }

        private string ErrorMessage = string.Empty;
        private MudTextField<string> pwField1;

        protected override async Task OnInitializedAsync()
        {
            LoginRequestModel = new LoginRequestModel(string.Empty, string.Empty);
            LoginResponseModel = new LoginResponseModel(false, string.Empty, string.Empty);

            User = await GetUserPrincipal();

            if (User.Identity.IsAuthenticated)
            {
                _navigationManager.NavigateTo("/");
            }

            await base.OnInitializedAsync();
        }

        private async Task LoginUser()
        {
            var result = await _accountService.Login(LoginRequestModel);

            if (result.IsAuthenticated)
            {
                var absoluteUri = new Uri(_navigationManager.Uri);
                var queryParam = HttpUtility.ParseQueryString(absoluteUri.Query);
                ReturnUrl = queryParam["returnUrl"];

                if (string.IsNullOrEmpty(ReturnUrl))
                {
                    _navigationManager.NavigateTo("/", true);
                }
                else
                {
                    _navigationManager.NavigateTo(ReturnUrl, true);
                }
            }
            else
            {
                ErrorMessage = result.ErrorMessage;
            }
        }

        private IEnumerable<string> PasswordStrength(string pw)
        {
            if (string.IsNullOrWhiteSpace(pw))
            {
                yield return "Password is required!";
                yield break;
            }
            if (pw.Length < 8)
                yield return "Password must be at least of length 8";
            if (!Regex.IsMatch(pw, @"[A-Z]"))
                yield return "Password must contain at least one capital letter";
            if (!Regex.IsMatch(pw, @"[a-z]"))
                yield return "Password must contain at least one lowercase letter";
            if (!Regex.IsMatch(pw, @"[0-9]"))
                yield return "Password must contain at least one digit";
        }

        async Task<ClaimsPrincipal> GetUserPrincipal()
        {
            return (await AuthState).User;
        }
    }
}
