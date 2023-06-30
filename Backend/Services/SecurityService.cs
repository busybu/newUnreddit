using System;
using System.Text;
using System.Security.Cryptography;

namespace Reddit.Services;

public interface ISecurityService
{
    string GenerateSalt();
    byte[] hash(string pass, string salt);
    bool isPasswordEqualToPasswordBD(string pass, byte[] passHashedFromBd, string salt);
}
public class SecurityService : ISecurityService
{
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
    
    public bool isPasswordEqualToPasswordBD(string pass, byte[] passHashedFromBd, string salt)
    {
        var passwordHashed = this.hash(pass, salt);

        Console.WriteLine(Convert.ToBase64String(passwordHashed));
        Console.WriteLine(Convert.ToBase64String(passHashedFromBd));

        for (int i = 0; i < passHashedFromBd.Length; i++)
        {   
            if(passwordHashed[i] != passHashedFromBd[i])
                return false; 
        }
        
        return true;
    }

    public byte[] hash(string pass, string salt)
    {
        using var sha = SHA256.Create();

        var passwordSalty = pass + salt;
        var passwordSaltyBytes = Encoding.UTF8.GetBytes(passwordSalty);
        var hashBytes = sha.ComputeHash(passwordSaltyBytes);

        return hashBytes;
    }
}