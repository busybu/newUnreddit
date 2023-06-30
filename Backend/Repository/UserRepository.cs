using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Reddit.Repository;

using Microsoft.EntityFrameworkCore;
using Model;

public class UserRepository : IRepository<Usuario>
{

    private UnedditContext ctx;

    public UserRepository(UnedditContext ctx) 
        => this.ctx = ctx;

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
}
