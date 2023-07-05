using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Reddit.Repository;

using Microsoft.EntityFrameworkCore;
using Model;

public class ForumRepository : IForumRepository
{

    private UnedditContext ctx;

    public ForumRepository(UnedditContext ctx)
        => this.ctx = ctx;

    public async Task Add(Forum obj)
    {
        ctx.Forums.Add(obj);
        await ctx.SaveChangesAsync();
    }

    public async Task Delete(Forum obj)
    {
        ctx.Forums.Remove(obj);
        await ctx.SaveChangesAsync();
    }

    public async Task<List<Forum>> Filter(Expression<Func<Forum, bool>> exp)
    {
        var query = ctx.Forums.Where(exp);
        var list = await query.ToListAsync();
        return list;
    }

    public async Task Update(Forum obj)
    {
        ctx.Forums.Update(obj);
        await ctx.SaveChangesAsync();
    }
    public async Task AddUser(Forum forum, Usuario user)
    {
        ForumUsuario usuarioForum = new ForumUsuario()
        {
            Forum = forum.Id,
            Usuarios = user.Id
        };

        await ctx.ForumUsuarios.AddAsync(usuarioForum);
        await ctx.SaveChangesAsync();
    }
    public async Task<Forum> Find(int id)
    {
        var forum = await ctx.Forums.FindAsync(id);
        return forum;
    }
    public async Task<List<Forum>> FindAll()
    {
        var foruns = ctx.Forums.Where(u => true);
        return await foruns.ToListAsync();
    }

    public  async Task<bool> IsMember(Usuario user, Forum forum)
    {
        bool isMember = await ctx.ForumUsuarios.AnyAsync(fu => fu.Usuarios == user.Id && fu.Forum == forum.Id);
        return isMember;
    }
    public async Task<List<Forum?>> GetUserGroups(Usuario user)
    {
        var query = ctx
            .ForumUsuarios
                .Include(u => u.ForumNavigation)
                .Where(fu => fu.Usuarios == user.Id)
                .Select(fu => fu.ForumNavigation);

        return await query.ToListAsync();
    }
}
