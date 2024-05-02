using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HelpingHands.API;
using HelpingHands.Data;
using HelpingHands.UI;
using HelpingHands.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace HelpingHands
{
    [Activity(Label = "HelpingHands Register", Theme = "@style/AppTheme")]
    [UI.Layout("activity_register_voluntar")]
    public class RegisterVoluntarActivity : AutoLoadActivity
    {
        [Control] EditText UsernameBox;
        [Control] EditText Pass1Box;
        [Control] EditText Pass2Box;
        [Control] EditText EmailBox;
        [Control] EditText NumeBox;
        [Control] EditText PrenumeBox;
        [Control] Button RegisterButton;

        public static string Encrypt(string strText, string key)
        {
            var publicKey = "<RSAKeyValue><Modulus>a1B+kVIkWffxN8Q6xqRzJ08JzzlGv6CNNuPpa7foG0vGNeEMXDLQEK8OTJ1xYEz0MTmngpQx4OQ2Hb9Q1hs1Zphlc4ouEHG14gWNrX6920Z8xu9lNybiCtGkhAwUxSCjNrxz1Qr+26QbihcSMcXgC7e3/5/YhiUr4lyCQAI6f0k=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

            var testData = Encoding.UTF8.GetBytes(strText);

            using (var rsa = new RSACryptoServiceProvider(1024))
            {
                try
                {
                    // client encrypting data with public key issued by server                                        
                    rsa.FromXmlString(publicKey.ToString());

                    var encryptedData = rsa.Encrypt(testData, true);

                    var base64Encrypted = Convert.ToBase64String(encryptedData);

                    return base64Encrypted;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RegisterButton.Click += RegisterButton_Click;
        }

        private async void RegisterButton_Click(object sender, EventArgs e)
        {
            try
            {
                var username = UsernameBox.Text;
                var pass1 = Pass1Box.Text;
                var pass2 = Pass2Box.Text;
                var email = EmailBox.Text;
                var nume = NumeBox.Text;
                var prenume = PrenumeBox.Text;

                if (pass1 != pass2)
                {
                    await MessageBox.Alert(this, "Password confirmation failed", "Error");
                    return;
                }

                var user = await Client.Register(username, pass1, email, nume, prenume);
                await MessageBox.Alert(this, "Register successful");
                StartActivity(new Intent(this, typeof(LoginActivity)));                
            }
            catch (Exception ex)
            {
                await MessageBox.Alert(this, ex.Message, "Error");
            }
        }
    }
}