using Microsoft.AspNetCore.Identity;
using suprimmil.Models;

namespace suprimmil.Services;
public interface IUserService
{
    Task<IdentityResult> CreateUserAsync(string email, string password, bool isAdmin = false);
    Task<List<User>> GetAllUsersAsync();
}