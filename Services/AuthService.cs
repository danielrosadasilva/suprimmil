using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using suprimmil.Dto.Access;
using suprimmil.Models;

namespace suprimmil.Services;

public class AuthService(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AuthService> logger) : IAuthService
{
    private readonly SignInManager<User> _signInManager = signInManager;

    private readonly UserManager<User> _userManager = userManager;

    private readonly ILogger<AuthService> _logger = logger;

    public async Task<bool> LoginAsync(LoginDto request)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return false;
            }


            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                request.Password,
                lockoutOnFailure: true
            );

            if (!result.Succeeded)
            {
                return false;
            }

            await _signInManager.SignInAsync(user, isPersistent: true);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro no login para o email {Email}", request.Email);
            return false;
        }
    }
}