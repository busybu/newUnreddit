namespace Reddit.Repository;

using Model;
using Reddit.DTO;

public interface IPostRepository : IRepository<Post>
{
    Task<Post> Find(int id);
    Task<List<Post>> FindAll();
    Task<Post> FindAutor(int id, int postAutor);
    Task<List<Post>> GetPostsForum(int id);
    Task Like(Post post, Usuario user);
    Task<int> GetLikes(Post post);
    Task<bool> isLiked(Post post, Usuario user);
    Task Deslike(Post post, Usuario user);

}