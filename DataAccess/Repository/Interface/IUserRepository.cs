using BusinessObject.DTO;
using BusinessObject.Model;

namespace DataAccess.Repository.Interface
{
    public interface IUserRepository
    {
        Task<List<UserDTO>> GetUsersAsync();
        Task<UserDTO> GetUserByUserNameAsync(string userName);
        Task<UserUpdateInfomationRequest> UpdateUserAsync(string username, UserUpdateInfomationRequest model);
        Task SetActiveUserAsync(string userName, bool isActive);
        Task ChangePasswordAsync(string userName, UserChangePasswordRequest model);
        Task CheckPasswordAsync(User user, string password);
    }
}
