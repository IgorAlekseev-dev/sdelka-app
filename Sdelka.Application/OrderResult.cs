namespace Sdelka.Application;

public class OrderResult
{
    public bool Success { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public string TrackNumber { get; set; } = string.Empty;
}