using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Post
{
    public int Id { get; set; }
    public User Owner { get; private set; }
    public string Title { get; private set; }

    public string PostText { get; set; }
    public bool Published { get; set; }

    public Post(User owner, string title, string postText)
    {
        Owner = owner;
        Title = title;
        PostText = postText;
    }

    private Post()
    {
    }
}