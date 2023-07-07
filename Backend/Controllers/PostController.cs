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
            Id = post.Id,
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
            int likes = await repo.GetLikes(post);

            PostDTO item = new PostDTO()
            {
                Id = post.Id,
                Titulo = post.Titulo,
                Conteudo = post.Conteudo,
                NomeForum = forumPost.Titulo,
                IdAutor = autorPost.Id,
                NomeAutor = autorPost.Username,
                DataCriacao = post.DataCriado,
                ForumID = post.Forum,
                Like = likes
            };
            result.Add(item);
        }
        return Ok(result.OrderByDescending(x => x.DataCriacao).ToList());

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
            int likes = await postService.GetLikes(post);
            Console.WriteLine(likes);
            PostDTO item = new PostDTO()
            {
                Id = post.Id,
                Titulo = post.Titulo,
                Conteudo = post.Conteudo,
                NomeAutor = autorPost.Username,
                IdAutor = autorPost.Id,
                DataCriacao = post.DataCriado,
                ForumID = post.Forum,
                Like = likes
            };
            result.Add(item);
        }
        return Ok(result.OrderByDescending(x => x.DataCriacao).ToList());

    }
    [HttpPost("likePost")]
    public async Task<ActionResult<List<ForumDTO>>> Like(
        [FromBody] PostLikeInteractionDTO interaction,
        [FromServices] IPostRepository postService,
        [FromServices] IUserRepository userService
    )
    {
        Usuario user;
        try
        {
            user = await userService.ValidateJwt(new JwtValue { Value = interaction.Jwt });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        if (user is null)
            return NotFound("Usuário não é válido");

        Post post;
        try
        {
            post = await postService.Find(interaction.IdPost);
        }
        catch (Exception ex)
        {
            Console.WriteLine("êta que nao tem esse post");
            return BadRequest(ex.Message);
        }
        bool hasLiked = await postService.isLiked(post, user);

        if (hasLiked)
        {
            Console.WriteLine("Descurtiu");
            await postService.Deslike(post, user);
            hasLiked = false;
            Console.WriteLine(hasLiked);
        }
        else
        {
            Console.WriteLine("Curtiu");
            await postService.Like(post, user);
            hasLiked = true;
            Console.WriteLine(hasLiked);
        }


        int likes = await postService.GetLikes(post);

        PostLikeInteractionDTO result = new PostLikeInteractionDTO()
        {
            IdPost = post.Id,
            Jwt = interaction.Jwt,
            HasLike = hasLiked,
            Quantity = likes,
            IdUser = user.Id
        };

        return Ok(result);
    }


}