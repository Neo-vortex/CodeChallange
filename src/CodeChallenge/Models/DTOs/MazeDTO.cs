namespace CodeChallenge.Models.DTOs;

public class MazeDTO
{
    public string Entrance { get; set; }
    public string GridSize { get; set; }
    public List<string> Walls { get; set; }
}