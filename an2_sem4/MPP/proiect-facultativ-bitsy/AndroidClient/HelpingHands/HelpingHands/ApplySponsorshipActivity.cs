using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Google.Android.Material.BottomNavigation;
using HelpingHands.Adapters;
using HelpingHands.API;
using HelpingHands.Data;
using HelpingHands.UI;
using HelpingHands.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Icu.Text.Transliterator;


namespace HelpingHands
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    [UI.Layout("activity_apply_sponsorship")]
    public class ApplySponsorshipActivity : AutoLoadActivity
    {
        [Control] EditText CifFirmaBox;
        [Control] EditText TelefonBox;
        [Control] EditText AdresaBox;
        [Control] EditText NumeBox;
        [Control] Spinner TypesBox;
        [Control] Button ApplySponsorshipButton;

        SponsorTypesAdapter SponsorTypesAdapter;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);           
            ApplySponsorshipButton.Click += ApplySponsorshipButton_Click;
            Task.Run(Load);
        }

        private async void ApplySponsorshipButton_Click(object sender, EventArgs e)
        {
            var volId = AppSession.UserId;
            var cif = CifFirmaBox.Text;
            var telefon = TelefonBox.Text;
            var adresa = AdresaBox.Text;
            var nume = NumeBox.Text;
            var sponsorType = (TypesBox.Adapter as SponsorTypesAdapter)[TypesBox.SelectedItemPosition];

            var cerere = new CerereSponsor
            {
                VolId = volId,
                CifFirma = cif,
                Telefon = telefon,
                Adresa = adresa,
                NumeFirma = nume,
                SponsorType = sponsorType.Name
            };

            try
            {
                await Client.ApplySponsorship(cerere);
                await MessageBox.Alert(this, "Cererea a fost trimisa cu succes");

                Toast.MakeText(Application.Context, "Ai castigat 50 de puncte Xp!", ToastLength.Short).Show();
                var intent = new Intent(this, typeof(MainVoluntarActivity));
                intent.PutExtra("tab", 1);
                StartActivity(intent);
            }
            catch (Exception ex)
            {
                await MessageBox.Alert(this, ex.Message);
            }

        }


        private async void Load()
        {
            SponsorTypesAdapter = new SponsorTypesAdapter(this, (await API.Client.GetSponsorTypes()).ToList());
            RunOnUiThread(() => TypesBox.Adapter = SponsorTypesAdapter);
        }
    }
}