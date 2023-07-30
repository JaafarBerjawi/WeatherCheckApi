using Data.EFCore;
using Security.Entities;

namespace Security.Data.EFCore
{
    //Data Access using EF Core => for application
    public class AuthenticationDataManager : IAuthenticationDataManager
    {
        private readonly ApplicationDBContext _applicationDBContext;
        public AuthenticationDataManager(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }
        public void RegisterUser(User user)
        {
            _applicationDBContext.Users.Add(user);
            _applicationDBContext.SaveChanges();
        }
        public User GetUserByUsername(string username)
        {
            return _applicationDBContext.Users.Where(user => user.Username == username)?.FirstOrDefault();
        }
        public UserToken GetUserTokenByUserId(int userId)
        {
            return _applicationDBContext.UserTokens.Where(userToken => userToken.UserId == userId)?.FirstOrDefault();
        }
        public void AddUserToker(UserToken userToken)
        {
            _applicationDBContext.UserTokens.Add(userToken);
            _applicationDBContext.SaveChanges();
        }
        public void UpdateUserToken(UserToken userToken)
        {
            _applicationDBContext.SaveChanges();
        }
        public UserToken GetActiveUserToken(string token)
        {
            return _applicationDBContext.UserTokens.FirstOrDefault(userToken => userToken.Token == token && userToken.ExpirationDate > DateTime.Now);
        }
    }
}