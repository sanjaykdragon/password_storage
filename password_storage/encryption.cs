using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace password_storage
{
    class encryption
    {
        //this is a wrapper class for EasyEncryption (nuget package) - an external library
        public static string encryption_key = "NO_ENCRYPTION_KEY"; //this is replaced by config file - placeholder value
        public static string encrypt_text(string plain_text)
        {
            return EasyEncryption.AesThenHmac.SimpleEncryptWithPassword(plain_text, encryption_key);
        }

        public static string decrypt_text(string encrypted_text)
        {
            return EasyEncryption.AesThenHmac.SimpleDecryptWithPassword(encrypted_text, encryption_key);
        }
    }
}
