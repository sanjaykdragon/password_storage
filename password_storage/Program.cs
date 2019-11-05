using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace password_storage
{
    class Program
    {

        //https://stackoverflow.com/questions/2883576/how-do-you-convert-epoch-time-in-c
        public static DateTime FromUnixTime(long unix_time)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unix_time);
        }

        static void Main(string[] args)
        {
            Console.Title = "password manager @sanjaykdragon";
            Console.WriteLine("[+] password manager by sanjaykdragon");
            Console.WriteLine("[!] to do: automatic DB saves, local config files, encryption");

            if (!File.Exists("config.pwm"))
            {
                Console.WriteLine("[!] config file does not exist.");
                Console.WriteLine("[+] enter the base url for the server (ex: http://localhost/)");
                network.base_url = Console.ReadLine();
                Console.WriteLine("[+] enter encryption password");
                string encryption_pass = Console.ReadLine();

                Dictionary<string, string> config_dict = new Dictionary<string, string>();

                config_dict.Add("url", network.base_url);
                config_dict.Add("enc_pass", encryption_pass);

                File.WriteAllText("config.pwm", JsonConvert.SerializeObject(config_dict));
            }
            else
            {
                Console.WriteLine("[+] found config.pwm, using");
                dynamic config = JsonConvert.DeserializeObject(File.ReadAllText("config.pwm"));
                network.base_url = config.url;
            }

            Console.WriteLine("[+] enter 1) get db, 2) save credentials");

            while (true)
            {
                string input = Console.ReadLine();
                char first_char = input.Length > 0 ? input.ToLower().ToCharArray()[0] : ' ';

                if (first_char == '1' || first_char == 'g')
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
                            string decrypted_site = item.site;
                            string decrypted_username = item.username;
                            string decrypted_password = item.password;
                            string cleaned_date = FromUnixTime((long)item.time).ToLocalTime().ToString();
                            Console.WriteLine("[+] site: {0}, username: {1}, password: {2} added on {3}", decrypted_site, decrypted_username, decrypted_password, cleaned_date);
                        }
                    }
                }
                else if (first_char == '2' || first_char == 's')
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
                Console.WriteLine("[+] enter 1) get db, 2) save credentials");
            }
        }
    }
}
