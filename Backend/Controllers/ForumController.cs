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
[Route("forum")]
[EnableCors("MainPolicy")]

public class ForumController : ControllerBase
{

    [HttpPost("create")]
    public async Task<IActionResult> Create(
        [FromBody] ForumDTO forum,
        [FromServices] IRepository<Forum> repo,
        [FromServices] IUserRepository userService
    )
    {
        JwtValue jwt = new JwtValue() { Value = forum.Jwt };
        var user = await userService.ValidateJwt(jwt);

        if (user is null)
            return Ok(new ErrorDTO("Não encontrado o usuário"));

        var groupExist = await repo.Filter(u => u.Titulo == forum.Titulo);

        if (groupExist.Any())
            return Ok(new ErrorDTO("O grupo já existe"));

        Forum newForum = new Forum()
        {
            Titulo = forum.Titulo,
            Descricao = forum.Descricao,
            DataCriado = forum.DataCriacao,
            Quantidade = forum.Quantidade,
            Criador = user.Id,
        };
        await repo.Add(newForum);
        return Ok(new ErrorDTO("Grupo criado"));
    }

    [HttpPost("addUser")]
    public async Task<IActionResult> EnterUser(
        [FromBody] ForumUserDTO forumUser,
        [FromServices] IUserRepository userService,
        [FromServices] IForumRepository forumService
    )
    {
        Usuario user;
        JwtValue jwt = new JwtValue() { Value = forumUser.Jwt };
        try
        {
            user = await userService.ValidateJwt(jwt);
        }
        catch (Exception ex)
        {
            return Ok(ex);
        }
        if (user is null)
            return Ok("Usuário não é válido");

        var forumExist = await forumService.Find(forumUser.ForumId);
        if (forumExist is null)
            return Ok("Fórum não é válido");
        
        await forumService.AddUser(forumExist, user);
        return Ok("Usuario adicionado no fórum");
    }
}