using Microsoft.AspNetCore.Identity;
using suprimmil.Dto.Access;
using suprimmil.Models;
using suprimmil.Repository;

namespace suprimmil.Services;

public class AuthService(UserRepository userRepository, UserManager<User> userManager, SignInManager<User> signInManager) : IAuthService
{
    private readonly UserRepository _userRepository = userRepository;
    private readonly SignInManager<User> _signInManager = signInManager;

    private readonly UserManager<User> _userManager = userManager;

    public async Task<bool> LoginAsync(LoginDto request)
    {
        Console.WriteLine($"=== LOGIN PARA: {request.Email} ===");

        try
        {
            // 1. Busca usuário
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                Console.WriteLine("Usuário não encontrado");
                return false;
            }

            // 2. Verifica senha usando o SignInManager (que vai usar seu CustomPasswordHasher)
            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                request.Password,
                lockoutOnFailure: true
            );

            if (!result.Succeeded)
            {
                Console.WriteLine($"Senha inválida: {result}");

                if (result.IsLockedOut)
                {
                    user.IsLocked = true;
                    await _userManager.UpdateAsync(user);
                }
                else
                {
                    user.LoginAttempts++;
                    await _userManager.UpdateAsync(user);
                }

                return false;
            }

            Console.WriteLine("Senha correta!");

            // 3. Atualiza campos personalizados
            user.LoginAttempts = 0;
            user.IsLocked = false;
            await _userManager.UpdateAsync(user);

            // 4. Faz o SignIn
            await _signInManager.SignInAsync(user, isPersistent: true);
            Console.WriteLine("Login bem sucedido!");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro no login: {ex.Message}");
            return false;
        }
    }
}