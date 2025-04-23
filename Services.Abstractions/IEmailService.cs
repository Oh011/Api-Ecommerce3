namespace Services.Abstractions
{
    public interface IEmailService
    {


        public Task SendEmail(string To, string subject, string Body);
    }
}
