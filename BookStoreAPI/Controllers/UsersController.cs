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
                    return NotFound(new ResponseDTO<Object>()
                    {
                        Success = false,
                        Payload = null,
                        Error = new ErrorDetails() { Code = 404, Message = "No user found." }
                    });
                }
                return Ok(new ResponseDTO<UserDTO[]>() { Payload = users.ToArray() });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDTO<Object>()
                {
                    Success = false,
                    Payload = null,
                    Error = new ErrorDetails() { Code = 500, Message = "Internal server error. Please try again later." }
                });
            }
        }

        [Authorize]
        [HttpGet("user-profile")]
        public async Task<IActionResult> GetLoggedInUserProfile()
        {
            string userName = User.FindFirst("username")?.Value;
            try
            {
                var user = await _repository.GetUserByUserNameAsync(userName);
                return Ok(new ResponseDTO<UserDTO>() { Payload = user });
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new ResponseDTO<Object>()
                {
                    Success = false,
                    Payload = null,
                    Error = new ErrorDetails() { Code = 404, Message = $"User {userName} not found." }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDTO<Object>()
                {
                    Success = false,
                    Payload = null,
                    Error = new ErrorDetails() { Code = 500, Message = "Internal server error. Please try again later." }
                });
            }
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> Get(string userName)
        {
            try
            {
                var user = await _repository.GetUserByUserNameAsync(userName);
                return Ok(new ResponseDTO<UserDTO>() { Payload = user });
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new ResponseDTO<Object>()
                {
                    Success = false,
                    Payload = null,
                    Error = new ErrorDetails() { Code = 404, Message = $"User {userName} not found." }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDTO<Object>()
                {
                    Success = false,
                    Payload = null,
                    Error = new ErrorDetails() { Code = 500, Message = "Internal server error. Please try again later." }
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserUpdateInfomationRequest model)
        {
            string userName = User.FindFirst("username")?.Value;
            try
            {
                await _repository.UpdateUserAsync(userName, model);
                return Ok(new ResponseDTO<string>() { Payload = $"Update User {model.UserName} successfully" });
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new ResponseDTO<Object>()
                {
                    Success = false,
                    Payload = null,
                    Error = new ErrorDetails() { Code = 404, Message = $"User {userName} not found." }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDTO<Object>()
                {
                    Success = false,
                    Payload = null,
                    Error = new ErrorDetails() { Code = 500, Message = "Internal server error. Please try again later." }
                });
            }
        }

        [HttpPatch("set-active")]
        public async Task<IActionResult> PatchSetActive([FromBody] UserSetIsActiveRequest model)
        {
            try
            {
                await _repository.SetActiveUserAsync(model.UserName, model.IsActive);
                return Ok(new ResponseDTO<string>() { Payload = $"Update User {model.UserName}'s active to {model.IsActive} successfully" });
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new ResponseDTO<Object>()
                {
                    Success = false,
                    Payload = null,
                    Error = new ErrorDetails() { Code = 404, Message = $"User {model.UserName} not found." }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDTO<Object>()
                {
                    Success = false,
                    Payload = null,
                    Error = new ErrorDetails() { Code = 500, Message = "Internal server error. Please try again later." }
                });
            }
        }

        [HttpPatch("change-password")]
        public async Task<IActionResult> PatchChangePassword([FromBody] UserChangePasswordRequest model)
        {
            string userName = User.FindFirst("username")?.Value;
            try
            {
                var userDTO = await _repository.GetUserByUserNameAsync(userName);
                var mapper = MapperConfig.Init();
                var user = mapper.Map<User>(userDTO);
                var passwordValidators = new PasswordValidator<User>();
                var result = await passwordValidators.ValidateAsync(_userManager, null, model.NewPassword);
                if (!result.Succeeded)
                    return StatusCode(400, new ResponseDTO<Object>()
                    {
                        Success = false,
                        Payload = null,
                        Error = new ErrorDetails() { Code = 400, Message = result.Errors.First().Description }
                    });
                await _repository.ChangePasswordAsync(userName, model);
                return Ok(new ResponseDTO<string>() { Payload = $"Change password successfully!" });
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new ResponseDTO<Object>()
                {
                    Success = false,
                    Payload = null,
                    Error = new ErrorDetails() { Code = 404, Message = $"User {userName} not found." }
                });
            }
            catch (IncorrectPasswordException ex)
            {
                return StatusCode(403, new ResponseDTO<Object>()
                {
                    Success = false,
                    Payload = null,
                    Error = new ErrorDetails() { Code = 403, Message = "Password is incorrect!" }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDTO<Object>()
                {
                    Success = false,
                    Payload = null,
                    Error = new ErrorDetails() { Code = 500, Message = "Internal server error. Please try again later." }
                });
            }
        }


    }
}
