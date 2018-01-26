using CredentialService;
using CredentialServiceTest;

namespace CredentialServer
{
    class Program
    {
        #region Private methods

        static void Main(string[] args)
        {
            //TODO use real Service 
            var server = new Server(new UserServiceMock());
            server.Run("http://localhost:8080");
        }

        #endregion
    }
}