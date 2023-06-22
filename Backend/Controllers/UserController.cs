using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Reddit.Controller;

using Model;
using Uneddit.Repository;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    [HttpPost("/register")]
    public ActionResult<Usuario> Register(
        [FromBody] Usuario user,
        [FromServices] IRepository<Usuario> repo
    )
    {
        var LisUsuarioExistente = repo.Filter(u => u.Username == user.Username);
        if(LisUsuarioExistente.Count() > 0)
            return BadRequest("Escolhe outro nome pae");

        repo.Add(user);
        return Ok();
    }
}