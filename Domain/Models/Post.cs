namespace Domain.Models;

public class Post
{
    public int Id { get; set; }
    public User Owner { get; }
    public string Title { get; }

    public string PostText { get; set; }
    public bool Published { get; set; }

    public Post(User owner, string title, string postText)
    {
        Owner = owner;
        Title = title;
        PostText = postText;
    }
}