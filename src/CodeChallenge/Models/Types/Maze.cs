using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CodeChallenge.Models.Identity;
using CodeChallenge.Models.Structs;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace CodeChallenge.Models.Types;

public class Maze
{
    public Maze(){}


    private async void  GenerateFirstStageImage()
    {
        using Image<Rgba32> image = new(800, 800);
        image.Mutate(ctx => ctx.Fill(Color.Gray));
        foreach (var wall in Walls)
        {
            image.Mutate(ctx => ctx.Fill(Color.Black, new Rectangle(wall.X * 800/Width, wall.Y * 800/Height, 800/Width, 800/Width)));
        }
        image.Mutate(ctx => ctx.Fill(Color.DarkGreen, new Rectangle(Start.X * 800/Width, Start.Y * 800/this.Height, 800/Width, 800/Width)));
        var memoryStream = new MemoryStream();
        await image.SaveAsync (memoryStream,PngFormat.Instance);
        FirstStageImage = memoryStream.ToArray();
        await memoryStream.DisposeAsync();
    }
    
    private MazePoint[] StringToMazePointArray(string s)
    {
        var parts = s.Split(",");
        var result = new MazePoint[parts.Length];
        for (var i = 0; i < parts.Length; i++)
        {
            result[i] = StringToMazePoint(parts[i].Replace(@"""", "").Trim());
        }
        return result;
    }

   private static MazePoint StringToMazePoint(string s)
    {
        return new MazePoint
        {
            X = s[0] - 65,
            Y = int.Parse(s[1].ToString())  - 1
        };
    }
    
    [Key]
    public int _id { get; set; }
    
    public bool IsSolved { get; set; }
    public int Height { get; set; }
    public  int Width { get; set; }
    public MazePoint[] Walls { get; set; }
    public MazePoint Start { get; set; }
    public  MazePoint End { get; set; }
    public  string StringRepresentation { get; set; }
    public MazePoint[]? ShortestPath { get; set; }
    public  MazePoint[]? LongestPath { get; set; }
    public  string Hash { get; set; }
    public byte[]? FirstStageImage { get; set; }
    public byte[]? SecondStageImage { get; set; }
    
    [JsonIgnore]
    public ICollection<ApplicationUser>? Users { get; set; }
    
    
    public Maze(IEnumerable<string> walls, string entry, string size, string textRepresentation)
    {
        try
        {
            Height = size[0] - '0';
            Width = size[2] - '0' ;
            Start = StringToMazePoint(entry);
            var srt = System.Text.Json.JsonSerializer.Serialize(walls.ToList()).Replace("[", "").Replace("]", "");
            Walls = StringToMazePointArray(srt );
            Hash =  Utilities.UtilityFunctions.CalculateHash( string.Concat(new { walls = srt , entry ,size}) , true).ToString();
            StringRepresentation = textRepresentation;
            GenerateFirstStageImage();
            ShortestPath = new []{new MazePoint(){X = -1, Y = -1}};
            LongestPath = new []{new MazePoint(){X = -1, Y = -1}};
            SecondStageImage = new byte[] { 0 };
        }
        catch (Exception e)
        {
            throw new Exception("Invalid maze input, " + e.Message );
        }
    }
    
    public Maze(string walls, string entry, string size, string textRepresentation)
    {
        try
        {
            Height = size[0] - '0';
            Width = size[2] - '0' ;
            Start = StringToMazePoint(entry);
            Walls = StringToMazePointArray(walls);
            Hash =  Utilities.UtilityFunctions.CalculateHash( string.Concat(new {walls , entry ,size}) , true).ToString();
            StringRepresentation = textRepresentation;
            GenerateFirstStageImage();
            ShortestPath = new []{new MazePoint(){X = -1, Y = -1}};
            LongestPath = new []{new MazePoint(){X = -1, Y = -1}};
            SecondStageImage = new byte[] { 0 };
        }
        catch (Exception e)
        {
            throw new Exception("Invalid maze input, " + e.Message );
        }
    }

}