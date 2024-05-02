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
    internal class Participant
    {
        [JsonProperty("voluntar")]
        public Voluntar Voluntar { get; set; }
        [JsonProperty("organizer")]
        public bool IsOrganizer { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }

        public override string ToString()
        {
            return $"Id={Id}, Voluntar={Voluntar}, Org={IsOrganizer}";
        }
    }
}