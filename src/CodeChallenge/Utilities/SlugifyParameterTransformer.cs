using System.Text.RegularExpressions;

namespace CodeChallenge.Utilities;

public class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string TransformOutbound(object? value)
    {
        return (value == null ? null : Regex.Replace(value.ToString() ?? throw new NullReferenceException("No string conversion is available"), "([a-z])([A-Z])", "$1-$2").ToLower()) ?? throw new InvalidOperationException("Cannot transform null value");
    }
}