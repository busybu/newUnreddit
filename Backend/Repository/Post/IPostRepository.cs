namespace Reddit.Repository;

using Model;
using Reddit.DTO;

public interface IPostRepository : IRepository<Post>
{
    Task<List<Post>> FindAll();
}