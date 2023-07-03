using System.Linq.Expressions;

namespace Reddit.Repository;

using Microsoft.EntityFrameworkCore;
using Model;
using Reddit.DTO;
using Security.Jwt;

public class UserRepository : IUserRepository
{

    private UnedditContext ctx;
    private IJwtService jwtService;
    private UserRepository userRepository;
    public UserRepository(UnedditContext ctx, IJwtService jwtService)

    {
        this.ctx = ctx;
        this.jwtService = jwtService;

    }
    public async Task Add(Usuario obj)
    {
        ctx.Usuarios.Add(obj);
        await ctx.SaveChangesAsync();
    }

    public async Task Delete(Usuario obj)
    {
        ctx.Usuarios.Remove(obj);
        await ctx.SaveChangesAsync();
    }

    public async Task<List<Usuario>> Filter(Expression<Func<Usuario, bool>> exp)
    {
        var query = ctx.Usuarios.Where(exp);
        var list = await query.ToListAsync();
        return list;
    }

    public async Task Update(Usuario obj)
    {
        ctx.Usuarios.Update(obj);
        await ctx.SaveChangesAsync();
    }
    public async Task<Usuario> Find(int id)
    {
        var user = await ctx.Usuarios.FindAsync(id);
        return user;
    }


    public async Task<Usuario> ValidateJwt(JwtValue jwt)
    {
        Usuario user;

        var token = jwtService.Validate<Jwt>(jwt.Value);

        if (!token.Authenticated)
            throw new InvalidDataException();

        user = await this.Find(token.UserID);

        return user;
    }
}
