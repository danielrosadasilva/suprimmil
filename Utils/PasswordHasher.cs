using System.Security.Cryptography;
using System.Text;

namespace suprimmil.Utils;
public static class PasswordHasher
{
    private static readonly string FixedSalt = "hsfghjlk156as1f56se1hiogdsr";

    public static string Hash(string password)
    {
        // Combina a senha com o salt fixo
        string saltedPassword = password + FixedSalt;

        // Converte a senha para bytes
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(saltedPassword));

        // Converte o array de bytes em uma string hexadecimal
        var hashStringBuilder = new StringBuilder();
        foreach (byte b in bytes)
        {
            hashStringBuilder.Append(b.ToString("x2"));
        }

        return hashStringBuilder.ToString();
    }

    public static bool Verify(string correctHashedPassword, string password)
    {
        return correctHashedPassword == Hash(password);
    }
}