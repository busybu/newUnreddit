namespace Reddit.Repository;

using Model;
using Reddit.DTO;

public interface IForumRepository : IRepository<Forum>
{
    Task AddUser(Forum forum, Usuario user);
    Task<Forum> Find(int id);
    Task<Forum> FindByName(string name);
    Task<List<Forum>> FindAll();
    Task<bool> IsMember(Usuario user, Forum forum);
    Task<List<Forum?>> GetUserGroups(Usuario user);
}


