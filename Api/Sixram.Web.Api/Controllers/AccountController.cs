using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sixram.Common.Helpers;
using Sixram.Contracts.Services;
using Sixram.Models;
using Sixram.Models.Request;
using Sixram.Models.Response;

namespace Sixram.Web.Api.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IOptions<AppSettings> _appSettings;
        private readonly ILogger<AccountController> _logger;

        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;

        public AccountController(
            IOptions<AppSettings> appSettings,
            ILogger<AccountController> logger,
            IUserService userService,
            IUserRoleService userRoleService)
        {
            _logger = logger;
            _appSettings = appSettings;
            _userService = userService;
            _userRoleService = userRoleService;
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
            try
            {
                _logger.LogInformation($"Attempting to Login [{JsonConvert.SerializeObject(model)}]");

                if (!model.Email.IsValidEmail())
                {
                    return BadRequest(new LoginResponseModel(false, string.Empty, "Invalid email address format."));
                }

                if (string.IsNullOrEmpty(model.Email) && string.IsNullOrEmpty(model.Password))
                {
                    return BadRequest(new LoginResponseModel(false, string.Empty, "Email or Password is empty."));
                }

                // Login flow here...
                var userByEmail = await _userService.GetUserByEmailAsync(model.Email);
                if (userByEmail == null)
                {
                    return BadRequest(new LoginResponseModel(false, string.Empty, "Email address not found."));
                }

                // Check if password is match
                var encryptedPassword = CryptographyHelper.Encrypt(model.Password, CryptographyHelper.GetEncryptionKey());
                if (userByEmail.PasswordHash != encryptedPassword)
                {
                    return BadRequest(new LoginResponseModel(false, string.Empty, "Invalid password."));
                }

                // Include roles from the user reponse.
                var userRolesByUser = await _userRoleService.GetAllUserRolesByUserIdAsync(userByEmail.Id);
                userByEmail.UserRoles = userRolesByUser == null
                    ? null
                    : userRolesByUser.ToList();

                // Generate claims once all validation passed
                var claims = await TokenHelper.GenerateClaimsResponse(userByEmail, _appSettings);

                return claims == null
                    ? NotFound(new GenericApiResponseModel(404, "Login failed."))
                    : Ok(claims);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
