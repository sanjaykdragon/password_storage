﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace password_storage
{
    class network
    {
        public static string base_url = "http://localhost/"; //this is changed by config file
        public static dynamic send_credentials(string username, string password, string site)
        {
            string response_string = string.Empty;
            using (WebClient web = new WebClient())
            {
                web.Proxy = null;
                NameValueCollection values = new NameValueCollection
                {
                    ["option"] = "save",
                    ["username"] = encryption.encrypt_text(username),
                    ["password"] = encryption.encrypt_text(password),
                    ["site"] = encryption.encrypt_text(site)
                };

                byte[] response_array = web.UploadValues(base_url + "test.php", values);
                response_string = Encoding.Default.GetString(response_array);
            }

            dynamic result_data = JsonConvert.DeserializeObject(response_string);
            return result_data;
        }

        public static dynamic get_database()
        {
            string response_string = string.Empty;
            using (WebClient web = new WebClient())
            {
                web.Proxy = null;
                NameValueCollection values = new NameValueCollection
                {
                    ["option"] = "get_list"
                };

                byte[] response_array = web.UploadValues(base_url + "test.php", values);
                response_string = Encoding.Default.GetString(response_array);
            }

            dynamic result_data = JsonConvert.DeserializeObject(response_string);
            return result_data;
        }
    }
}
