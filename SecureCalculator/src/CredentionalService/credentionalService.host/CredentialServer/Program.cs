using CredentialService;
using CredentialServiceTest;

namespace CredentialServer
{
    class Program
    {
        #region Private methods

        static void Main(string[] args)
        {
            var server = new Server(new credentionalService.core.user_services());
            server.Run("http://localhost:80");
        }

        #endregion
    }
}