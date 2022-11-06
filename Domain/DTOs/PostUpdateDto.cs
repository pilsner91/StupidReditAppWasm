namespace Domain.DTOs;

public class PostUpdateDto
{
    public int Id { get; }
    public int? OwnerId { get; set; }
    public string? Title { get; set; }
    public bool? Published { get; set; }
    public string? PostText { get; set; }

    public PostUpdateDto(int id)
    {
        Id = id;
    }
}