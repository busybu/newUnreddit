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
        byte[] hash = security.hash(user.Password, salt);

        newUser.Salt = salt;
        newUser.Senha = hash;

        await repo.Add(newUser);

        Console.WriteLine("Registro:");
        Console.WriteLine(user.Password);
        Console.WriteLine(newUser.Salt);

        return Ok(new ErrorDTO("Usuário Registrado"));
    }


    [HttpPost("login")]
    public async Task<ActionResult<Jwt>> Login(
        [FromBody] UserLoginDTO userLogin,
        [FromServices] ISecurityService security,
        [FromServices] IRepository<Usuario> repo,
        [FromServices] IJwtService jwtService
     )
    {
        var result = new LoginDTO();

        var usersQuery = await repo.Filter(u => u.Email == userLogin.Email);

        result.UserExist = usersQuery.Count() > 0;
        if (!result.UserExist)
            return Ok(result);

        Usuario target = usersQuery.First();

        Console.WriteLine("Login:");
        Console.WriteLine(userLogin.Password);
        Console.WriteLine(target.Salt);

        if (security.isPasswordEqualToPasswordBD(userLogin.Password, target.Senha, target.Salt))
        {
            string token = jwtService.GetToken<Jwt>(new Jwt { UserID = target.Id, Authenticated = true });

    
            result.Jwt = token;
            result.Sucess = true;


            return Ok(result);
        }

        result.Sucess = false;
        return Ok(result);
    }

    [HttpPost("validate")]
    public async Task<ActionResult<Jwt>> ValidateJwt(
        [FromServices] IJwtService jwtService,
        [FromBody] JwtValue jwt
    )
    {
        if (jwt.Value == "" || jwt.Value is null)
            return Ok(new Jwt { Authenticated = false });

        try
        {
            var token = jwtService.Validate<Jwt>(jwt.Value);
            token.Authenticated = true;
            return Ok(token);
        }
        catch (Exception ex)
        {
            return Ok(new Jwt { Authenticated = false });
        }

    }

    [HttpPost("get")]
    public async Task<ActionResult<UserInfoDTO>> GetSingle(
        [FromBody] JwtValue jwt,
        [FromServices] IRepository<Usuario> userRepository,
        [FromServices] IJwtService jwtService
    ) 
    {
        System.Console.WriteLine("JWT: " + jwt.Value);

        Usuario user;
        try {
            var token = jwtService.Validate<Jwt>(jwt.Value);

            if(!token.Authenticated)
                return BadRequest("nao ta autenticado");
            
            System.Console.WriteLine(token.UserID);
            var query = await userRepository.Filter(u => u.Id == token.UserID);
            user = query.FirstOrDefault();
        } 
        catch (Exception e) {
            return BadRequest("invalido");
        }

        if(user is null)
            return BadRequest("nulo");

        UserInfoDTO result = new UserInfoDTO()
        {
            UserName = user.Username,
            Email = user.Email,
            ProfilePic = 0
        };

        return Ok(result);
    }
}