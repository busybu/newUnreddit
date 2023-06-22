namespace Uneddit.Repository;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Reddit.Model;


public class UserRepository : IRepository<Usuario>
{

    private UnedditContext ctx;

    public UserRepository(UnedditContext ctx)
    {
        this.ctx = ctx;
    }

    public void Add(Usuario obj)
    {
        ctx.Usuarios.Add(obj);
        ctx.SaveChangesAsync();
    }

    public void Delete(Usuario obj)
    {
        ctx.Usuarios.Remove(obj);
        ctx.SaveChangesAsync();
    }

    public List<Usuario> Filter(Expression<Func<Usuario, bool>> exp)
    {
        return ctx.Usuarios.Where(exp.Compile()).ToList();
    }

    public void Update(Usuario obj)
    {
        ctx.Usuarios.Update(obj);
        ctx.SaveChangesAsync();
    }
}
