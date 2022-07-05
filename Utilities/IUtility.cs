using auth.Models;

namespace auth.Utilities;

public interface IUtility
{
    // public bool IsUser(string userName);
    public bool IsUser(string userId);
    public User GetUser(string userId);
    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    public string CreateAccessToken(User user);
    public RefreshToken GetRefreshToken();
    public Task SetRefreshToken(User targetUser, RefreshToken refreshToken);

    
}