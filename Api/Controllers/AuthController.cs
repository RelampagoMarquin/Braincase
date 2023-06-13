using Api.Dto.User;
using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(
            IConfiguration configuration,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;

        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserCreateDTO model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);

            if (userExists is not null)
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new Response { Success = false, Message = "Usuário já existe!" }
                );

                // Remova espaços em branco do início e do final do nome
                var name = model.Name.Trim();

                // Remova caracteres especiais do nome
                var validCharacters = new HashSet<char>("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789.-_+@");
                var sanitizedName = new string(name.Where(c => validCharacters.Contains(c)).ToArray());

            User user = new()
            {
                UserName = sanitizedName,
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = model.Email,
                Name = model.Name,
                Registration = model.Registration,
                
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = "Erro ao criar Usuario" });
            }
                

            return Ok(new Response { Message = "Usuário criado com sucesso!" });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] Login model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is not null && await _userManager.CheckPasswordAsync(user, model.Password))
            {

                var authClaims = new List<Claim>
            {
                new (ClaimTypes.Name, user.UserName),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                var userRoles = await _userManager.GetRolesAsync(user);

                foreach (var userRole in userRoles)
                    authClaims.Add(new(ClaimTypes.Role, userRole));

                return Ok(new Response { Data = GetToken(authClaims) });
            }

            return Unauthorized();
        }

        private TokenModel GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo
            };

        }

        private async Task AddToRoleAsync(User user, string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new(role));

            await _userManager.AddToRoleAsync(user, role);
        }


    }
}
