using BusinessObject.DTO;
using BusinessObject.Model;
using DataAccess.DAO;
using DataAccess.Repository.Interface;

namespace DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDAO _dao = UserDAO.Instance;
        public Task ChangePasswordAsync(string userName, UserChangePasswordRequest model)
            => _dao.ChangePasswordAsync(userName, model);

        public Task CheckPasswordAsync(User user, string password)
            => _dao.CheckPasswordAsync(user, password);

        public Task<UserDTO> GetUserByUserNameAsync(string userName)
            => _dao.GetUserByUserNameAsync(userName);

        public Task<List<UserDTO>> GetUsersAsync()
            => _dao.GetUsersAsync();

        public Task SetActiveUserAsync(string userName, bool isActive)
            => _dao.SetActiveUserAsync(userName, isActive);

        public Task<UserUpdateInfomationRequest> UpdateUserAsync(string username, UserUpdateInfomationRequest model)
            => _dao.UpdateUserAsync(username, model);
    }
}
