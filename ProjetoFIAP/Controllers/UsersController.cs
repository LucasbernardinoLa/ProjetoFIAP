using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoFIAP.Api.Application.DTOs;
using ProjetoFIAP.Api.Domain.Exceptions;
using ProjetoFIAP.Api.Infra.Repository.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ProjetoFIAP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [SwaggerTag("Gerenciamento de usu�rios")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userService;

        public UsersController(IUserRepository userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        [SwaggerOperation(Summary = "Registrar novo usu�rio",Description = "Cria um novo usu�rio no sistema")]
        [SwaggerResponse(200, "Usu�rio registrado com sucesso", typeof(UserResponseDTO))]
        [SwaggerResponse(400, "Dados inv�lidos")]
        public async Task<ActionResult<UserResponseDTO>> Register(
            [FromBody, SwaggerRequestBody(Required = true)] UserRegistrationDTO registration)
        {
            try
            {
                var user = await _userService.RegisterUserAsync(registration);
                return Ok(user);
            }
            catch (DomainValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        [SwaggerOperation(Summary = "Listar usu�rios",Description = "Retorna todos os usu�rios cadastrados (requer permiss�o de Admin)")]
        [SwaggerResponse(200, "Lista de usu�rios retornada com sucesso", typeof(IEnumerable<UserResponseDTO>))]
        [SwaggerResponse(401, "N�o autorizado")]
        [SwaggerResponse(403, "Acesso negado - Requer role Admin")]
        public async Task<ActionResult<IEnumerable<UserResponseDTO>>> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
    }
}