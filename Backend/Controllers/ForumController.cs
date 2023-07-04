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

    [HttpPost("listForums")]
    public async Task<ActionResult<List<ForumDTO>>> ListForum(
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
            return BadRequest(ex.Message);
        }
        if (user is null)
            return NotFound("Usuário não é válido");

        var foruns = await forumService.FindAll();
        List<ForumDTO> result = new List<ForumDTO>();
        
        foreach (var forum in foruns)
        {
            ForumDTO item = new ForumDTO()
            {
                Titulo = forum.Titulo,
                IsMember = await forumService.IsMember(user, forum),
                Quantidade = forum.Quantidade,
            };
            result.Add(item);
        }
        return Ok(result.OrderBy(x=> x.Quantidade));
    }


    [HttpPost("listUserForums")]
    public async Task<ActionResult<List<ForumDTO>>> GetUserForum(
        [FromBody] JwtValue jwt,
        [FromServices] IUserRepository userService,
        [FromServices] IForumRepository forumService
    )
    {
        Console.WriteLine("to aq");
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

        var foruns = await forumService.GetUserGroups(user);
        List<ForumDTO> result = new List<ForumDTO>();
        
        foreach (var forum in foruns)
        {
            ForumDTO item = new ForumDTO()
            {
                Titulo = forum.Titulo,
                Id = forum.Id
            };
            result.Add(item);
        }
        return Ok(result);
    }
}