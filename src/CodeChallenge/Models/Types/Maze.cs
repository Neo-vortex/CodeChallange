using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int _id { get; set; }
    public  bool NoSolutionDefinitely { get; set; }
    public  bool PrimitiveAnalysis { get; set; }
    public bool MinSolved { get; set; }
    public bool MaxSolved { get; set; }
    public int Height { get; set; }
    public  int Width { get; set; }
    public MazePoint[] Walls { get; set; }
    public MazePoint Start { get; set; }
    public  MazePoint End { get; set; }
    public  string StringRepresentation { get; set; }
    public MazePoint[]? ShortestPath { get; set; }
    public  MazePoint[]? LongestPath { get; set; }
    
    public  MazePointHeuristic[]? Heuristics { get; set; }
    public  string Hash { get; set; }
    
    public byte[]? Image { get; set; }
    
    [JsonIgnore]
    public ICollection<ApplicationUser>? Users { get; set; }
    
    
    public Maze(IEnumerable<string> walls, string entry, string size, string textRepresentation)
    {
        try
        {
            Height = size[0] - '0';
            Width = size[2] - '0' ;
            Start = Utilities.Extentions.ToMazePoint(entry);
            var srt = System.Text.Json.JsonSerializer.Serialize(walls.ToList()).Replace("[", "").Replace("]", "");
            Walls = Utilities.Extentions.StringToMazePointArray(srt );
            Hash = Utilities.UtilityFunctions.CalculateHash(string.Concat(new { walls = srt, entry, size }));
            StringRepresentation = textRepresentation;
            GenerateImage();
            ShortestPath = new []{new MazePoint(){X = -1, Y = -1}};
            LongestPath = new []{new MazePoint(){X = -1, Y = -1}};
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
            Start = Utilities.Extentions.ToMazePoint(entry);
            Walls = Utilities.Extentions.StringToMazePointArray(walls);
            Hash = Utilities.UtilityFunctions.CalculateHash(string.Concat(new { walls, entry, size }));
            StringRepresentation = textRepresentation;
            GenerateImage();
            ShortestPath = new []{new MazePoint(){X = -1, Y = -1}};
            LongestPath = new []{new MazePoint(){X = -1, Y = -1}};
        }
        catch (Exception e)
        {
            throw new Exception("Invalid maze input, " + e.Message );
        }
    }
    public async void GenerateImage()
    {
        using Image<Rgba32> image = new(800, 800);
        var options = new DrawingOptions
        {
            GraphicsOptions = new GraphicsOptions { BlendPercentage = .2F }
        };
        image.Mutate(ctx => ctx.Fill(Color.Gray));
        foreach (var wall in Walls)
        {
            image.Mutate(ctx => ctx.Fill(options, Color.Black,
                new Rectangle(wall.X * 800 / Width, (wall.Y * 800 / Height), 800 / Width, 800 / Height)));
        }

        image.Mutate(ctx => ctx.Fill(options, Color.DarkGreen,
            new Rectangle(Start.X * 800 / Width, Start.Y * 800 / this.Height, 800 / Width, 800 / Height)));
        if (PrimitiveAnalysis && !NoSolutionDefinitely)
        {
            image.Mutate(ctx => ctx.Fill(options, Color.DarkRed,
                new Rectangle(End.X * 800 / Width, End.Y * 800 / this.Height, 800 / Width, 800 / Height)));
        }

        if (MinSolved)
        {
            foreach (var path in ShortestPath!)
            {
                image.Mutate(ctx => ctx.Fill(options, Color.LightBlue,
                    new Rectangle(path.X * 800 / Width, (path.Y * 800 / Height), 800 / Width, 800 / Height)));
            }
        }

        if (MaxSolved)
        {
            foreach (var path in LongestPath!)
            {
                image.Mutate(ctx => ctx.Fill(options, Color.DarkBlue,
                    new Rectangle(path.X * 800 / Width, (path.Y * 800 / Height), 800 / Width, 800 / Height)));
            }
        }

        var memoryStream = new MemoryStream();
        await image.SaveAsync(memoryStream, PngFormat.Instance);
        Image = memoryStream.ToArray();
        await memoryStream.DisposeAsync();
    }
}