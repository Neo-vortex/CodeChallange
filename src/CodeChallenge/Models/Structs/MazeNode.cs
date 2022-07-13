namespace CodeChallenge.Models.Structs;

public class MazeNode : MazePointHeuristic
{
    public sealed class MazeNodeEqualityComparer :   IEqualityComparer<MazeNode> 
    {
        public bool Equals(MazeNode x, MazeNode y)
        {

            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            return (x.Point.X == y.Point.X) && (x.Point.Y == y.Point.Y)  ;
        }

        public int GetHashCode(MazeNode obj)
        {
            return obj.Point.GetHashCode();
        }
    }

    public static IEqualityComparer<MazeNode> CurrentComparer { get; } = new MazeNodeEqualityComparer();

    
    public  MazeNode Parrent { get; set; }
    
}