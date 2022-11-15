using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDao _postDao;
    private readonly IUserDao userDao;

    public PostLogic(IPostDao postDao, IUserDao userDao)
    {
        this._postDao = postDao;
        this.userDao = userDao;
    }

    public async Task<Post> CreateAsync(PostCreationDto dto)
    {
        User? user = await userDao.GetByIdAsync(dto.OwnerId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.OwnerId} was not found.");
        }

        Post post = new Post(user, dto.Title, dto.Text);
        
        ValidatePost(post);
        Console.WriteLine("id="+post.Id + "Owner: " + "doesn't work " +post.Owner.Password +" " + post.Owner.UserName+ post.Published +  " " + post.Title + " " + post.PostText);
        Post created = await _postDao.CreateAsync(post);
        return created;
    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        return _postDao.GetAsync(searchParameters);
    }

    public async Task UpdateAsync(PostUpdateDto dto)
    {
        Post? existing = await _postDao.GetByIdAsync(dto.Id);

        if (existing == null)
        {
            throw new Exception($"Post with ID {dto.Id} not found!");
        }

        User? user = null;
        if (dto.OwnerId != null)
        {
            user = await userDao.GetByIdAsync((int)dto.OwnerId);
            if (user == null)
            {
                throw new Exception($"User with id {dto.OwnerId} was not found.");
            }
        }

        if (dto.Published != null && existing.Published && !(bool)dto.Published)
        {
            throw new Exception("Cannot un-publish a published Post");
        }

        User userToUse = user ?? existing.Owner;
        string titleToUse = dto.Title ?? existing.Title;
        string postTextToUse = dto.PostText ?? existing.PostText;
        bool publishedUpdate = dto.Published ?? existing.Published;
        
        Post updated = new (userToUse, titleToUse, postTextToUse)
        {
            Published = publishedUpdate,
            Id = existing.Id,
        };

        ValidatePost(updated);

        await _postDao.UpdateAsync(updated);
    }

    public async Task DeleteAsync(int id)
    {
        Post? post = await _postDao.GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"Post with ID {id} was not found!");
        }

        if (!post.Published)
        {
            throw new Exception("Cannot delete un-completed Post!");
        }

        await _postDao.DeleteAsync(id);
    }

    public async Task<PostBasicDto> GetByIdAsync(int id)
    {
        Post? post = await _postDao.GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"Post with id {id} not found");
        }
    //    Console.WriteLine($"id = {post.Id} + username = {post.Owner.UserName} + Title = {post.Title} + published = {post.Published} + text = {post.PostText}");
        PostBasicDto returnvalue = new PostBasicDto(post.Id, post.Owner.UserName, post.Title, post.Published, post.PostText);

        return returnvalue;
    }

    private void ValidatePost(Post dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        // other validation stuff
    }
}