using System.ComponentModel.DataAnnotations;

namespace CodeChallenge.Models.Types;

public class Maze
{
    [Key]
    public int _id { get; set; }
    public  string MazeIdentifier { get; set; }
    public string MazeDetails { get; set; }
}