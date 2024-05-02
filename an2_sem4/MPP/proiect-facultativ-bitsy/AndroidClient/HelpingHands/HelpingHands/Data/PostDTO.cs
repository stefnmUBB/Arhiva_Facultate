using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace HelpingHands.Data
{
    internal class PostDTO  
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("userId")]
        public int IdUser { get; set; }
        [JsonProperty("username")]
        public String Username{ get; set; }

        [JsonProperty("evenimentDTO")]
        public Eveniment Eveniment { get; set; }
        [JsonProperty("descriere")]
        public String Descriere { get; set; }

        [JsonProperty("data")]
        public DateTime Data { get; set; }
    }
}