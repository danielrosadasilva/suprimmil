using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using suprimmil.Models;

public class AppUserClaimsPrincipalFactory(
    UserManager<User> userManager,
    RoleManager<IdentityRole<int>> roleManager,
    IOptions<IdentityOptions> optionsAccessor) : UserClaimsPrincipalFactory<User, IdentityRole<int>>(userManager, roleManager, optionsAccessor)
{
    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        identity.AddClaim(new Claim("IsAdmin", user.IsAdmin ? "true" : "false"));

        return identity;
    }
}
