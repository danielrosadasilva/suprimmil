using Microsoft.AspNetCore.Identity;
using suprimmil.Models;

namespace suprimmil.Services;
public interface IUserService
{
    Task<IdentityResult> CreateUserAsync(string email, string password, bool isAdmin = false);
    Task<List<User>> GetAllUsersAsync();
    Task<User?> GetCurrentUserAsync(string? userId);
    Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
}