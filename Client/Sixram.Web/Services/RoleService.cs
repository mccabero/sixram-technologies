using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sixram.Models.Response;
using Sixram.Web.Common;
using Sixram.Web.ViewModel;

namespace Sixram.Web.Services
{
    public class RoleService(
        HttpClient httpClient, 
        IConfiguration configuration, 
        ILocalStorageService localStorage, 
        AuthenticationStateProvider authStateProvider)
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IConfiguration _configuration = configuration;
        private readonly ILocalStorageService _localStorage = localStorage;
        private readonly AuthenticationStateProvider _authStateProvider = authStateProvider;

        // Get all
        public async Task<IList<RoleViewModel>> GetAllUserRoles()
        {
            string requestUri = $"{_configuration.GetSection(Constants.ApiUrl).Value}api/role/list";
            
            using (var httpResponse = await _httpClient.GetAsync(requestUri))
            {
                var contentTemp = await httpResponse.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IList<RoleViewModel>>(contentTemp);

                return result;
            }
        }

        // Get by id

        // Create

        // Update

        // Delete
        public async Task<GenericApiResponseModel> DeleteRoleById(int id)
        {
            string requestUri = $"{_configuration.GetSection(Constants.ApiUrl).Value}api/role/delete?id={id}";

            using (var httpResponse = await _httpClient.DeleteAsync(requestUri))
            {
                var contentTemp = await httpResponse.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GenericApiResponseModel>(contentTemp);

                return result;
            }
        }
    }
}
