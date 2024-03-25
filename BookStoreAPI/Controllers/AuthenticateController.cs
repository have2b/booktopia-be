using BusinessObject.DTO;
using BusinessObject.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticateController(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                if (!user.IsActive)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, new { Message = "Account is inactive!!!" });
                }
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim("username", user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim("role", userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new ResponseDTO<string>()
                {
                    Payload = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }

            return Unauthorized(new ResponseDTO<Object>()
            {
                Success = false,
                Payload = null,
                Error = new ErrorDetails() { Code = 401, Message = "Unauthorized" }
            });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterRequest model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return Conflict(new ResponseDTO<Object>()
                {
                    Success = false,
                    Payload = null,
                    Error = new ErrorDetails() { Code = 409, Message = "User already exists!" }
                });

            User user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                Name = model.Name
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(400, new ResponseDTO<Object>()
                {
                    Success = false,
                    Payload = null,
                    Error = new ErrorDetails()
                    { Code = 400, Message = result.Errors.First().Description }
                });

            await _userManager.AddToRoleAsync(user, UserRole.User);

            return Ok(new ResponseDTO<User>() { Payload = user });
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterRequest model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return Conflict(new ResponseDTO<Object>()
                {
                    Success = false,
                    Payload = null,
                    Error = new ErrorDetails() { Code = 409, Message = "User already exists!" }
                });

            User user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                Name = model.Name
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(400, new ResponseDTO<Object>()
                {
                    Success = false,
                    Payload = null,
                    Error = new ErrorDetails()
                    { Code = 400, Message = result.Errors.First().Description }
                });


            //Admin has full authority of all role  
            await _userManager.AddToRoleAsync(user, UserRole.Admin);
            await _userManager.AddToRoleAsync(user, UserRole.User);

            return Ok(new ResponseDTO<User>() { Payload = user });
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddDays(30),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }
    }
}