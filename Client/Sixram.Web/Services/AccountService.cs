using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Sixram.Models.Request;
using Sixram.Models.Response;
using Sixram.Web.Common;
using Sixram.Web.Middleware;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Sixram.Web.Services
{
    public sealed class AccountService(
        HttpClient httpClient, 
        IConfiguration configuration, 
        ILocalStorageService localStorage, 
        AuthenticationStateProvider authStateProvider)
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IConfiguration _configuration = configuration;
        private readonly ILocalStorageService _localStorage = localStorage;
        private readonly AuthenticationStateProvider _authStateProvider = authStateProvider;

        public async Task<LoginResponseModel> Login(LoginRequestModel loginRequest)
        {
            var content = JsonConvert.SerializeObject(loginRequest);
            var bodyContent = new StringContent(content, Encoding.UTF8, Constants.MediaType);
            string requestUri = $"{_configuration.GetSection(Constants.ApiUrl).Value}api/account/login";

            using (var httpResponse = await _httpClient.PostAsync(requestUri, bodyContent))
            {
                var contentTemp = await httpResponse.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<LoginResponseModel>(contentTemp);

                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    await _localStorage.SetItemAsync(Constants.LocalUserDetails, result);
                    await _localStorage.SetItemAsync(Constants.LocalToken, result.Token);

                    ((AuthStateProvider)_authStateProvider).NotifyUserLoggedIn(result.Token);
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Bearer, result.Token);

                    return new LoginResponseModel(
                        isAuthenticated: true,
                        token: result.Token,
                        errorMessage: null);
                }
                else
                {
                    return result;
                }
            }
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync(Constants.LocalToken);
            await _localStorage.RemoveItemAsync(Constants.LocalUserDetails);

            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();

            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}