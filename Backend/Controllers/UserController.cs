using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Reddit.Controller;

using Model;
using Uneddit.Repository;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{

    [HttpPost("register")]
    public async Task<ActionResult<Usuario>> Register(
        [FromBody] UserRegisterDTO user,
        [FromServices] IRepository<Usuario> repo
    )
    {
        var UsuarioExistente = repo.Filter(u => u.Username == user.Username);
        if(UsuarioExistente.Count() > 0)
            return BadRequest("O nome já existe no banco de dados");

        UsuarioExistente = repo.Filter(u => u.Email == user.Email);
        if(UsuarioExistente.Count() > 0)
            return BadRequest("O e-mail registrado já existe no banco de dados");
        
        // repo.Add(user);
        return Ok();
    }


    [HttpPost("login")]
    public async Task<ActionResult<Usuario>> Login(
        [FromBody] Usuario user,
        [FromServices] IRepository<Usuario> repo
    )
    {
        var query = repo.Filter(u => u.Email == user.Email);
        if(query.Count() > 0)
        {
            return Ok();
        }

        return NotFound();
    }

    
}