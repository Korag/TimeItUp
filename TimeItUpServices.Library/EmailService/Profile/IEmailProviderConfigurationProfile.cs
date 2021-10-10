namespace TimeItUpServices.Library.EmailService.Profile
{
    public interface IEmailProviderConfigurationProfile
    {
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }

        public string SmtpServer { get; }
        public int SmtpPort { get; }

        public string SenderName { get; set; }
    }
}