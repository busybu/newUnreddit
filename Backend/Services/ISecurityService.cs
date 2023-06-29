using System;

namespace Reddit.Services;
public interface ISecurityService
{
    string GenerateSalt();
    byte[] hash(string pass, string salt);
    bool isPasswordEqualToPasswordBD(string pass, byte[] passHashedFromBd, string salt);
}