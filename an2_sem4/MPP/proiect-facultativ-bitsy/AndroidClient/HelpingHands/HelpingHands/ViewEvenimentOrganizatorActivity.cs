using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using HelpingHands.Adapters;
using HelpingHands.API;
using HelpingHands.Data;
using HelpingHands.UI;
using HelpingHands.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.App.DatePickerDialog;
using static Java.Util.Jar.Attributes;

namespace HelpingHands
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    [UI.Layout("activity_view_eveniment_organizator")]
    internal class ViewEvenimentOrganizatorActivity : AutoLoadActivity, IOnDateSetListener
    {
        Eveniment Eveniment;

        [Control] EditText VolEvenimentName;
        [Control] EditText VolEvenimentDescription;
        [Control] Button VolButtonUpdate;
        [Control] ListView ParticipantListView;
        [Control] Spinner InterestBox;
        [Control] EditText StartDateBox;
        [Control] EditText EndDateBox;
        EditText TargetDateBox;
        [Control] EditText LocatieBox;

        List<Participant> Participants = new List<Participant>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Console.WriteLine("HERE IN ORGANIZER");
            base.OnCreate(savedInstanceState);            

            Eveniment = JsonConvert.DeserializeObject<Eveniment>(Intent.GetStringExtra("eveniment"));                        

            VolEvenimentName.Text = Eveniment.Name;
            VolEvenimentDescription.Text = Eveniment.Description;
            StartDateBox.Text = Eveniment.StartDate.ToString("dd.MM.yyyy");
            EndDateBox.Text = Eveniment.EndDate.ToString("dd.MM.yyyy");
            LocatieBox.Text = Eveniment.Location;

            VolButtonUpdate.Click += VolButtonUpdate_Click;

            ParticipantListView.SetOnTouchListener(new ListTouchListener());

            StartDateBox.Click += DateBox_Click;
            EndDateBox.Click += DateBox_Click;
            StartDateBox.Touch += DateBox_Touch;
            EndDateBox.Touch += DateBox_Touch;            
            StartDateBox.FocusChange += DateBox_FocusChange;
            EndDateBox.FocusChange += DateBox_FocusChange;

            Task.Run(Load);
        }

        private void DateBox_FocusChange(object sender, View.FocusChangeEventArgs e)
        {
            if (!e.HasFocus) return;
            TargetDateBox = sender as EditText;
            var dateTimeNow = DateTime.Now;
            DatePickerDialog datePicker = new DatePickerDialog(this, this, dateTimeNow.Year, dateTimeNow.Month - 1, dateTimeNow.Day);
            datePicker.Show();
        }

        private void DateBox_Touch(object sender, View.TouchEventArgs e)
        {
            View view = this.CurrentFocus;
            if (view != null)
            {
                InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(view.WindowToken, 0);
            }

            var input = (sender as EditText);
            var inType = input.InputType;
            input.InputType = Android.Text.InputTypes.Null;
            input.OnTouchEvent(e.Event);
            input.InputType = inType;
            e.Handled = true;
        }       

        class ListTouchListener : Java.Lang.Object, ListView.IOnTouchListener
        {
            public bool OnTouch(View v, MotionEvent e)
            {
                var action = e.Action;
                switch (action)
                {
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
                    Console.WriteLine("Getting interests");
                    var interests = (await API.Client.GetInterests()).ToList();
                    InterestBox.Adapter = new InterestAdapter(this, interests);

                    if (Eveniment.Interests.Length > 0) 
                    {
                        var interest = interests.Where(_ => _.Name == Eveniment.Interests[0]).First();
                        InterestBox.SetSelection(interests.IndexOf(interest));
                    }

                    Console.WriteLine("Getting participants");
                    Participants = (await API.Client.GetParticipants(Eveniment.Id)).ToList();

                    Console.WriteLine("----------------------------");
                    Participants.ForEach(Console.WriteLine);
                    Console.WriteLine("----------------------------");
                    var adapter = new ParticipantAdapter(this, Participants, true);
                    adapter.RemoveButtonClick += Adapter_RemoveButtonClick;
                    ParticipantListView.Adapter = adapter;

                });
            }
            catch (Exception e)
            {
                await MessageBox.Alert(this, $"GET request failed. Connection possibly failed\n\n{e.Message}", "Error");
            }
            //GetParticipants
        }

        private async void Adapter_RemoveButtonClick(ImageView sender, int participantId)
        {   
            try
            {
                Eveniment = await Client.RemoveParticipantFromEveniment(Eveniment.Id, participantId);
                Task.Run(Load);
                await MessageBox.Alert(this, "Removed successfully");
            }
            catch(Exception ex)
            {
                await MessageBox.Alert(this, ex.Message, "Error");
            }            
        }

        private async void VolButtonUpdate_Click(object sender, EventArgs e)
        {
            var nume = VolEvenimentName.Text;
            var desc = VolEvenimentDescription.Text;
            Console.WriteLine("Dates lalalala");
            Console.WriteLine(StartDateBox.Text);
            Console.WriteLine(EndDateBox.Text);
            var startDate = DateTime.ParseExact(StartDateBox.Text, "dd.MM.yyyy", null);
            var endDate = DateTime.ParseExact(EndDateBox.Text, "dd.MM.yyyy", null);
            var loc = LocatieBox.Text;
            var interest = new Interest[] { (InterestBox.Adapter as InterestAdapter)[InterestBox.SelectedItemPosition] };
            var ev = new Eveniment
            {
                Id=Eveniment.Id,
                Name = nume,
                Description = desc,
                StartDate = startDate,
                EndDate = endDate,
                Location = loc,
                Interests = interest.Select(_ => _.Name).ToArray(),
                InitiatorId = Eveniment.InitiatorId,
                Status = Eveniment.Status
            };
            try
            {
                await Client.UpdateEveniment(ev);                
                await MessageBox.Alert(this, "Eveniment updated successfully");                                
            }
            catch(Exception ex)
            {
                await MessageBox.Alert(this, ex.Message);
            }
        }

        private void DateBox_Click(object sender, EventArgs e)
        {
            TargetDateBox = sender as EditText;
            var dateTimeNow = DateTime.Now;
            DatePickerDialog datePicker = new DatePickerDialog(this, this, dateTimeNow.Year, dateTimeNow.Month - 1, dateTimeNow.Day);
            datePicker.Show();
        }

        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            TargetDateBox.Text = new DateTime(year, month + 1, dayOfMonth).ToString("dd.MM.yyyy");
        }
    }
}