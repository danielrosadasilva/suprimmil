using Microsoft.EntityFrameworkCore;
using suprimmil.Context;
using suprimmil.Models;

namespace suprimmil.Repository;

public class UserRepository(AppDbContext context)
{
    public readonly AppDbContext _context = context;

    public async Task<User?> GetUserByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

     public async Task<User> Update(User user)
    {
        _context.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }
}