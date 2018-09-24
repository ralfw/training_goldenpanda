using System;
using System.Net;
using RestSharp;
using sc.contracts;

namespace CalculatorCredentialService
{
    public class CalculatorCredentialService : ICalculatorCredentialService
    {
        #region ICalculatorCredentialService members

        public void LogIn(string emailAddress, string passwordHash, Action<PermissionSet> onSuccess, Action<string> onError)
        {
            var client = new RestClient("http://wagocredentials.cloud.dropstack.run");
            WebProxy myproxy = new WebProxy("10.1.4.17", 8080);
            myproxy.UseDefaultCredentials = true;
            myproxy.BypassProxyOnLocal = false;
            client.Proxy = myproxy;

            var myJsonDeserializer = new NewtonsoftJsonDeserializer();
            client.AddHandler("application/json", myJsonDeserializer);
            client.AddHandler("text/json", myJsonDeserializer);

            var request = new RestRequest("/api/v1/users/role", Method.GET);
            request.AddParameter("emailAddress", emailAddress);
            request.AddParameter("passwordHash", passwordHash);

            var loginResult = client.Execute<LoginResult>(request);
            if (loginResult.ErrorException == null && string.IsNullOrEmpty(loginResult.Data.Error))
                onSuccess?.Invoke(loginResult.Data.Permissions);
            else if (loginResult.ErrorException != null)
                onError?.Invoke(loginResult.ErrorException.ToString());
            else
                onError?.Invoke(loginResult.Data.Error);
        }

        #endregion
    }
}