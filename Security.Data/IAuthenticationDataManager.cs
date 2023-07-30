using Security.Entities;

namespace Security.Data
{
    public interface IAuthenticationDataManager
    {
        void AddUserToker(UserToken userToken);
        UserToken GetActiveUserToken(string token);
        User GetUserByUsername(string username);
        UserToken GetUserTokenByUserId(int userId);
        void RegisterUser(User user);
        void UpdateUserToken(UserToken userToken);
    }
}