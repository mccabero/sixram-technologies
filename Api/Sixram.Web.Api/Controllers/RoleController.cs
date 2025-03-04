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
    [Route("api/role")]
    public class RoleController : ControllerBase
    {
        private readonly ILogger<RoleController> _logger;
        private readonly IMapper _mapper;

        private readonly IRoleService _roleService;

        public RoleController(
            IRoleService roleService,
            IMapper mapper,
            ILogger<RoleController> logger)
        {
            _roleService = roleService;
            _mapper = mapper;
            _logger = logger;
        }

        //[Authorize]
        [HttpGet, Route("list")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _roleService.GetAllRolesAsync();
                var data = _mapper.Map<IEnumerable<RoleResponseModel>>(response);

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
                var response = await _roleService.GetByIdAsync(id);
                var data = _mapper.Map<RoleResponseModel>(response);

                return data == null
                    ? NotFound(new GenericApiResponseModel(404, $"Role with id {id} does not exist."))
                    : Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new GenericApiResponseModel(400, ex.Message));
            }
        }

        [HttpPost, Route("create")]
        public async Task<IActionResult> Create([FromBody] RoleRequestModel model)
        {
            try
            {
                var currentUserId = TokenHelper.GetCurrentUserId(this.HttpContext);

                var dto = _mapper.Map<RolesDto>(model);

                dto.CreatedById = currentUserId;
                dto.CreatedDateTime = DateTime.UtcNow;
                dto.UpdatedById = currentUserId;
                dto.UpdatedDateTime = DateTime.UtcNow;

                var response = await _roleService.CreateAsync(dto);
                var mapResponse = _mapper.Map<RoleResponseModel>(response);

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
        public async Task<IActionResult> Update(int id, [FromBody] RoleRequestModel model)
        {
            try
            {
                var currentUserId = TokenHelper.GetCurrentUserId(this.HttpContext);

                // Original data
                var dto = await _roleService.GetByIdAsync(id);

                if (dto == null)
                    return NotFound(new GenericApiResponseModel(404, $"Role with id {id} does not exist."));

                #region Specific Data to Update
                dto.Name = model.Name;
                dto.Description = model.Description;
                #endregion

                dto.UpdatedById = currentUserId;
                dto.UpdatedDateTime = DateTime.UtcNow;

                var response = await _roleService.UpdateAsync(dto);
                var mapResponse = _mapper.Map<RoleResponseModel>(response);

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
                var dto = await _roleService.GetByIdAsync(id);

                if (dto == null)
                    return NotFound(new GenericApiResponseModel(404, $"Role with id {id} does not exist."));

                await _roleService.DeleteAsync(id);

                return Ok(new GenericApiResponseModel(200, "Role successfully deleted."));
            }
            catch (Exception ex)
            {
                return BadRequest(new GenericApiResponseModel(400, ex.Message));
            }
        }
    }
}