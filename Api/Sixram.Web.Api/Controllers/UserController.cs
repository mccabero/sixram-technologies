using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sixram.Common.Helpers;
using Sixram.Contracts.Services;
using Sixram.DTO;
using Sixram.Models.Request;
using Sixram.Models.Response;

namespace Sixram.Web.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;

        private readonly IUserService _userService;

        public UserController(
            IUserService userService,
            IMapper mapper,
            ILogger<UserController> logger)
        {
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet, Route("list")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _userService.GetAllUsersAsync();
                var data = _mapper.Map<IEnumerable<UserResponseModel>>(response);

                return data == null
                    ? NotFound(new GenericApiResponseModel(404, "No user roles found."))
                    : Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new GenericApiResponseModel(400, ex.Message));
            }
        }

        [HttpPost, Route("user-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var response = await _userService.GetUserByIdAsync(id);
                var data = _mapper.Map<UserResponseModel>(response);

                return data == null
                    ? NotFound(new GenericApiResponseModel(404, $"User with id {id} does not exist."))
                    : Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new GenericApiResponseModel(400, ex.Message));
            }
        }

        [HttpPost, Route("create")]
        public async Task<IActionResult> Create([FromBody] UserRequestModel model)
        {
            try
            {
                var currentUserId = TokenHelper.GetCurrentUserId(this.HttpContext);

                var dto = _mapper.Map<UsersDto>(model);

                dto.CreatedById = currentUserId;
                dto.CreatedDateTime = DateTime.UtcNow;
                dto.UpdatedById = currentUserId;
                dto.UpdatedDateTime = DateTime.UtcNow;

                var response = await _userService.CreateAsync(dto);
                var mapResponse = _mapper.Map<UserResponseModel>(response);

                return mapResponse == null
                    ? NotFound(mapResponse) // TODO: Need to check what should be the correct response if error
                    : Ok(mapResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(new GenericApiResponseModel(400, ex.Message));
            }
        }

        [HttpPut, Route("update")]
        public async Task<IActionResult> Update(int id, [FromBody] UserRequestModel model)
        {
            try
            {
                var currentUserId = TokenHelper.GetCurrentUserId(this.HttpContext);

                // Original data
                var dto = await _userService.GetByIdAsync(id);

                if (dto == null)
                    return NotFound(new GenericApiResponseModel(404, $"User with id {id} does not exist."));

                #region Specific Data to Update
                dto.RoleId = model.RoleId;
                dto.Email = model.Email;
                dto.RoleId = model.RoleId;
                dto.Salt = model.Salt;
                dto.Gender = model.Gender;
                dto.PasswordHash = model.PasswordHash;
                dto.FirstName = model.FirstName;
                dto.MiddleName = model.MiddleName;
                dto.LastName = model.LastName;
                dto.MobileNumber = model.MobileNumber;
                dto.Birthday = model.Birthday;
                dto.IsActive = model.IsActive;
                #endregion

                dto.UpdatedById = currentUserId;
                dto.UpdatedDateTime = DateTime.UtcNow;

                var response = await _userService.UpdateAsync(dto);
                var mapResponse = _mapper.Map<UserResponseModel>(response);

                return mapResponse == null
                    ? NotFound(mapResponse) // TODO: Need to check what should be the correct response if error
                    : Ok(mapResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(new GenericApiResponseModel(400, ex.Message));
            }

        }

        [HttpDelete, Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Original data
                var dto = await _userService.GetByIdAsync(id);

                if (dto == null)
                    return NotFound(new GenericApiResponseModel(404, $"User with id {id} does not exist."));

                await _userService.DeleteAsync(id);

                return Ok(new GenericApiResponseModel(200, "User successfully deleted."));
            }
            catch (Exception ex)
            {
                return BadRequest(new GenericApiResponseModel(400, ex.Message));
            }
        }
    }
}
