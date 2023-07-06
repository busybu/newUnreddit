using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Reddit.Repository;

using Microsoft.EntityFrameworkCore;
using Model;

public class PostRepository : IPostRepository
{

    private UnedditContext ctx;

    public PostRepository(UnedditContext ctx)
        => this.ctx = ctx;

    public async Task Add(Post obj)
    {
        ctx.Posts.Add(obj);
        await ctx.SaveChangesAsync();
    }

    public async Task Delete(Post obj)
    {
        ctx.Posts.Remove(obj);
        await ctx.SaveChangesAsync();
    }

    public async Task<List<Post>> Filter(Expression<Func<Post, bool>> exp)
    {
        var query = ctx.Posts.Where(exp);
        var list = await query.ToListAsync();
        return list;
    }

    public async Task Update(Post obj)
    {
        ctx.Posts.Update(obj);
        await ctx.SaveChangesAsync();
    }
    public async Task<List<Post>> FindAll()
    {
        var posts = ctx.Posts.Where(u => true);
        return await posts.ToListAsync();
    }

     public async Task<List<Post>> GetPostsForum(int id)
    {
        var query = ctx
            .Posts
            .Where(u => u.Forum == id);
        return await query.ToListAsync();
    }
}
