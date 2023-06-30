namespace Reddit.Repository;

using Model;

public interface IUserRepository : IRepository<Usuario>
{
    Task<Usuario> Find(int id);

}
