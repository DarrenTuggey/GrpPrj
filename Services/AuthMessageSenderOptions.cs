namespace GroupProject.Services
{
    // Service for getting/setting the SendGrid user and API key
    public class AuthMessageSenderOptions
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
    }
}
