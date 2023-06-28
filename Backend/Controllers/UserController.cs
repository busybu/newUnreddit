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

        return Ok("Usuário Registrado");
    }


    [HttpPost("login")]
    public async Task<ActionResult<Usuario>> Login(
        [FromBody] Usuario user,
        [FromServices] ISecurityService security,
        [FromServices] IRepository<Usuario> repo,
        [FromServices] IJwtService jwtService
     )
    {
        var usertoBeCompared = new LoginDTO();

        var usersQuery = await repo.Filter(u => u.Email == user.Email);

        usertoBeCompared.UserExist = usersQuery.Count() > 0;
        if (usersQuery.Count() == 0)
        {
            return Ok("Usuário não existe.");
        }

        return NotFound();
    }


}