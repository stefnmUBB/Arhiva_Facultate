using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HelpingHands.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelpingHands.API
{
    internal static class AppSession
    {
        public static UserSession UserData { get; set; }
        public static int UserId => UserData.User.Id;
    }
}