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
    internal class EventParams
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("eveniment")]
        public Eveniment Eveniment { get; set; }

    }
}