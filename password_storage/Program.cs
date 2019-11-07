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
        static void Main(string[] args)
        {
            Console.Title = "password manager @sanjaykdragon";
            Console.WriteLine("[+] password manager by @sanjaykdragon on github");
            Console.WriteLine("[!] to do: automatic DB saves, search db clientside for specific values");

            config.handle_config_detection();

            Console.WriteLine("[+] enter 1) get db, 2) save credentials, 3) clear console");

            while (true)
            {
                string input = Console.ReadLine();
                char first_char = input.Length > 0 ? input.ToLower().ToCharArray()[0] : ' ';

                db_control.handle_input(first_char);

                Console.WriteLine("[+] enter 1) get db, 2) save credentials, 3) clear console");
            }
        }
    }
}
