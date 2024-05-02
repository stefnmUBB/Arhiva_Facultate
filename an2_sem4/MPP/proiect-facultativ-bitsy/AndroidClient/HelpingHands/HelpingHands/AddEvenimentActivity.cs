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
using static Android.App.DatePickerDialog;

namespace HelpingHands
{    
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    [UI.Layout("activity_add_eveniment")]
    public class AddEvenimentActivity : AutoLoadActivity, IOnDateSetListener
    {
        [Control] EditText NumeBox;
        [Control] EditText DescriereBox;
        [Control] EditText StartDateBox;
        [Control] EditText EndDateBox;
        EditText TargetDateBox;
        [Control] Spinner InterestBox;
        [Control] EditText LocatieBox;
        [Control] Button AddEventButton;

        InterestAdapter InterestAdapter;

        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            TargetDateBox.Text = new DateTime(year, month+1, dayOfMonth).ToString("dd.MM.yyyy");                               
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);                                                

            AddEventButton.Click += AddEventButton_Click;
            
            Task.Run(Load);

            StartDateBox.Text = DateTime.Now.ToString("dd.MM.yyyy");
            EndDateBox.Text = DateTime.Now.ToString("dd.MM.yyyy");

            StartDateBox.FocusChange += DateBox_FocusChange;
            EndDateBox.FocusChange += DateBox_FocusChange;
            StartDateBox.Touch += DateBox_Touch;
            EndDateBox.Touch += DateBox_Touch;

            StartDateBox.Click += DateBox_Click;
            EndDateBox.Click += DateBox_Click;
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

        private async void AddEventButton_Click(object sender, EventArgs e)
        {
            //AddEveniment
            var nume = NumeBox.Text;
            var desc = DescriereBox.Text;
            var startDate = DateTime.ParseExact(StartDateBox.Text, "dd.MM.yyyy", null);
            var endDate = DateTime.ParseExact(EndDateBox.Text, "dd.MM.yyyy", null);

            var loc = LocatieBox.Text;
            var interest = new Interest[] { (InterestBox.Adapter as InterestAdapter)[InterestBox.SelectedItemPosition] };
            var ev = new Eveniment
            {
                Name = nume,
                Description = desc,
                StartDate = startDate,
                EndDate = endDate,
                Location = loc,
                Interests = interest.Select(_ => _.Name).ToArray(),
                InitiatorId = AppSession.UserId,
                Status = "PENDING"
            };
            try
            {
                await Client.AddEveniment(ev);
                await MessageBox.Alert(this, "Evenimentul a fost adaugat cu succes");

                var intent = new Intent(this, typeof(MainVoluntarActivity));
                Toast.MakeText(Application.Context, "Ai castigat 150 de puncte Xp!", ToastLength.Short).Show();
                intent.PutExtra("tab", 1);
                StartActivity(intent);                
            }
            catch(Exception ex)
            {
                await MessageBox.Alert(this, ex.Message);
            }



            //var 
        }

        private async void Load()
        {
            InterestAdapter = new InterestAdapter(this, (await API.Client.GetInterests()).ToList());
            RunOnUiThread(() => InterestBox.Adapter = InterestAdapter);
        }

        private void DateBox_Click(object sender, EventArgs e)
        {
            TargetDateBox = sender as EditText;
            var dateTimeNow = DateTime.Now;
            DatePickerDialog datePicker = new DatePickerDialog(this, this, dateTimeNow.Year, dateTimeNow.Month-1, dateTimeNow.Day);
            datePicker.Show();
        }

        private void DateBox_FocusChange(object sender, View.FocusChangeEventArgs e)
        {
            if (!e.HasFocus) return;
            TargetDateBox = sender as EditText;
            var dateTimeNow = DateTime.Now;
            DatePickerDialog datePicker = new DatePickerDialog(this, this, dateTimeNow.Year, dateTimeNow.Month-1, dateTimeNow.Day);
            datePicker.Show();
        }
    }
}