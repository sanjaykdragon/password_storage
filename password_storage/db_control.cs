using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace password_storage
{
    class db_control
    {

        //https://stackoverflow.com/questions/2883576/how-do-you-convert-epoch-time-in-c
        private static DateTime FromUnixTime(long unix_time)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unix_time);
        }
        public static void get_database()
        {
            //get db
            var response = network.get_database();
            if (response.status == "failed")
            {
                Console.WriteLine("[!] error occurred: {0}", response.detail);
            }
            else
            {
                foreach (var item in response.values)
                {
                    string decrypted_site = encryption.decrypt_text((string)item.site);
                    string decrypted_username = encryption.decrypt_text((string)item.username);
                    string decrypted_password = encryption.decrypt_text((string)item.password);
                    string cleaned_date = FromUnixTime((long)item.time).ToLocalTime().ToString();
                    Console.WriteLine("[+] site: {0}, username: {1}, password: {2} added on {3}", decrypted_site, decrypted_username, decrypted_password, cleaned_date);
                }
            }
        }

        public static void save_credentials()
        {
            //save creds
            Console.WriteLine("[+] enter the name of the site: ");
            string site = Console.ReadLine();
            Console.WriteLine("[+] enter the username for this site: ");
            string username = Console.ReadLine();
            Console.WriteLine("[+] enter the password for this site: ");
            string password = Console.ReadLine();

            var response = network.send_credentials(username, password, site);
            if (response.status == "failed")
            {
                Console.WriteLine("[!] error occurred: {0}", response.detail);
            }
            else
            {
                Console.WriteLine("[+] added to db successfully!");
            }
        }
    }
}
