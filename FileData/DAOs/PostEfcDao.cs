using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using FileData.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FileData.DAOs;

public class PostEfcDao:IPostDao
{

    private PostContext context;

    public PostEfcDao(PostContext context)
    {
        this.context = context;
    }
    
    public async Task<Post> CreateAsync(Post post)
    {
        EntityEntry<Post> added = await context.Posts.AddAsync(post);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        IQueryable<Post> query = context.Posts.Include(post => post.Owner).AsQueryable();
        
        if (!string.IsNullOrEmpty(searchParameters.Username))
        {
            // we know username is unique, so just fetch the first
            query = query.Where(todo =>
                todo.Owner.UserName.ToLower().Equals(searchParameters.Username.ToLower()));
        }
        
        if (searchParameters.UserId != null)
        {
            query = query.Where(t => t.Owner.Id == searchParameters.UserId);
        }
        
        if (searchParameters.Published != null)
        {
            query = query.Where(t => t.Published == searchParameters.Published);
        }
        
        if (!string.IsNullOrEmpty(searchParameters.TitleContains))
        {
            query = query.Where(t =>
                t.Title.ToLower().Contains(searchParameters.TitleContains.ToLower()));
        }

        List<Post> result = await query.ToListAsync();
        return result;
    }

    public async Task UpdateAsync(Post post)
    {
        context.ChangeTracker.Clear();
        context.Posts.Update(post);
        await context.SaveChangesAsync();
    }

    public async Task<Post?> GetByIdAsync(int postId)
    {
        Post? found = await context.Posts.Include(post => post.Owner).SingleOrDefaultAsync(p => p.Id==postId);
        return found;
    }

    public async Task DeleteAsync(int id)
    {
        Post? existing = await GetByIdAsync(id);
        if (existing == null)
        {
            throw new Exception($"Post with id {id} not found");
        }

        context.Posts.Remove(existing);
        await context.SaveChangesAsync();

    }
}