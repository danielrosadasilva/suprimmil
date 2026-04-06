using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using suprimmil.Models;

namespace suprimmil.Services;

public class UserService(UserManager<User> userManager) : IUserService
{
    private readonly UserManager<User> _userManager = userManager;

    public async Task<IdentityResult> CreateUserAsync(string email, string password, bool isAdmin = false)
    {
        var user = new User
        {
            UserName = email,
            Email = email,
            IsAdmin = isAdmin
        };

        var result = await _userManager.CreateAsync(user, password);
        return result;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<User?> GetCurrentUserAsync(string? userId)
    {
        if (string.IsNullOrEmpty(userId)) return null;
        return await _userManager.FindByIdAsync(userId);
    }

    public async Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return IdentityResult.Failed(new IdentityError { Description = "Usuário não encontrado." });

        return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
    }
}