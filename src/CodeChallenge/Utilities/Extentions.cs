namespace CodeChallenge.Utilities;

public static class Extentions
{
    public  static string Tojson(this object obj)
    {
        return System.Text.Json.JsonSerializer.Serialize(obj);
    }
    
}