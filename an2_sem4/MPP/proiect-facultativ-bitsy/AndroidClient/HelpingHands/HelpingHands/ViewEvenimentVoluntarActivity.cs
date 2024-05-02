using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HelpingHands.Adapters;
using HelpingHands.API;
using HelpingHands.Data;
using HelpingHands.UI;
using HelpingHands.Utils;
using Java.Interop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpingHands
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    [UI.Layout("activity_view_eveniment_voluntar")]
    public class ViewEvenimentVoluntarActivity : AutoLoadActivity
    {
        Eveniment Eveniment;

        [Control] TextView VolEvenimentName;
        [Control] TextView VolEvenimentDescription;
        [Control] TextView VolEvenimentDate;
        [Control] TextView VolEvenimentInterese;
        [Control] Button VolButtonAddParticipant;
        [Control] ListView ParticipantListView;
        [Control] TextView VolEvenimentLocatie;

        List<Participant> Participants = new List<Participant>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Console.WriteLine("HERE IN VOLUNTAR");
            base.OnCreate(savedInstanceState);

            Eveniment = JsonConvert.DeserializeObject<Eveniment>(Intent.GetStringExtra("eveniment"));            

            VolEvenimentName.Text = Eveniment.Name;
            VolEvenimentDescription.Text = Eveniment.Description;
            VolEvenimentDate.Text = Eveniment.StartDate.ToString("dd.MM.yyyy") + " - " + Eveniment.EndDate.ToString("dd.MM.yyyy");
            VolEvenimentInterese.Text = string.Join(", ", Eveniment.Interests);
            VolEvenimentLocatie.Text = Eveniment.Location; 

            VolButtonAddParticipant.Click += VolButtonAddParticipant_Click;

            ParticipantListView.SetOnTouchListener(new ListTouchListener());

            Task.Run(Load);
        }

        class ListTouchListener : Java.Lang.Object, ListView.IOnTouchListener
        {
            public bool OnTouch(View v, MotionEvent e)
            {
                var action = e.Action;
                switch (action) {
                    case MotionEventActions.Down:
                        // Disallow ScrollView to intercept touch events.
                        v.Parent.RequestDisallowInterceptTouchEvent(true);
                        break;

                    case MotionEventActions.Up:
                        // Allow ScrollView to intercept touch events.
                        v.Parent.RequestDisallowInterceptTouchEvent(false);
                        break;
                }
                // Handle ListView touch events.
                v.OnTouchEvent(e);
                return true;
            }
        }               

        async void Load()
        {
            try
            {                
                RunOnUiThread(async () =>
                {
                    Participants.Clear();
                    Console.WriteLine("Getting participants");
                    Participants = (await API.Client.GetParticipants(Eveniment.Id)).ToList();

                    Console.WriteLine("----------------------------");
                    Participants.ForEach(Console.WriteLine);
                    Console.WriteLine("----------------------------");
                    ParticipantListView.Adapter = new ParticipantAdapter(this, Participants);           
                    
                    if(Participants.Any(p=>p.Voluntar.Id == AppSession.UserId))
                    {
                        VolButtonAddParticipant.Text = "Ma retrag";
                    }
                    else
                    {
                        VolButtonAddParticipant.Text = "Ma inscriu";
                    }
                });
            }
            catch (Exception e)
            {
                await MessageBox.Alert(this, $"GET request failed. Connection possibly failed\n\n{e.Message}", "Error");
            }
            //GetParticipants
        }

        private async void VolButtonAddParticipant_Click(object sender, EventArgs e)
        {
            int evId = Eveniment.Id;
            int volId = AppSession.UserId;
       
            if (VolButtonAddParticipant.Text == "Ma retrag")
            {
                try
                {
                    var participant = Participants.Where(p => p.Voluntar.Id == AppSession.UserId).FirstOrDefault();
                    if(participant==null)
                    {
                        throw new ArgumentNullException("Participant is null");
                    }
                    _ = await Client.RemoveParticipantFromEveniment(evId, participant.Id);
                    await MessageBox.Alert(this, "You are no logner part of this eveniment!");
                    Toast.MakeText(Application.Context, "Ai pierdut 100 de puncte Xp!", ToastLength.Short).Show();
                    Load();
                }
                catch (Exception ex)
                {
                    await MessageBox.Alert(this, ex.Message, "Retragere failed");
                    return;
                }                
            }
            else
            {
                try
                {
                    var inscriere = await Client.AddVoluntarToEveniment(evId, volId, "volunteer");
                    await MessageBox.Alert(this, "You were added to eveniment!");
                    Toast.MakeText(Application.Context, "Ai castigat 100 de puncte Xp!", ToastLength.Short).Show();
                    Load();
                }
                catch (Exception ex)
                {
                    await MessageBox.Alert(this, ex.Message, "Aderation failed");
                    return;
                }
            }
        }
    }
}