using System;
using System.IO;
using System.Linq;
using credentionalService.core.data;
using sc.contracts;

namespace credentionalService.core.adapter
{
    class db_provider
    {
        private static string DbFile = "userfile.txt";

        public void insert_user(string email, string passwordHash,Permissions[] initialPermission)
        {
            user_info newInfo = new user_info()
            {
                password_hash = passwordHash,
                user_email = email,
                permissions = new PermissionSet(initialPermission)
            };

            var userLine = create_line_from_user(newInfo);
            File.AppendAllLines(DbFile,new[] {userLine});
        }


        public user_info get_user_info(string email)
        {
            var users = get_all_users();
            return users.FirstOrDefault(o => o.user_email.Equals(email));           
        }


        private user_info[] get_all_users()
        {
            var lines = File.ReadAllLines(DbFile);
            return lines.Select(get_user_from_line).ToArray();        
        }

        private string create_line_from_user(user_info user)
        {
            var persimmions = user.permissions.Permissions.Select(o => o.ToString());
            var permissionsString = persimmions.Aggregate((i, j) => i + "," + j);

            return $"{user.user_email},{user.password_hash},{permissionsString}";
        }

        private user_info get_user_from_line(string line)
        {
            var parts = line.Split(',');
            var target = new user_info();
            fill_permissions_from_line(target, parts);
            fill_usercredentials_from_line(target, parts.Skip(2).ToArray());
            return target;
            
        }


        private void fill_permissions_from_line(user_info target, string[] lineParts)
        {
            target.user_email = lineParts[0];
            target.password_hash = lineParts[1];
        }

        private void fill_usercredentials_from_line(user_info target, string[] lineParts)
        {            
            var permissions = lineParts.Select(o => (Permissions)Enum.Parse(typeof(Permissions), o)).ToArray();
            target.permissions = new PermissionSet(permissions);
        }
    }
}
