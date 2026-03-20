using Microsoft.AspNetCore.Identity;

namespace suprimmil.Models;

public class User : IdentityUser<int>
{
    public string Password { get; set; } = null!;
    public int LoginAttempts { get; set; }
    public bool IsActive { get; set; }
    public bool IsLocked { get; set; }
    public bool IsAdmin { get; set; }
}