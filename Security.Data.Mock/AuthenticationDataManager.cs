using Security.Entities;

namespace Security.Data.Mock
{
    //Data Access using mock data => for tests
    public class AuthenticationDataManager : IAuthenticationDataManager
    {
        private static readonly List<UserToken> userTokens = new List<UserToken>()
        {
            new UserToken{ Id = 1, UserId = 1, Token="TestToken1", ExpirationDate=DateTime.MinValue },
            new UserToken{ Id = 2, UserId = 2, Token="TestToken2", ExpirationDate = DateTime.Now.AddHours(1) },
        };
        public static readonly List<User> users = new List<User>
        {
            new User { Id = 1, Username = "TestUser1", Password = "c669ebcc10ad2fdc08b5f688d0638f07905040d49e3fbfdc498775b3a2ea67f4" }, // UnencryptedPassword : TestPassword1
            new User { Id = 2, Username = "TestUser2", Password = "3fed51cad87a3ba97c291d785c8a215b52c2715c67baaa5b25d9d1c0891a07d0"}   // UnencryptedPassword : TestPassword2
        };

        public void AddUserToker(UserToken userToken)
        {
            userToken.Id = userTokens.Max(x => x.Id) + 1;
            userTokens.Add(userToken);
        }

        public UserToken GetActiveUserToken(string token)
        {
            return userTokens.FirstOrDefault(x => x.Token == token && x.ExpirationDate > DateTime.Now);
        }

        public User GetUserByUsername(string username)
        {
            return users.FirstOrDefault(x => x.Username == username);
        }

        public UserToken GetUserTokenByUserId(int userId)
        {
            return userTokens.FirstOrDefault(x => x.UserId == userId);
        }

        public void RegisterUser(User user)
        {
            user.Id = users.Max(x => x.Id) + 1;
            users.Add(user);
        }

        public void UpdateUserToken(UserToken userToken)
        {
            var token = userTokens.FirstOrDefault(x => x.Id == userToken.Id);
            token.Token = userToken.Token;
            token.ExpirationDate = userToken.ExpirationDate;
        }
    }
}