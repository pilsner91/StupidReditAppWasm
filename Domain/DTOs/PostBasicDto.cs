namespace Domain.DTOs;

public class PostBasicDto
{
    public int Id { get; }
    public string OwnerName { get; }
    public string Title { get; }
    public bool Published { get;  }

    public PostBasicDto(int id, string ownerName, string title, bool published)
    {
        Id = id;
        OwnerName = ownerName;
        Title = title;
        Published = published;
    }
}