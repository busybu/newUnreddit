using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Reddit.Controller;

using Model;
using DTO;
using Repository;
using Services;

using Microsoft.AspNetCore.Cors;
using Security.Jwt;


[ApiController]
[Route("post")]
[EnableCors("MainPolicy")]

public class PostController : ControllerBase
{
    [HttpPost("create")]

    public async Task<IActionResult> Create(
        [FromBody] PostDTO post,
        [FromServices] IPostRepository repo,
        [FromServices] IForumRepository forum,
        [FromServices] IUserRepository user
    )
    {
        Usuario isUser;
        try
        {
            isUser = await user.ValidateJwt(new JwtValue { Value = post.Jwt });
        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }
        if (isUser is null)
            return Ok("Usuario não existe");

        var forumExist = await forum.Filter(u => u.Id == post.ForumID);

        if (forumExist.Count() == 0)
            return Ok(new ErrorDTO("Esse fórum não existe"));

        var newForum = forumExist.First(u => u.Id == post.ForumID);

        Post newPost = new Post()
        {
            Titulo = post.Titulo,
            Conteudo = post.Conteudo,
            Anexo = null,
            DataCriado = post.DataCriacao,
            Autor = isUser.Id,
            Forum = newForum.Id,
        };
        await repo.Add(newPost);
        return Ok(new ErrorDTO("Post criado"));
    }

    [HttpPost("listPost")]
    public async Task<ActionResult<List<PostDTO>>> ListPost(
        [FromBody] JwtValue jwt,
        [FromServices] IPostRepository repo,
        [FromServices] IForumRepository forum,
        [FromServices] IUserRepository userService
    )
    {
        Usuario user;
        try
        {
            user = await userService.ValidateJwt(jwt);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        if (user is null)
            return NotFound("Usuário não é válido");

        var posts = await repo.FindAll();
        List<PostDTO> result = new List<PostDTO>();

        foreach (var post in posts)
        {
            var autorPost = await userService.Find(post.Autor);
            var forumPost = await forum.Find(post.Forum);

            PostDTO item = new PostDTO()
            {
                Titulo = post.Titulo,
                Conteudo = post.Conteudo,
                NomeForum = forumPost.Titulo,
                NomeAutor = autorPost.Username,
                DataCriacao = post.DataCriado,
                ForumID = post.Forum
            };
            result.Add(item);
        }
        return Ok(result.OrderBy(x => x.DataCriacao).ToList());

    }

    [HttpPost("getPostFromForum")]
    public async Task<ActionResult<List<ForumDTO>>> GetPostFromForum(
        [FromBody] ForumUserDTO forumUser,
        [FromServices] IPostRepository postService,
        [FromServices] IForumRepository forumService,
        [FromServices] IUserRepository userService
    )
    {
        var posts = await postService.GetPostsForum(forumUser.ForumId);
        List<PostDTO> result = new List<PostDTO>();

        foreach (var post in posts)
        {
            var autorPost = await userService.Find(post.Autor);

            PostDTO item = new PostDTO()
            {
                Titulo = post.Titulo,
                Conteudo = post.Conteudo,
                NomeAutor = autorPost.Username,
                DataCriacao = post.DataCriado,
                ForumID = post.Forum
            };
            result.Add(item);
        }
        return Ok(result.OrderBy(x => x.DataCriacao).ToList());

    }
}