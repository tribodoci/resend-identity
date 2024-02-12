using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using ResendIdentity.WebApp.Data;
using ResendIdentity.WebApp.Models;

namespace ResendIndentity.WebApp.Services;

public class ResendEmailSenderService(
    ILogger<ResendEmailSenderService> logger,
    IHttpClientFactory httpClientFactory,
    IConfiguration configuration) : IEmailSender<ApplicationUser>
{
    private readonly ILogger<ResendEmailSenderService> _logger = logger;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly string _apiKey = configuration.GetSection("Resend")["ApiKey"] ?? throw new Exception("Resend ApiKey Não configurado");
    private readonly string _apiUrl = configuration.GetSection("Resend")["ApiUrl"] ?? throw new Exception("Resend ApiUrl Não configurado");

    public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
    {
        throw new NotImplementedException();
    }

    public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
    {
        throw new NotImplementedException();
    }

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
    {
        throw new NotImplementedException();
    }

    private async Task SendEmailAsync(SendEmailRequest emailRequest)
    {
        try
        {
            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, _apiUrl);
            request.Headers.Add("Authorization", $"Bearer {_apiKey}");
            var content = new StringContent(
                JsonSerializer.Serialize(emailRequest),
                null,
                "application/json"
            );
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception)
        {
            throw;
        }
    }
}