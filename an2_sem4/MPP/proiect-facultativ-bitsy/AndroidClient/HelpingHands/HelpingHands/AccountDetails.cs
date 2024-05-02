using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Provider;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using FFImageLoading.Work;
using FFImageLoading;
using HelpingHands.Adapters;
using HelpingHands.API;
using HelpingHands.UI;
using HelpingHands.Utils;
using Java.IO;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Android.Content.ClipData;

namespace HelpingHands
{
    public partial class MainVoluntarActivity
    {
        [Control] GridLayout ProfileView;

        [Control] TextView AccPrenume;
        [Control] TextView AccNume;
        [Control] TextView AccEmail;
        [Control] TextView AccXpPct;
        [Control] TextView AccInterests;
        [Control] Button AccApplyForSponsorButton;
        [Control] Button AccLogoutButton;
        [Control] ImageButton SelectPPButton;
        [Control] ImageView ProfilePicView;

        protected void OnCreate_AccountPage()
        {            
            AccApplyForSponsorButton.Click += ApplyForSponsorButton_Click;
            AccLogoutButton.Click += AccLogoutButton_Click;
            SelectPPButton.Click += SelectPPButton_Click;
            
            ProfileView.Visibility = ViewStates.Gone;            

            Task.Run(LoadProfile);
        }

        private static readonly int RESULT_LOAD_IMAGE = 1;

        private async void SelectPPButton_Click(object sender, EventArgs e)
        {
            Intent photoPickerIntent = new Intent(Intent.ActionPick);
            photoPickerIntent.SetType("image/*");            
            StartActivityForResult(photoPickerIntent, RESULT_LOAD_IMAGE);
        }

        protected override async void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                if (requestCode == RESULT_LOAD_IMAGE)
                {
                    var selectedImage = data.Data;
                    try
                    {                        
                        var bitmap = MediaStore.Images.Media.GetBitmap(ContentResolver, selectedImage);

                        var sz = Math.Min(bitmap.Width, bitmap.Height);

                        bitmap = Bitmap.CreateScaledBitmap(Bitmap.CreateBitmap(bitmap, (bitmap.Width - sz) / 2, (bitmap.Height - sz) / 2, sz, sz), 64, 64, true);                                                
                        //ProfilePicView.SetImageBitmap(bitmap);

                        using (var ms = new MemoryStream())
                        {
                            bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                            byte[] bytes = ms.ToArray();
                            bitmap.Recycle();

                            await Client.SetProfilePic(AppSession.UserId, bytes);

                            await MessageBox.Alert(this, "Profile picture updated successfully", "Info");
                            await Task.Run(LoadProfile);
                        }
                        

                        //carImage.setImageBitmap(bitmap);
                    }
                    catch (Exception e)
                    {
                        await MessageBox.Alert(this, e.Message, "Error");                        
                    }                    
                }
            }
        }

        private async void AccLogoutButton_Click(object sender, EventArgs e)
        {
            try
            {
                await Client.Logout();                
                StartActivity(new Intent(this, typeof(LoginActivity)));
            }
            catch(Exception ex)
            {
                await MessageBox.Alert(this, ex.Message, "Logout failed");
            }
        }

        async void LoadProfile()
        {
            try
            {
                RunOnUiThread(async () =>
                {
                    var vol = await Client.GetVoluntar(AppSession.UserId);

                    System.Console.WriteLine("Account Profile Details");
                    AccNume.Text =AppSession.UserData.User.Nume;
                    AccEmail.Text = AppSession.UserData.User.Email;
                    AccPrenume.Text = AppSession.UserData.User.Prenume;
                    AppSession.UserData.User.XpPoints = vol.XpPoints;
                    AccXpPct.Text = vol.XpPoints.ToString();
                    var interestsList = await API.Client.GetVoluntarInterests(AppSession.UserId);
                    var interestsText = string.Join(", ", interestsList.Select(x => x.Name));
                    AccInterests.Text = interestsText;

                    ImageService.Instance.InvalidateMemoryCache();
                    ImageService.Instance.LoadUrl(ClientBase.BaseAddress + $"/helpinghands/users/{AppSession.UserId}/pp")
                        .WithPriority(LoadingPriority.High)
                        .Retry(3, 200)
                        .LoadingPlaceholder("default_pp.jpg", ImageSource.CompiledResource)
                        .ErrorPlaceholder("default_pp.jpg", ImageSource.CompiledResource)
                        .WithCache(FFImageLoading.Cache.CacheType.Memory)
                        .Into(ProfilePicView);
                });
            }
            catch (Exception e)
            {
                await MessageBox.Alert(this, $"FAILED ACCOUNT PAGE\n\n{e.Message}", "Error");
            }
        }

        private void ApplyForSponsorButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ApplySponsorshipActivity));
            StartActivity(intent);
        }
    }
}