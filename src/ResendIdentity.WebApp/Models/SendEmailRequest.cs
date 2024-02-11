using System.Text.Json.Serialization;

namespace ResendIdentity.WebApp.Models;

public class SendEmailRequest
{
    [JsonPropertyName("from")]
    public string From { get; set; } = "Onboarding <onboarding@resend.com>";
    [JsonPropertyName("to")]
    public required string To { get; set; }
    [JsonPropertyName("subject")]
    public required string Subject { get; set; }
    [JsonPropertyName("html")]
    public required string Html { get; set; }
}