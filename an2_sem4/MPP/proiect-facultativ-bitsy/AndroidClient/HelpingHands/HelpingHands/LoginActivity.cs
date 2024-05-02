using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.SE.Omapi;
using Android.Views;
using Android.Widget;
using FFImageLoading;
using FFImageLoading.Work;
using HelpingHands.API;
using HelpingHands.UI;
using HelpingHands.Utils;
using System;

namespace HelpingHands
{
    [Activity(Label = "HelpingHands", Theme = "@style/AppTheme", MainLauncher = true)]
    [UI.Layout("activity_login")]
    public class LoginActivity : AutoLoadActivity
    {
        [Control] EditText PasswordBox;
        [Control] EditText UsernameBox;
        [Control] Button LoginButton;
        [Control] TextView RegisterLink;
        [Control] ImageView logo;        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);            
            
            RegisterLink.PaintFlags |= PaintFlags.UnderlineText;

            LoginButton.Click += LoginButton_Clicked;
            RegisterLink.Click += RegisterLink_Click;        
            //ShapeableImageView
        }

        private void RegisterLink_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(RegisterVoluntarActivity));
            StartActivity(intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public async void LoginButton_Clicked(object sender, EventArgs e)
        {                       
            var username = UsernameBox.Text;
            var password = PasswordBox.Text;

            try
            {
                var userInfo = await API.Client.Login(username, password);                
                AppSession.UserData = userInfo;

                if (userInfo.Type == "Voluntar")
                {
                    await MessageBox.Alert(this, $"Bine ai venit, {userInfo.User.Nume} {userInfo.User.Prenume}", "Login cu succes");
                    Intent intent = new Intent(this, typeof(MainVoluntarActivity));
                    StartActivity(intent);
                }
                else
                {
                    await MessageBox.Alert(this, "Admins cannot connect through app");
                }
            }
            catch(Exception ex)
            {
                await MessageBox.Alert(this, ex.Message, "Login failed");
                return;
            }                        
        }
    }
}