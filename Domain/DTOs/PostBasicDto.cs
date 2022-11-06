namespace Domain.DTOs;

public class PostBasicDto
{
    public int Id { get; }
    public string OwnerName { get; }
    public string Title { get; }
    public string PostText { get; set; }
    public bool Published { get;  }

    public PostBasicDto(int id, string ownerName, string title, bool published, string postText)
    {
        Id = id;
        OwnerName = ownerName;
        Title = title;
        Published = published;
        PostText = postText;
    }
}