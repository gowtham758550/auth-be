using auth.Dtos;
using auth.Data;

namespace auth.Repositories;

public interface IAuthRepository
{
    public Task Register(RegisterDto userReq);
    public void Login(LoginDto userReq);
    public void GetRefreshToken();

}