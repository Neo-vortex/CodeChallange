using CodeChallenge.Models.Structs;
using CodeChallenge.Models.Types;

namespace CodeChallenge.Utilities;

public static class Extentions
{  
    public static int CalculateFScore(this MazePoint point, Maze maze)
    {
        var G = Math.Abs(maze.End.X - point.X) + Math.Abs(maze.End.Y - point.Y);
        var H = Math.Abs(maze.Start.X - point.X) + Math.Abs(maze.Start.Y - point.Y);
        return G + H;
    }
    public static IEnumerable<MazePoint> CalculateNeighbours(this Maze maze, MazePoint point)
    {
        var neighbours = new List<MazePoint>
        {
            new(point.X - 1, point.Y),
            new(point.X + 1, point.Y ),
            new(point.X, point.Y - 1),
            new(point.X, point.Y + 1)
        };
        neighbours.RemoveAll(p => (p.X < 0 || p.X >= maze.Width || p.Y < 0 || p.Y >= maze.Height) || maze.Walls.Contains(p));
        return neighbours;
    }
    public static MazePoint[] StringToMazePointArray(string s)
    {
        var parts = s.Split(",");
        var result = new MazePoint[parts.Length];

        Parallel.For(0, parts.Length, i =>
        {
            result[i] = ToMazePoint(parts[i].Replace(@"""", "").Trim());
        });
        return result;
    }

    public static MazePoint ToMazePoint(this string s)
    {
        return new MazePoint
        {
            X = s[0] - 65,
            Y = int.Parse(s[1].ToString())  - 1
        };
    }
    public static string ToPlainText(this MazePoint point)
    {
        return string.Concat((char) (point.X + 65), (point.Y + 1).ToString());
    }

    public  static string Tojson(this object obj)
    {
        return System.Text.Json.JsonSerializer.Serialize(obj);
    }
    
}