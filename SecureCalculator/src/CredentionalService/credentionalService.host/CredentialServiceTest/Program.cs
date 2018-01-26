using CredentialService;

namespace CredentialServiceTest
{
    class Program
    {
        #region Private methods

        static void Main(string[] args)
        {
            var server = new Server(new UserServiceMock());
            server.Run("http://localhost:80");
        }

        #endregion
    }
}