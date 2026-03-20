using Microsoft.AspNetCore.Identity;
using suprimmil.Models;
using System.Security.Cryptography;
using System.Text;

namespace suprimmil.Services;

public class CustomPasswordHasher : IPasswordHasher<User>
{
    private static readonly string FixedSalt = "hsfghjlk156as1f56se1hiogdsr";

    // Seu método de hash antigo
    private string LegacyHash(string password)
    {
        string saltedPassword = password + FixedSalt;
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(saltedPassword));
        
        var hashStringBuilder = new StringBuilder();
        foreach (byte b in bytes)
        {
            hashStringBuilder.Append(b.ToString("x2"));
        }
        return hashStringBuilder.ToString();
    }

    public string HashPassword(User user, string password)
    {
        // Para NOVAS senhas, usa o algoritmo padrão do Identity
        var identityHasher = new PasswordHasher<User>();
        return identityHasher.HashPassword(user, password);
    }

    public PasswordVerificationResult VerifyHashedPassword(
        User user, 
        string hashedPassword, 
        string providedPassword)
    {
        // Se for hash do Identity (começa com AQAAAA)
        if (hashedPassword.StartsWith("AQAAAA"))
        {
            var identityHasher = new PasswordHasher<User>();
            return identityHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
        }
        
        // Se for hash legado (seu algoritmo)
        if (hashedPassword == LegacyHash(providedPassword))
        {
            // Senha válida, mas precisa migrar para o Identity
            return PasswordVerificationResult.SuccessRehashNeeded;
        }
        
        return PasswordVerificationResult.Failed;
    }
}