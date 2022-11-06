namespace Domain.DTOs;

public class PostCreationDto
{
    public int OwnerId { get; }
    public string Title { get; }
    public string Text { get; }

    public PostCreationDto(int ownerId, string title, string text)
    {
        OwnerId = ownerId;
        Title = title;
        Text = text;
    }
}