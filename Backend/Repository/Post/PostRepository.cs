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
    public async Task<Post> Find(int id)
    {
        var post = await ctx.Posts.FindAsync(id);
        return post;
    }
    public async Task<Post> FindAutor(int id, int postAutor)
    {
        var post = await ctx.Posts.Where
        (u => u.Id == id && u.Autor == postAutor)
        .FirstOrDefaultAsync();
        return post;
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
    public async Task Like(Post post, Usuario user)
    {
        UpVote like = new UpVote()
        {
            Post = post.Id,
            Usuario = user.Id
        };

        await ctx.UpVotes.AddAsync(like);
        await ctx.SaveChangesAsync();
    }
    public async Task<int> GetLikes(Post post)
    {
        var query = ctx
        .UpVotes.
        Where(u => u.Post == post.Id);
        int likes = await query.CountAsync();
        return likes;
    }
    public async Task<bool> isLiked(Post post, Usuario user)
    {
        bool isLike = await ctx
        .UpVotes.
        AnyAsync
        (up => up.Post == post.Id &&
        up.Usuario == user.Id);

        return isLike;
    }
    public async Task Deslike(Post post, Usuario user)
    {
        bool isLike = await isLiked(post, user);
        
        if (isLike)
        {
            UpVote like = new UpVote();
            like = await ctx.UpVotes.FirstAsync
            (u => u.Post == post.Id &&
             u.Usuario == user.Id);
            ctx.UpVotes.Remove(like);
        }
        
        await ctx.SaveChangesAsync();

    }
}
