using System;
using System.Text;
using System.Security.Cryptography;

namespace Reddit.Services;

public class SecurityService : ISecurityService
{
    public byte[] ApplyHash(string pass)
    {
        using var sha = SHA256.Create();
        var passwordBytes = Encoding.UTF8.GetBytes(pass);
        var hashBytes = sha.ComputeHash(passwordBytes);
        return hashBytes;
    }

    public string GenerateSalt()
    {
        Random rd = new Random();
        int length = 12;

        byte[] salt = new byte[length];
        rd.NextBytes(salt);

        var saltTo64 = Convert.ToBase64String(salt);
        Console.WriteLine(saltTo64);
        return saltTo64;
    }
}