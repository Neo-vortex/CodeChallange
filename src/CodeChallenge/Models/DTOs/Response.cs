using CodeChallenge.Models.Types;

namespace CodeChallenge.Models;

public class Response
{
    public ResponseStatus Status { get; set; }
    public string? Message { get; set; }
}