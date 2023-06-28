using System;

namespace Reddit.Services;
public interface ISecurityService
{
    string GenerateSalt();
    byte[] ApplyHash(string pass);
}