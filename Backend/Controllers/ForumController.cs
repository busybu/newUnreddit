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
        [FromServices] IForumRepository repo,
        [FromServices] UserService userService
    )
    {
        JwtValue jwt = new JwtValue(){Value = forum.Jwt};
        
        var user = await userService.ValidateJwt(jwt);

        if(user is null)
          return BadRequest("Não encontrado o usuário");
        
        var groupExist = await repo.Filter(u => u.Titulo == forum.Titulo);

        if(groupExist.Any())
            return BadRequest("O grupo já existe");

        Forum newForum = new Forum()
        {
            Titulo = forum.Titulo,
            Descricao = forum.Descricao,
            DataCriado = forum.DataCriacao,
            Quantidade = forum.Quantidade

        }
    }
}