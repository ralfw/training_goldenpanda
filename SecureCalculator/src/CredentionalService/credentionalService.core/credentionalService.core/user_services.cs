using System;
using System.Linq;
using System.Security.Cryptography;
using credentionalService.core.adapter;
using sc.contracts;

namespace credentionalService.core
{
    public class user_services : IUserService
    {
        #region IUserService members

        public void AddUser(String emailAddress, String rolename, Action onSuccess, Action<String> onError)
        {
            var availableUser = new db_provider().get_user_info(emailAddress);

            if (availableUser != null)
            {
                onError("User exists already !!!");
                return;
            }
            var permission = GetPermissionsFromRole(rolename);
            if (permission.Length == 0)
            {
                onError("No permissions for role !!!");
                return;
            }

            var pw_length = random.Next(10, 20);
            var newPassword = RandomString(pw_length);
            string hash = newPassword.Length.ToString();
            new db_provider().insert_user(emailAddress, hash, permission);

            try
            {
                new mailgun_adapter().NotifyUser(emailAddress, SubjectUserAdd, $"Your password is : {newPassword}");
            }
            catch (Exception ex)
            {
                onError(ex.Message);
            }

            onSuccess();
        }

        public void LogIn(String emailAddress, String passwordHash, Action<PermissionSet> onSuccess, Action<String> onError)
        {
            var availableUser = new db_provider().get_user_info(emailAddress);
            if (availableUser == null)
            {
                onError("User does not exist!");
                return;
            }

            if (availableUser.password_hash != passwordHash)
            {
                onError("Wrong password!");
                return;
            }

            if (availableUser.permissions == null)
            {
                throw new ApplicationException($"no permissions set for user {availableUser.user_email}!");
            }

            onSuccess.Invoke(availableUser.permissions);
        }

        #endregion

        #region Private methods

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private Permissions[] GetPermissionsFromRole(string rolename)
        {
            if (rolename.Equals("Student", StringComparison.InvariantCultureIgnoreCase))
                return new[] {Permissions.Add, Permissions.Subtract};

            if (rolename.Equals("Bachelor", StringComparison.InvariantCultureIgnoreCase))
                return new[] {Permissions.Add, Permissions.Subtract, Permissions.Multiply};

            if (rolename.Equals("Master", StringComparison.InvariantCultureIgnoreCase))
                return new[] {Permissions.Add, Permissions.Subtract, Permissions.Divide, Permissions.Multiply};

            return new Permissions[] {};
        }

        #endregion

        #region Fields

        private readonly string SubjectUserAdd = "Welcome new user at the holy secure calculator!!";

        #endregion
    }
}