using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using sc.contracts;

namespace CalculatorClient.Resources
{
    public class CalculatorCredentialService : ICalculatorCredentialService
    {
        public void LogIn(string emailAddress, string passwordHash, Action<PermissionSet> onSuccess, Action<string> onError)
        {
            // Fake some results here depending on the given email address
            if(!emailAddress.Contains("success"))
            {
                onError("[ServiceError] Please try again.");
                return;
            }

            onSuccess(GetFakePermissions(int.Parse(passwordHash)));
        }

        private static PermissionSet GetFakePermissions(int pwLength)
        {
            var permissions = new List<Permissions>();

            if(pwLength >= 2)
                permissions.Add(Permissions.Add);
            if(pwLength >= 3)
                permissions.Add(Permissions.Subtract);
            if(pwLength >= 4)
                permissions.Add(Permissions.Multiply);
            if(pwLength >= 5)
                permissions.Add(Permissions.Divide);

            return new PermissionSet(permissions.ToArray());
        }
    }
}