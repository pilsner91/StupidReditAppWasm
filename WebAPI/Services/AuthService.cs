using System.ComponentModel.DataAnnotations;
using Domain.Models;
using FileData;


namespace WebApi.Services;

public class AuthService : IAuthService
{
    private readonly FileContext context = new FileContext();


    public ICollection<User> users => context.Users;


    public Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = users.FirstOrDefault(u => u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return Task.FromResult(existingUser);
    }

    public Task RegisterUser(User user)
    {
        Console.WriteLine(users.Count);
        Console.WriteLine(user.Id + user.Password+user.UserName);
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
        
        users.Add(user);
        Console.WriteLine(users.Count);
        return Task.CompletedTask;
    }
}