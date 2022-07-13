using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CodeChallenge.Models.Structs;

public struct MazePoint
{

    private sealed class XYEqualityComparer : IEqualityComparer<MazePoint>
    {
        public bool Equals(MazePoint x, MazePoint y)
        {
            return x.X == y.X && x.Y == y.Y;
        }

        public int GetHashCode(MazePoint obj)
        {
            return HashCode.Combine(obj.X, obj.Y);
        }
    }

    public MazePoint(int x, int y, bool visited = false) : this()
    {
        X = x;
        Y = y;
    }


    public static IEqualityComparer<MazePoint> XYComparer { get; } = new XYEqualityComparer();

    public int X { get; set; }

    public int Y { get; set; }
}