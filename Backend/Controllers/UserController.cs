using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Reddit.Controller;

using Model;
using DTO;
using Repository;
using Services;
using Microsoft.AspNetCore.Cors;

[ApiController]
[Route("user")]
[EnableCors("MainPolicy")]
public class UserController : ControllerBase
{

    [HttpPost("register")]
    public async Task<ActionResult> Register(
        [FromBody] UserRegisterDTO user,
        [FromServices] IRepository<Usuario> repo,
        [FromServices] ISecurityService security
    )
    {
        if (user.Email == "" || user.Username == "" || user.Password == "")
            return Ok(new ErrorDTO("Existem campos que não foram preenchidos."));

        var UsuarioExistente = await repo.Filter(u => u.Username == user.Username);

        if (UsuarioExistente.Count() > 0)
            return Ok(new ErrorDTO("O nome já existe no banco de dados."));

        UsuarioExistente = await repo.Filter(u => u.Email == user.Email);
        if (UsuarioExistente.Count() > 0)
            return Ok(new ErrorDTO("O e-mail registrado já existe no banco de dados."));

        Usuario newUser = new Usuario();

        newUser.DataNascimento = user.DataNascimento;
        newUser.Email = user.Email;
        newUser.Username = user.Username;

        var salt = security.GenerateSalt();
        var passwordSalty = user.Password + salt;
        byte[] hash = security.ApplyHash(passwordSalty);

        newUser.Salt = salt;
        newUser.Senha = hash;

        await repo.Add(newUser);

        return Ok();
    }


    [HttpPost("login")]
    public async Task<ActionResult<Usuario>> Login(
        [FromBody] Usuario user,
        [FromServices] IRepository<Usuario> repo
    )
    {
        var query = await repo.Filter(u => u.Email == user.Email);
        if (query.Count() > 0)
        {
            return Ok();
        }

        return NotFound();
    }


}