namespace Domain.DTOs;

public class SearchPostParametersDto
{
    public string? Username { get;}
    public int? UserId { get;}
    public bool? Published { get;}
    public string? TitleContains { get;}

    public SearchPostParametersDto(string? username, int? userId, bool? published, string? titleContains)
    {
        Username = username;
        UserId = userId;
        Published = published;
        TitleContains = titleContains;
    }
}