using Microsoft.AspNetCore.Identity;
using suprimmil.Dto.Access;
using suprimmil.Models;

namespace suprimmil.Services;

public class AuthService(UserManager<User> userManager, SignInManager<User> signInManager) : IAuthService
{
    private readonly SignInManager<User> _signInManager = signInManager;

    private readonly UserManager<User> _userManager = userManager;

    public async Task<bool> LoginAsync(LoginDto request)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return false;
            }


            // Verifica senha usando o SignInManager (que vai usar seu CustomPasswordHasher)
            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                request.Password,
                lockoutOnFailure: true
            );

            if (!result.Succeeded)
            {
                return false;
            }

            await _userManager.UpdateAsync(user);

            await _signInManager.SignInAsync(user, isPersistent: true);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro no login: {ex.Message}");
            return false;
        }
    }
}