using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelpingHands.Data
{
    internal class User
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("nume")]
        public string Nume { get; set; }
        [JsonProperty("prenume")]
        public string Prenume { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("interests")]
        public ISet<Interest> Interests { get; set; }
        [JsonProperty("xpPoints")]
        public int XpPoints { get; set; }
        [JsonProperty("sponsor")]
        public bool IsSponsor { get; set; }
        [JsonProperty("activeSponsor")]
        public bool IsActiveSponsor { get; set; }        

    }
}