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

    public async Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
    {
        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("SendConfirmationLinkAsync {email}", email);
        }
        try
        {
            await SendEmailAsync(new SendEmailRequest()
            {
                Subject = "Confirmação de e-mail da conta",
                To = email,
                Html = $"Olá {user.UserName} Click <a href=\"{confirmationLink}\">aqui</a> para confirmar a sua conta ou cole no seu navegador link a seguir para  confirmar a sua conta <pre>{confirmationLink}</pre>"
            });
        }
        catch (Exception ex)
        {
            if (_logger.IsEnabled(LogLevel.Error))
            {
                _logger.LogError("SendConfirmationLinkAsync {ex} - {stackTracke}", email, ex.StackTrace);
            }
            throw;
        }
    }

    public async Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
    {
        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("SendPasswordResetCodeAsync {username}  {email}", user.UserName, email);
        }
        try
        {
            await SendEmailAsync(new SendEmailRequest()
            {
                Subject = "Codigo Solicitação para reset de senha",
                To = email,
                Html = $"Olá {user.UserName}, Código para resetar a sua senha <pre>{resetCode}</pre>"
            });
        }
        catch (Exception ex)
        {
            if (_logger.IsEnabled(LogLevel.Error))
            {
                _logger.LogError("SendPasswordResetCodeAsync {ex} - {stackTracke}", email, ex.StackTrace);
            }
            throw;
        }
    }

    public async Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
    {
        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("SendPasswordResetLinkAsync {email}", email);
        }
        try
        {
            await SendEmailAsync(new SendEmailRequest()
            {
                Subject = "Reset de senha",
                To = email,
                Html = $"Olá {user.UserName}, Click <a href=\"{resetLink}\">aqui</a> para resetar a sua senha ou cole no seu navegador o link a seguir: <pre>{resetLink}</pre>"
            });
        }
        catch (Exception ex)
        {
            if (_logger.IsEnabled(LogLevel.Error))
            {
                _logger.LogError("SendPasswordResetLinkAsync {ex} - {stackTracke}", email, ex.StackTrace);
            }
            throw;
        }
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