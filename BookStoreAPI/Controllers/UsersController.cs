using BusinessObject;
using BusinessObject.DTO;
using BusinessObject.Model;
using DataAccess.Exceptions;
using DataAccess.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly UserManager<User> _userManager;
        public UsersController(IUserRepository repository, UserManager<User> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var users = await _repository.GetUsersAsync();
                if (!users.Any())
                {
                    return NotFound("No user found.");
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [Authorize]
        [HttpGet("user-profile")]
        public async Task<IActionResult> GetCurrentUserProfile()
        {
            string userName = User.FindFirst("username")?.Value;
            try
            {
                var user = await _repository.GetUserByUserNameAsync(userName);
                return Ok(user);
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound($"User {userName} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> Get(string userName)
        {
            try
            {
                var user = await _repository.GetUserByUserNameAsync(userName);
                return Ok(user);
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound($"User {userName} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Post([FromBody] UserUpdateInfomationRequest model)
        {
            string userName = User.FindFirst("username")?.Value;
            try
            {
                await _repository.UpdateUserAsync(userName, model);
                return Ok($"Update User {model.UserName} successfully");
            }
            catch (UserNotFoundException ex)
            {
                return NotFound($"User {model.UserName} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpPatch("set-active")]
        public async Task<IActionResult> PatchSetActive([FromBody] UserSetIsActiveRequest model)
        {
            try
            {
                await _repository.SetActiveUserAsync(model.UserName, model.IsActive);
                return Ok($"Update User {model.UserName}'s active to {model.IsActive} successfully");
            }
            catch (UserNotFoundException ex)
            {
                return NotFound($"User {model.UserName} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpPatch("change-password")]
        public async Task<IActionResult> PostSetActive([FromBody] UserChangePasswordRequest model)
        {
            string userName = User.FindFirst("username")?.Value;
            try
            {
                var userDTO = _repository.GetUserByUserNameAsync(userName);
                var mapper = MapperConfig.Init();
                var user = mapper.Map<User>(userDTO.Result);
                var passwordValidators = new PasswordValidator<User>();
                var result = await passwordValidators.ValidateAsync(_userManager, null, model.NewPassword);
                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = result.Errors.ToList() });
                await _repository.ChangePasswordAsync(userName, model);
                return Ok($"Change password successfully!");
            }
            catch (UserNotFoundException ex)
            {
                return NotFound($"User {userName} not found.");
            }
            catch (IncorrectPasswordException ex)
            {
                return BadRequest("Password is incorrect!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }


    }
}
