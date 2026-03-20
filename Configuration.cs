namespace suprimmil;

public static class Configuration
{
    public static string HttpClientName = "ServerAPI";
    public static string BackendUrl = "http://localhost:5068"; // Sua porta atual
    public static string FrontendUrl = "http://localhost:5068"; // Mesma URL (Blazor Server)
    public static string CorsPolicyName = "blazor";
}