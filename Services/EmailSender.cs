using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace SchoolMaterials.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(ILogger<EmailSender> logger)
        {
            _logger = logger;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            _logger.LogWarning("\n=== СИМУЛАЦИЯ НА ИЗПРАТЕН ИМЕЙЛ ===");
            _logger.LogWarning($"ДО: {email}");
            _logger.LogWarning($"ТЕМА: {subject}");
            _logger.LogWarning($"СЪДЪРЖАНИЕ: {htmlMessage}");
            _logger.LogWarning("=====================================\n");
            
            return Task.CompletedTask;
        }
    }
}