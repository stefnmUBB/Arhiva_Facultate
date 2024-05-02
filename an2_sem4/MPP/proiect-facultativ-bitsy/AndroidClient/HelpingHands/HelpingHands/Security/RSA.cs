using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace HelpingHands.Security
{
    internal class RSA
    {
        public static string Encrypt(string strText, string publicKey = PublicKey)
        {            
            var bytes = Encoding.UTF8.GetBytes(strText);

            using (var rsa = new RSACryptoServiceProvider(1024))
            {
                try
                {
                    rsa.FromXmlString(publicKey.ToString());
                    var encryptedData = rsa.Encrypt(bytes, RSAEncryptionPadding.OaepSHA1);
                    var base64Encrypted = Convert.ToBase64String(encryptedData);
                    return base64Encrypted;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        private const string PublicKey =
            "<RSAKeyValue><Modulus>a1B+kVIkWffxN8Q6xqRzJ08JzzlGv6CNNuPpa7foG0vGNeEMXDLQEK8OTJ1xYEz0MTmngpQx4OQ2Hb9Q1hs1Zphlc4ouEHG14gWNrX6920Z8xu9lNybiCtGkhAwUxSCjNrxz1Qr+26QbihcSMcXgC7e3/5/YhiUr4lyCQAI6f0k=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
    }
}