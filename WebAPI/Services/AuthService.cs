using System.ComponentModel.DataAnnotations;
using Application.DaoInterfaces;
using Domain.Models;
using FileData;


namespace WebApi.Services;

public class AuthService : IAuthService
{

    private readonly IUserDao userDao;

    public AuthService(IUserDao userDao)
    {
        this.userDao = userDao;
    }

    public async Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = await userDao.GetByUsernameAsync(username);
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return await Task.FromResult(existingUser);
    }

    public Task RegisterUser(User user)
    {
        if (string.IsNullOrEmpty(user.UserName))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(user.Password))
        {
            throw new ValidationException("Password cannot be null");
        }
        // Do more user info validation here
        
        // save to persistence instead of list
        
        userDao.CreateAsync(user);
        return Task.CompletedTask;
    }
}