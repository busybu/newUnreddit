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
        [FromServices] IRepository<Post> repo,
        [FromServices] IForumRepository forum,
        [FromServices] IUserRepository user
    )
    {
        Usuario isUser;
        try{
            isUser = await user.ValidateJwt(new JwtValue{Value = post.Jwt});
        }
        catch(Exception ex){
            return Ok(ex.Message);
        }
        if(isUser is null)
           return Ok("Usuario não existe");
        
        var forumExist = await forum.Filter(u => u.Id == post.ForumID);

        if(forumExist.Count() == 0)
            return Ok(new ErrorDTO("Esse fórum não existe"));

        var newForum = forumExist.First(u => u.Id == post.ForumID);
        
        Post newPost = new Post()
        {
            Titulo = post.Titulo,
            Conteudo = post.Conteudo,
            Anexo = null,
            Autor = isUser.Id,
            Forum = newForum.Id,
        };
        await repo.Add(newPost);
        return Ok(new ErrorDTO("Post criado")); 
    }
}