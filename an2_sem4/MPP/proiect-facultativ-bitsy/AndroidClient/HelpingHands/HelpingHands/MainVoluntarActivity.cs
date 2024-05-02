using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.SE.Omapi;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.BottomNavigation;
using HelpingHands.Adapters;
using HelpingHands.API;
using HelpingHands.Data;
using HelpingHands.UI;
using HelpingHands.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace HelpingHands
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    [UI.Layout("activity_main_voluntar")]
    public partial class MainVoluntarActivity : AutoLoadAppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        [Control] GridLayout HomeView;
        [Control] GridLayout DashboardView;
        [Control] ListView EvenimenteListView;
        [Control] ListView OrganizatorEvenimenteListView;
        [Control] ListView VoluntarEvenimenteListView;
        [Control] Button EvNextButton;
        [Control] Button EvPrevButton;
        [Control] TextView EvPageTextView;
        [Control] Button CreateEvButton;
        [Control] GridLayout PostView;
        [Control] ListView PostariListView;
        [Control] GridLayout MeniuHelpView;
        [Control] WebView PgWeb;
        [Control] Spinner InterestBox;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);                        

            EvPrevButton.Click += EvPrevButton_Click;
            EvNextButton.Click += EvNextButton_Click;
            
            CreateEvButton.Click += CreateEvButton_Click;
            EvenimenteListView.ItemClick += EvenimenteListView_ItemClick;
            OrganizatorEvenimenteListView.ItemClick += EvenimenteListView_ItemClick;            
            VoluntarEvenimenteListView.ItemClick += EvenimenteListView_ItemClick;
            InterestBox.ItemSelected += InterestBox_ItemSelected;
            EventLoader = async (p) => await Client.GetEvenimentePaged(p - 1);

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);


            HomeView.Visibility = ViewStates.Visible;
            DashboardView.Visibility = ViewStates.Gone;
            PostView.Visibility = ViewStates.Gone;
            MeniuHelpView.Visibility = ViewStates.Gone;
            Task.Run(LoadHome);
            Task.Run(LoadDashboard);
            Task.Run(LoadPosts);

            int tab = Intent.GetIntExtra("tab", 0);
            navigation.SelectedItemId = tab;

            OnCreate_AccountPage();
        }

        Func<int, Task<Eveniment[]>> EventLoader;

        private async void InterestBox_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var interest = (InterestBox.Adapter as InterestAdapter)[e.Position];
            if(interest.Id=="0")
            {
                EventLoader = async (p) => await Client.GetEvenimentePaged(p - 1);
                Evenimente = (await Client.GetEvenimentePaged(int.Parse(EvPageTextView.Text) - 1)).ToList();
                RunOnUiThread(() =>
                {
                    EvenimenteListView.Adapter = new EvenimentAdapter(this, Evenimente);
                });
            }
            else
            {
                EvPageTextView.Text = "1";
                Console.WriteLine("INTEREST = "+interest.Id+" "+interest.Name);
                EventLoader = async (p) => await Client.GetEvenimenteByInterestPaged(interest.Name, p - 1);
                Evenimente = (await Client.GetEvenimenteByInterestPaged(interest.Name, int.Parse(EvPageTextView.Text) - 1)).ToList();
                RunOnUiThread(() =>
                {
                    EvenimenteListView.Adapter = new EvenimentAdapter(this, Evenimente);
                });
            }
        }

        List<PostDTO> PostariEv = new List<PostDTO>();
        private async void LoadPosts()
        {
            try
            {
                PostariEv = (await Client.GetPostOfVoluntar()).ToList();
                RunOnUiThread(() =>
                {
                    PostariListView.Adapter = new PostAdapter(this, PostariEv);
                });
            }
            catch (Exception e)
            {
                await MessageBox.Alert(this, "GET request failed. Connection possibly failed", "Error");
            }
        }

        bool EventSelected = false;

        
        protected override void OnResume()
        {
            base.OnResume();            
            Console.WriteLine("RESULTHERE");
            Task.Run(LoadDashboard);          
        }        

        private async void EvenimenteListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {                        
            Console.WriteLine("EventSelected");
            if (EventSelected) return;
            EventSelected = true;     
            
            Console.WriteLine($"Clicked index {e.Position}");                                    
            var ev = ((sender as ListView).Adapter as EvenimentAdapter)[e.Position];
            Console.WriteLine($"{ev}");

            Intent intent;
            try
            {
                if ((await Client.GetParticipants(ev.Id)).Any(_ => _.IsOrganizer && _.Voluntar.Id == AppSession.UserId))
                {
                    Console.WriteLine("Organizer cu vanilie");
                    intent = new Intent(this, typeof(ViewEvenimentOrganizatorActivity));
                }
                else
                {
                    Console.WriteLine("Voluntar cu vanilie");
                    intent = new Intent(this, typeof(ViewEvenimentVoluntarActivity));
                }
            }
            catch(Exception ex) { await MessageBox.Alert(this, ex.Message); return; }
            EventSelected = false;

            intent.PutExtra("eveniment", JsonConvert.SerializeObject(ev));
            StartActivity(intent);
            
        }        

        void CreateEvButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(AddEvenimentActivity));
            StartActivity(intent);
        }

        async void EvPrevButton_Click(object sender, EventArgs e)
        {
            try
            {
                var pageN = int.Parse(EvPageTextView.Text);
                if (pageN == 1) return;
                pageN--;
                //Evenimente = (await API.Client.GetEvenimentePaged(pageN - 1)).ToList();
                Evenimente = (await EventLoader(pageN)).ToList();                
                RunOnUiThread(() =>
                {
                    EvPageTextView.Text = pageN.ToString();
                    EvenimenteListView.Adapter = new EvenimentAdapter(this, Evenimente);
                });
            }
            catch (Exception ex)
            {
                await MessageBox.Alert(this, "GET request failed. Connection possibly failed", "Error");
            }
        }

        async void EvNextButton_Click(object sender, EventArgs e)
        {
            try
            {
                var pageN = int.Parse(EvPageTextView.Text);
                pageN++;
                //var ev = (await API.Client.GetEvenimentePaged(pageN - 1)).ToList();
                var ev = (await EventLoader(pageN)).ToList();
                if (ev.Count == 0) { pageN--; return; }
                Evenimente = ev;
                RunOnUiThread(() =>
                {
                    EvPageTextView.Text = pageN.ToString();

                    EvenimenteListView.Adapter = new EvenimentAdapter(this, Evenimente);
                });
            }
            catch (Exception ex)
            {
                await MessageBox.Alert(this, "GET request failed. Connection possibly failed", "Error");
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        List<Eveniment> Evenimente = new List<Eveniment>();
        List<Eveniment> ManagedEvenimente = new List<Eveniment>();

        async void LoadHome()
        {
            try
            {
                Evenimente = (await EventLoader(int.Parse(EvPageTextView.Text))).ToList();
                //Evenimente = (await API.Client.GetEvenimentePaged(int.Parse(EvPageTextView.Text) - 1)).ToList();
                RunOnUiThread(() =>
                {
                    EvenimenteListView.Adapter = new EvenimentAdapter(this, Evenimente);
                });
                var InterestAdapter = new InterestAdapter(this, (await API.Client.GetInterests()).Prepend(
                    new Interest { Id = "0", Name = "(None)" }).ToList());
                RunOnUiThread(() => InterestBox.Adapter = InterestAdapter);
            }
            catch(Exception e)
            {
                await MessageBox.Alert(this, "GET request failed. Connection possibly failed", "Error");
            }
        }

        List<Eveniment> ParticipantEvenimente = new List<Eveniment>();

        async void LoadDashboard()
        {
            try
            {
                ManagedEvenimente = (await Client.GetEvenimenteByOrganizerId(AppSession.UserId)).ToList();
                ParticipantEvenimente = (await Client.GetEvenimenteByVoluntar(AppSession.UserId)).ToList();
                RunOnUiThread(() =>
                {
                    OrganizatorEvenimenteListView.Adapter = new EvenimentAdapter(this, ManagedEvenimente);
                    VoluntarEvenimenteListView.Adapter = new EvenimentAdapter(this, ParticipantEvenimente);
                });
            }
            catch (Exception e)
            {
                await MessageBox.Alert(this, "GET request failed. Connection possibly failed", "Error");
            }

        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    HomeView.Visibility = ViewStates.Visible;
                    DashboardView.Visibility = ViewStates.Gone;
                    ProfileView.Visibility = ViewStates.Gone;
                    PostView.Visibility = ViewStates.Gone;
                    MeniuHelpView.Visibility = ViewStates.Gone;
                    LoadHome();
                    return true;
                case Resource.Id.navigation_dashboard:
                    HomeView.Visibility = ViewStates.Gone;
                    DashboardView.Visibility = ViewStates.Visible;
                    ProfileView.Visibility = ViewStates.Gone;     
                    PostView.Visibility= ViewStates.Gone;
                    MeniuHelpView.Visibility = ViewStates.Gone;
                    LoadDashboard();
                    return true;
                case Resource.Id.navigation_user:
                    HomeView.Visibility = ViewStates.Gone;
                    DashboardView.Visibility = ViewStates.Gone;
                    ProfileView.Visibility = ViewStates.Visible;
                    PostView.Visibility = ViewStates.Gone;
                    MeniuHelpView.Visibility = ViewStates.Gone;
                    LoadProfile();
                    return true;                                
                case Resource.Id.navigation_posts:
                    HomeView.Visibility = ViewStates.Gone;
                    DashboardView.Visibility = ViewStates.Gone;
                    ProfileView.Visibility = ViewStates.Gone;
                    PostView.Visibility = ViewStates.Visible;
                    MeniuHelpView.Visibility = ViewStates.Gone;
                    LoadPosts();
                    return true;
                case Resource.Id.help_FAQ:
                    HomeView.Visibility = ViewStates.Gone;
                    DashboardView.Visibility = ViewStates.Gone;
                    ProfileView.Visibility = ViewStates.Gone;
                    PostView.Visibility = ViewStates.Gone;
                    MeniuHelpView.Visibility = ViewStates.Visible;
                    LoadHelp();
                    return true;
            }
            return false;
        }

        private void LoadHelp()
        {                 
            PgWeb.Settings.JavaScriptEnabled = true;
            PgWeb.LoadUrl("file:///android_asset/meniuHelp.html");
        }

    }
}

