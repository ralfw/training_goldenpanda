using System;
using RestSharp;
using RestSharp.Authenticators;
using sc.contracts;

namespace credentionalService.core.adapter
{
    public class mailgun_adapter : IEmailProvider
    {
        #region IEmailProvider members

        // You can see a record of this email in your logs: https://mailgun.com/app/logs .


        public void NotifyUser(String emailAddress, String subject, String content)
        {
            var message_result = send_message_over_mailgun(emailAddress, subject, content);
            if (!message_result.IsSuccessful)
                throw new Exception("email sending failed");
        }

        #endregion

        #region Private methods

        private static RestResponse send_message_over_mailgun(string mailTo, string subject, string content)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");

            client.Authenticator = new HttpBasicAuthenticator("api", API_KEY);

            RestRequest request = new RestRequest();
            request.AddParameter("domain", "sandbox8464b70ecfa24fe79a1e61a507edb948.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Secure Calculator Service <admin@scs.net>");
            //request.AddParameter("from", "Mailgun Sandbox <postmaster@sandbox8464b70ecfa24fe79a1e61a507edb948.mailgun.org>");

            request.AddParameter("to", mailTo);
            request.AddParameter("subject", subject);
            request.AddParameter("text", content);
            request.Method = Method.POST;
            return (RestResponse)client.Execute(request);
        }

        #endregion

        #region Fields

        private static readonly string API_KEY = "key-627ca8b55e3df1f5fcbb6c69f716b591";

        #endregion
    }
}