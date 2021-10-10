using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using MimeKit;
using MimeKit.Utils;
using System.IO;
using System.Threading.Tasks;
using TimeItUpServices.Library.EmailService.Model;
using TimeItUpServices.Library.EmailService.Profile;

namespace TimeItUpServices.Library.EmailService
{
    public class EmailServiceProvider : IEmailServiceProvider
    {
        private IEmailProviderConfigurationProfile _emailConfigurationProfile;
        private IHostingEnvironment _environment;

        public EmailServiceProvider(IEmailProviderConfigurationProfile emailConfigurationProfile,
                                    IHostingEnvironment environment)
        {
            _emailConfigurationProfile = emailConfigurationProfile;
            _environment = environment;
        }

        public async Task SendEmailMessageAsync(EmailMessageContentDto emailMessageContent)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(_emailConfigurationProfile.SenderName, _emailConfigurationProfile.SmtpUsername));
            message.To.Add(new MailboxAddress(emailMessageContent.EmailRecipientFullName, emailMessageContent.EmailRecipientAddress));
            message.Subject = emailMessageContent.EmailTopic;

            var builder = new BodyBuilder();
            builder.TextBody = $@"
                               TimeItUp
                               
                               {emailMessageContent.HeaderOfEmailContent}
                               
                               {emailMessageContent.PrimaryContent}
                               
                               {emailMessageContent.URLActionText}: {emailMessageContent.AdditionalURLToAction}
                               
                               ______________________
                               TimeItUp Dev Team";

            var logo = builder.LinkedResources.Add(Path.Combine(_environment.WebRootPath, @"images\clock_time_outline.gif"));
            logo.ContentId = MimeUtils.GenerateMessageId();
            string html = File.ReadAllText(Path.Combine(_environment.WebRootPath, @"resources\EmailMessageTemplate\index.htm"));

            builder.HtmlBody = html
                                  .Replace("{BrandLogo}", logo.ContentId)
                                  .Replace("{Topic}", emailMessageContent.HeaderOfEmailContent)
                                  .Replace("{PrimaryContent}", emailMessageContent.PrimaryContent)
                                  .Replace("{SecondaryContent}", emailMessageContent.SecondaryContent);

            if (!string.IsNullOrWhiteSpace(emailMessageContent.AdditionalURLToAction))
            {
                builder.HtmlBody = builder.HtmlBody
                                                 .Replace("display: none", "display: inline-block")
                                                 .Replace("{AdditionalURLToAction}", emailMessageContent.AdditionalURLToAction)
                                                 .Replace("{URLActionText}", emailMessageContent.URLActionText);
            };

            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Connect(_emailConfigurationProfile.SmtpServer, _emailConfigurationProfile.SmtpPort, false);
                client.Authenticate(_emailConfigurationProfile.SmtpUsername, _emailConfigurationProfile.SmtpPassword);

                client.Send(message);
                client.Disconnect(true);
            }

            await Task.CompletedTask;
        }
    }
}