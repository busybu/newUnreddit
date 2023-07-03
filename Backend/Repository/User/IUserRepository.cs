namespace Reddit.Repository;

using Model;
using Reddit.DTO;

public interface IUserRepository : IRepository<Usuario>
{
    Task<Usuario> Find(int id);
    Task<Usuario> ValidateJwt(JwtValue jwt);
    
}
