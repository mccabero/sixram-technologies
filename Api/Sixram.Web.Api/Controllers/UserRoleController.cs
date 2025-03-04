using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sixram.Contracts.Services;
using Sixram.DTO;
using Sixram.Models.Request;
using Sixram.Models.Response;

namespace Sixram.Web.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user-role")]
    public class UserRoleController : ControllerBase
    {
        private readonly ILogger<UserRoleController> _logger;
        private readonly IMapper _mapper;

        private readonly IUserRoleService _userRoleService;

        public UserRoleController(
            IUserRoleService userRoleService,
            IMapper mapper,
            ILogger<UserRoleController> logger)
        {
            _userRoleService = userRoleService;
            _mapper = mapper;
            _logger = logger;
        }

        //[Authorize]
        [HttpGet, Route("list")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _userRoleService.GetAllUserRolesAsync();
                var data = _mapper.Map<IEnumerable<UserRoleResponseModel>>(response);

                return data == null
                    ? NotFound(new GenericApiResponseModel(404, "No user roles found."))
                    : Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new GenericApiResponseModel(400, ex.Message));
            }
        }

        [HttpPost, Route("role-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var response = await _userRoleService.GetByIdAsync(id);
                var data = _mapper.Map<UserRoleResponseModel>(response);

                return data == null
                    ? NotFound(new GenericApiResponseModel(404, $"User Role with id {id} does not exist."))
                    : Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new GenericApiResponseModel(400, ex.Message));
            }
        }

        [HttpPost, Route("create")]
        public async Task<IActionResult> Create([FromBody] UserRoleRequestModel model)
        {
            try
            {
                var dto = _mapper.Map<UserRolesDto>(model);

                dto.CreatedById = 1; // TODO: Update this to get the current user
                dto.CreatedDateTime = DateTime.UtcNow;
                dto.UpdatedById = 1; // TODO: Update this to get the current user
                dto.UpdatedDateTime = DateTime.UtcNow;

                var response = await _userRoleService.CreateAsync(dto);
                var mapResponse = _mapper.Map<UserRoleResponseModel>(response);

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
        public async Task<IActionResult> Update(int id, [FromBody] UserRoleRequestModel model)
        {
            try
            {
                // Original data
                var dto = await _userRoleService.GetByIdAsync(id);

                if (dto == null)
                    return NotFound(new GenericApiResponseModel(404, $"User Role with id {id} does not exist."));

                #region Specific Data to Update
                dto.UserId = model.UserId;
                dto.RoleId = model.RoleId;
                #endregion

                dto.UpdatedById = 1; // TODO: Update this to get the current user
                dto.UpdatedDateTime = DateTime.UtcNow;

                var response = await _userRoleService.UpdateAsync(dto);
                var mapResponse = _mapper.Map<UserRoleResponseModel>(response);

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
                var dto = await _userRoleService.GetByIdAsync(id);

                if (dto == null)
                    return NotFound(new GenericApiResponseModel(404, $"User Role with id {id} does not exist."));

                await _userRoleService.DeleteAsync(id);

                return Ok(new GenericApiResponseModel(200, "User Role successfully deleted."));
            }
            catch (Exception ex)
            {
                return BadRequest(new GenericApiResponseModel(400, ex.Message));
            }
        }
    }
}