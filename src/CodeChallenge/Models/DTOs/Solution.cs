using CodeChallenge.Models.Enums;

namespace CodeChallenge.Models.DTOs;

public class Solution
{
    public Solution(string type)
    {
        Type = type switch
        {
            "min" => SolutionType.MIN,
            "max" => SolutionType.MAX,
            _ => throw new InvalidDataException("Solution Type in unknown")
        };
    }

    public  SolutionType Type { get; set; }
}