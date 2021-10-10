using System.Threading.Tasks;
using TimeItUpServices.Library.EmailService.Model;

namespace TimeItUpServices.Library.EmailService
{
    public interface IEmailServiceProvider
    {
        Task SendEmailMessageAsync(EmailMessageContentDto emailContent);
    }
}
