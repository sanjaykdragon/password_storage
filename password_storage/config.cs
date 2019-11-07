using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace password_storage
{
    class config
    {
        private static bool config_exists()
        {
            return File.Exists("config.pwm");
        }

        private static void setup_config()
        {
            Console.WriteLine("[+] enter the base url for the server (ex: http://localhost/)");
            network.base_url = Console.ReadLine();
            Console.WriteLine("[+] enter encryption password");
            encryption.encryption_key = Console.ReadLine();

            Dictionary<string, string> config_dict = new Dictionary<string, string>
            {
                {"url", network.base_url}, {"enc_pass", encryption.encryption_key}
            };


            File.WriteAllText("config.pwm", JsonConvert.SerializeObject(config_dict)); //save above to config
        }

        public static void handle_config_detection()
        {

            if (!config_exists())
            {
                Console.WriteLine("[!] config file does not exist.");
                setup_config();
            }
            else
            {
                Console.WriteLine("[+] found config.pwm, should we use? (y)es (n)o");
                var user_response = Console.ReadKey();
                Console.WriteLine(); //start printing from the next line
                if (user_response.Key == ConsoleKey.Y)
                {
                    dynamic config = JsonConvert.DeserializeObject(File.ReadAllText("config.pwm")); //read config
                    network.base_url = config.url;
                    encryption.encryption_key = config.enc_pass;
                }
                else
                {
                    setup_config();
                }
            }
        }
    }
}
