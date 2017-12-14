namespace sc.contracts
{
    public interface IEmailProvider
    {
        void NotifyUser(string emailAddress, string subject, string content);
    }
}