using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ProjetoFIAP.Api.Application.DTOs;
using ProjetoFIAP.Api.Infra.Repository.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ProjetoFIAP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [SwaggerTag("Autenticação e gerenciamento de tokens")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            IConfiguration configuration, 
            IUserRepository userRepository,
            ILogger<AuthController> logger)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpPost("login")]
        [SwaggerOperation( Summary = "Autenticar usuário",Description = "Realiza autenticação e retorna token JWT")]
        [SwaggerResponse(200, "Login realizado com sucesso", typeof(LoginResponseDTO))]
        [SwaggerResponse(400, "Dados inválidos")]
        [SwaggerResponse(401, "Credenciais inválidas")]
        public async Task<ActionResult<LoginResponseDTO>> Login(
            [FromBody, SwaggerRequestBody(Required = true)] LoginDTO login)
        {
            try
            {
                if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
                    return BadRequest("Email e senha são obrigatórios");

                var user = await _userRepository.GetUserByEmailAndPasswordAsync(login.Email, login.Password);
                
                if (user == null)
                {
                    _logger.LogWarning("Tentativa de login mal sucedida para o email: {Email}", login.Email);
                    return Unauthorized("Email ou senha inválidos");
                }

                var token = GenerateToken(user);
                
                _logger.LogInformation("Login bem sucedido para o usuário: {UserId}", user.Id);

                return Ok(new LoginResponseDTO 
                { 
                    Token = token,
                    User = user
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar login");
                return StatusCode(500, "Erro interno ao processar a requisição");
            }
        }

        private string GenerateToken(UserResponseDTO user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userId", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? 
                    throw new InvalidOperationException("JWT Key não configurada")));
                    
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8), // Token válido por 8 horas
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
