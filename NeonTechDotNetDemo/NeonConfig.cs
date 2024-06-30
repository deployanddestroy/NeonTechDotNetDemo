namespace NeonTechDotNetDemo;

/// <summary>
/// Standard Postgres DB configuration object
/// </summary>
internal class NeonConfig
{
    public string Server { get; set; } = default!;
    public string Database { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}
