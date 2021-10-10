namespace TimeItUpServices.Library.EmailService.Model
{
    public class EmailMessageContentDto
    {
        public string EmailRecipientAddress { get; set; }
        public string EmailRecipientFullName { get; set; }
       
        public string EmailTopic { get; set; }
        public string HeaderOfEmailContent { get; set; }
        public string PrimaryContent { get; set; }
        public string SecondaryContent { get; set; }

        public string AdditionalURLToAction { get; set; }
        public string URLActionText { get; set; }

        public EmailMessageContentDto(string emailRecipientAddress, 
                                      string emailRecipientFullName, 
                                      string emailClassifierKey, 
                                      string additionalUrlToAction = null)
        {
            this.EmailRecipientAddress = emailRecipientAddress;
            this.EmailRecipientFullName = emailRecipientFullName;

            this.EmailTopic = EmailClassifierDictionary.EmailTopic[emailClassifierKey];
            this.HeaderOfEmailContent = EmailClassifierDictionary.HeaderOfEmailContent[emailClassifierKey];
            this.PrimaryContent = EmailClassifierDictionary.PrimaryContent[emailClassifierKey];
            this.SecondaryContent = EmailClassifierDictionary.SecondaryContent[emailClassifierKey];

            this.AdditionalURLToAction = additionalUrlToAction;
            this.URLActionText = EmailClassifierDictionary.URLActionText[emailClassifierKey];
        }
    }
}
