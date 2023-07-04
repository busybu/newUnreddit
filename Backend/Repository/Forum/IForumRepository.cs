namespace Reddit.Repository;

using Model;
using Reddit.DTO;

public interface IForumRepository : IRepository<Forum>
{
    Task AddUser(Forum forum, Usuario user);
    Task<Forum> Find(int id);
}


