using auth.Models;
using auth.Data;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;

namespace auth.Utilities
{
    public class Utility : IUtility
    {
        private readonly AuthDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Utility(AuthDbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            if (context != null) _context = context;
            else throw new Exception("Dbcontext cannot be null");
            if (configuration != null) _configuration = configuration;
            else throw new Exception("Configuration cannot be null");
            if (httpContextAccessor != null) _httpContextAccessor = httpContextAccessor;
            else throw new Exception("HttpContextAccessor cannot be null");
            
        }
        
        public bool IsUser(string userId)
        {
            if (_context.User != null) return _context.User.Any(user => user.Id.ToString() == userId);
            throw new Exception("User not found");
        }

        public User GetUser(string userId)
        {
            if (_context.User != null) return _context.User.First(user => user.Id.ToString() == userId);
            throw new Exception("User not found");

        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public string CreateAccessToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: creds
            );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public RefreshToken GetRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Created = DateTime.Now,
                Expires = DateTime.Now.AddDays(7)
            };
            return refreshToken;
        }

        public async Task SetRefreshToken(User targetUser, RefreshToken refreshToken)
        {
            // var cookieOptions = new CookieOptions
            // {
            //     HttpOnly = true,
            //     Expires = refreshToken.Expires
            // };
            // ControllerBase.Response.Cookies.Append("nekot", refreshToken.Token, cookieOptions);
            // targetUser.RefreshToken = refreshToken.Token;
            // targetUser.TokenCreated = refreshToken.Created;
            // targetUser.TokenExpires = refreshToken.Expires;
            await _context.SaveChangesAsync();
        }

        public string GetUserId()
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                var userId = _httpContextAccessor.HttpContext.Request.Cookies["userId"];
                if (userId != null) return userId;
                else throw new Exception("UserId not found");
            }
            throw new Exception("HttpContext cannot be null");
        }

    }
}