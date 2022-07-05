using auth.Dtos;
using auth.Data;
using auth.Models;
using auth.Utilities;

namespace auth.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        public readonly AuthDbContext _context;
        public readonly IUtility _utility;

        public AuthRepository(AuthDbContext context, IUtility utility)
        {
            _context = context;
            _utility = utility;
        }

        public async Task Register(RegisterDto userReq)
        {
            if (_utility.IsUser(userReq.Username ?? throw new Exception("Invalid user identity")))
            {
                throw new Exception("User already exist");
            }
            var newUser = new User()
            {
                Username = userReq.Username,
                EmailId = userReq.EmailId,
                Role = userReq.Role
            };
            if (_context.User != null) await _context.User.AddAsync(newUser);
            else throw new Exception("User table cannot be null in context");
            await _context.SaveChangesAsync();
        }

        public void GetRefreshToken()
        {
            throw new NotImplementedException();
        }

        public void Login(LoginDto userReq)
        {
            throw new NotImplementedException();
        }
    }
}