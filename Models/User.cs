using Microsoft.AspNetCore.Identity;

namespace suprimmil.Models;

public class User : IdentityUser<int>
{
    public bool IsAdmin { get; set; }
}