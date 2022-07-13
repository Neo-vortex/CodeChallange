namespace CodeChallenge.Models.DTOs;

public record MazeDTO
{
    public string Entrance { get; set; }
    public string GridSize { get; set; }
    public List<string> Walls { get; set; }
    
    public  string? Hash { get; set; }
}