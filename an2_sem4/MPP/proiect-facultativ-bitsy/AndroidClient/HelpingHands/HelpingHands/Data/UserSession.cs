using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelpingHands.Data
{
    internal class UserSession
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("utilizator")]
        public User User { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}