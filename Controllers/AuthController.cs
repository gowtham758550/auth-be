using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using auth.Models;
using auth.Data;
using auth.Dtos;
using auth.Services;
using auth.Repositories;

namespace auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        // private static List<User> userList = new List<User>();
        // private readonly IMailService _mailService;
        // private readonly IConfiguration? _configuration;
        // private AuthDbContext _context;
        // public AuthController(IConfiguration configuration, AuthDbContext context, IMailService MailService,)
        // {
        //     _configuration = configuration;
        //     _context = context;
        //     _mailService = MailService;
        // }
        public AuthController(IAuthRepository authRepository)
        {
            if (authRepository != null) _authRepository = authRepository;
            else throw new Exception("Authrepository cannot be null");
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto userReq)
        {
            try{
                await _authRepository.Register(userReq);
                return Ok("User Created");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // [HttpPut("verify-user")]
        // [Authorize]
        // public async Task<ActionResult<string>> VerifyUser()
        // {
        //     var userId = Request.Cookies["userId"];
        //     Console.WriteLine(userId);
        //     User currentUser = GetUser(userId);
        //     if (currentUser.IsVerified)
        //         return Ok("Verified already");
        //     return BadRequest("Not verified");
        //     // return Ok("User verified");
        // }

        // [HttpPost("login")]
        // public async Task<ActionResult<string>> Login(LoginDto user)
        // {
        //     if (!IsUser(user.Username))
        //     {
        //         return BadRequest("User not found");
        //     }
        //     // var targetUser = userList.FirstOrDefault(user => user.Username == user.Username);
        //     var targetUser = GetUser(user.Username);
        //     if (!VerifyPasswordHash(user.Password, targetUser.PasswordSalt, targetUser.PasswordHash))
        //     {
        //         return BadRequest("Wrong password");
        //     }
        //     var refreshToken = GetRefreshToken();
        //     SetRefreshToken(targetUser, refreshToken);
        //     // return Ok("You are right");
        //     // return Ok(new { token = CreateToken(targetUser), refreshToken = refreshToken });
        //     return Ok(new { token = CreateToken(targetUser) });
        // }

        // // [Authorize]
        // [HttpPut("refresh-token")]
        // public async Task<ActionResult<string>> _RefreshToken()
        // {
        //     // var targetUser = 
        //     var refreshToken = Request.Cookies["nekot"];
        //     if (refreshToken == null)
        //     {
        //         return BadRequest("Refresh token not found");
        //     }
        //     // else if ()
        //     return Ok("Token refreshed");
        // }

        // [Authorize]
        // [HttpPut("change-password")]
        // public async Task<ActionResult<string>> ChangePassword(ChangePasswordDto requestBody)
        // {
        //     if (!IsUser(requestBody.UserName))
        //     {
        //         return NotFound("User not exist to change password");
        //     }
        //     var targetUser = GetUser(requestBody.UserName);
        //     if (!VerifyPasswordHash(requestBody.OldPassword, targetUser.PasswordSalt, targetUser.PasswordHash))
        //     {
        //         return BadRequest("Incorrect password");
        //     }
        //     CreatePasswordHash(requestBody.NewPassword, out byte[] newPasswordHash, out byte[] newPasswordSalt);
        //     targetUser.PasswordHash = newPasswordHash;
        //     targetUser.PasswordSalt = newPasswordSalt;
        //     await _context.SaveChangesAsync();
        //     _mailService.Send("methran2003@gmail.com", "Important", "Your password changed successfully");
        //     return Ok("Password changed successfully");
            
        // }


        // [Authorize(Roles = "Admin")]
        // [HttpGet("get")]
        // public async Task<ActionResult<int>> GetAllUser()
        // {
        //     return Ok(_context.User.ToList());
        // }

        // [Authorize(Roles = "Admin")]
        // [HttpDelete("delete")]
        // public async Task<ActionResult<string>> DeleteUserByName(DeleteUserDto requestBody)
        // {
        //     var targetUser = _context.User.Where(user => user.Username == requestBody.UserName).SingleOrDefault();
        //     if (targetUser == null)
        //     {
        //         return BadRequest("User not found");
        //     }
        //     _context.User.Remove(targetUser);
        //     _context.SaveChanges();
        //     return Ok("User deleted");
        // }

        // private RefreshToken GetRefreshToken()
        // {
        //     var refreshToken = new RefreshToken
        //     {
        //         Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
        //         Created = DateTime.Now,
        //         Expires = DateTime.Now.AddDays(7)
        //     };
        //     return refreshToken;
        // }

        // private async Task SetRefreshToken(User targetUser, RefreshToken refreshToken)
        // {
        //     var cookieOptions = new CookieOptions
        //     {
        //         HttpOnly = true,
        //         Expires = refreshToken.Expires
        //     };
        //     Response.Cookies.Append("nekot", refreshToken.Token, cookieOptions);
        //     targetUser.RefreshToken = refreshToken.Token;
        //     targetUser.TokenCreated = refreshToken.Created;
        //     targetUser.TokenExpires = refreshToken.Expires;
        //     await _context.SaveChangesAsync();
        // }

        // private bool IsUser(string userName)
        // {
        //     return _context.User.Any(user => user.Username == userName);
        // }

        // private User GetUser(string userName)
        // {
        //     return _context.User.FirstOrDefault(user => user.Username == userName);
        // }

        // private User GetUser(Guid userId)
        // {
        //     return _context.User.FirstOrDefault(user => user.Id == userId);
        // }

        // private string CreateToken(User user)
        // {
        //     // Console.WriteLine(user.Role.ToString());
        //     var claims = new List<Claim>
        //     {
        //         // new Claim(ClaimTypes.Name, user.Username),
        //         new Claim("Id", user.Id.ToString()),
        //         new Claim(ClaimTypes.Role, user.Role.ToString())
        //     };
        //     Console.Write(claims);
        //     var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
        //     var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        //     var token = new JwtSecurityToken(
        //         claims: claims,
        //         expires: DateTime.Now.AddHours(8),
        //         signingCredentials: creds
        //     );
        //     var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        //     return jwt;
        // }

        // private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        // {
        //     using (var hmac = new HMACSHA512())
        //     {
        //         passwordSalt = hmac.Key;
        //         passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //     }
        // }

        // private bool VerifyPasswordHash(string password, byte[] passwordSalt, byte[] passwordHash)
        // {
        //     using (var hmac = new HMACSHA512(passwordSalt))
        //     {
        //         var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //         return computedHash.SequenceEqual(passwordHash);
        //     }
        // }

    }
}