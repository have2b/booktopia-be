using BusinessObject;
using BusinessObject.DTO;
using BusinessObject.Model;
using DataAccess.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class UserDAO
    {
        private static readonly Lazy<UserDAO> _instance =
        new Lazy<UserDAO>(() => new UserDAO(new AppDbContext()));

        private readonly AppDbContext _context;

        private UserDAO(AppDbContext context)
        {
            _context = context;
        }

        public static UserDAO Instance => _instance.Value;
        public async Task ChangePasswordAsync(string userName, UserChangePasswordRequest model)
        {
            var hasher = new PasswordHasher<IdentityUser>();
            var user = await GetFullfilUserByUserNameAsync(userName);
            await CheckPasswordAsync(user, model.CurrentPassword);
            user.PasswordHash = hasher.HashPassword(user, model.NewPassword);
            await _context.SaveChangesAsync();
        }

        public async Task<UserDTO> GetUserByIdAsync(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            var mapper = MapperConfig.Init();
            var userDTO = mapper.Map<User, UserDTO>(user);
            var listRole = await GetRolesAsync(userDTO.UserName);
            userDTO.Roles.AddRange(listRole);
            return userDTO;
        }

        public async Task<UserDTO> GetUserByUserNameAsync(string userName)
        {
            var user = await GetFullfilUserByUserNameAsync(userName);
            var mapper = MapperConfig.Init();
            var userDTO = mapper.Map<User, UserDTO>(user);
            var listRole = await GetRolesAsync(userDTO.UserName);
            userDTO.Roles.AddRange(listRole);
            return userDTO;
        }

        public async Task<List<UserDTO>> GetUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            var mapper = MapperConfig.Init();
            var userDTOs = users.Select(user => mapper.Map<User, UserDTO>(user)).ToList();
            foreach (var userDTO in userDTOs)
            {
                var listRole = await GetRolesAsync(userDTO.UserName);
                userDTO.Roles.AddRange(listRole);
            }
            return userDTOs.ToList();
        }

        public async Task SetActiveUserAsync(string userName, bool isActive)
        {
            var user = await GetFullfilUserByUserNameAsync(userName);
            user.IsActive = isActive;
            await _context.SaveChangesAsync();
        }

        public async Task<UserUpdateInfomationRequest> UpdateUserAsync(string username, UserUpdateInfomationRequest model)
        {
            if (username != model.UserName)
            {
                throw new UserNotFoundException(username);
            }
            var user = await GetFullfilUserByUserNameAsync(username);
            var mapper = MapperConfig.Init();
            user = mapper.Map(model, user);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task CheckPasswordAsync(User user, string password)
        {
            var hasher = new PasswordHasher<IdentityUser>();
            if (hasher.VerifyHashedPassword(user, user.PasswordHash, password) != PasswordVerificationResult.Success)
            {
                throw new IncorrectPasswordException();
            }
        }

        public async Task<User> GetFullfilUserByUserNameAsync(string userName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            if (user == null)
            {
                throw new UserNotFoundException(userName);
            }
            return user;
        }

        public async Task<List<string>> GetRolesAsync(string username)
        {
            List<string> roleList = new List<string>();
            var user = await GetFullfilUserByUserNameAsync(username);
            var rolesOfUser = await _context.UserRoles.Where(u => u.UserId == user.Id).ToListAsync();
            foreach (var role in rolesOfUser)
            {
                var roleName = await _context.Roles.Where(r => r.Id == role.RoleId).Select(r => r.Name).FirstOrDefaultAsync();
                roleList.Add(roleName);
            }
            return roleList;
        }
    }
}
