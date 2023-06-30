using Reddit.Model;
using Reddit.Repository;
using Security.Jwt;
using Reddit.DTO;

namespace Reddit.Services;

public interface IUserService
{
    Task<Usuario> ValidateJwt(JwtValue jwt);
}

public class UserService : IUserService
{
    private IJwtService jwtService;
    private UserRepository userRepository;

    public UserService(IJwtService jwtService, UserRepository userRepository)
    {
        this.jwtService = jwtService;
        this.userRepository = userRepository;
    }

    public async Task<Usuario> ValidateJwt(JwtValue jwt)
    {
        Usuario user;

        var token = jwtService.Validate<Jwt>(jwt.Value);

        if(!token.Authenticated)
            throw new InvalidDataException();
        
        user = await userRepository.Find(token.UserID);
        
        return user;
    }
}