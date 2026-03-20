using suprimmil.Dto.Access;

namespace suprimmil.Services;
public interface IAuthService
{
    Task<bool> LoginAsync(LoginDto request);
}