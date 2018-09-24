using System;
using System.Net;
using RestSharp;
using sc.contracts;

namespace AdminCredentialService
{
    public class AdminCredentialService : IAdminCredentialService
    {
        #region IAdminCredentialService members

        public void CreateUser(string emailAddress, string rolename, Action onSuccess, Action<string> onError)
        {
            var client = new RestClient("http://wagocredentials.cloud.dropstack.run");
            WebProxy myproxy = new WebProxy("10.1.4.17", 8080);
            myproxy.UseDefaultCredentials = true;
            myproxy.BypassProxyOnLocal = false;
            client.Proxy = myproxy;

            var request = new RestRequest("/api/v1/users", Method.GET);
            request.AddParameter("emailAddress", emailAddress);
            request.AddParameter("roleName", rolename);

            var result = client.Execute(request);
            if (result.ErrorException != null)
            {
                onError?.Invoke(result.ErrorException.ToString());
                return;
            }

            if (!string.IsNullOrEmpty(result.Content))
            {
                onError?.Invoke(result.Content);
                return;
            }

            onSuccess?.Invoke();
        }

        #endregion
    }
}