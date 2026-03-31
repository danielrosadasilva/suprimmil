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
}