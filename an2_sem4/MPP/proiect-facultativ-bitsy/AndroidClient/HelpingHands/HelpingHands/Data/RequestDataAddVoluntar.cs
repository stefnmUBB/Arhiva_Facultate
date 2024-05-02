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
    internal class RequestDataAddVoluntar
    {
        [JsonProperty("idVoluntar")]
        public int VoluntarId { get; set; }
        [JsonProperty("role")]
        public string Role { get; set; }

        public RequestDataAddVoluntar(int voluntarId, string role)
        {
            VoluntarId = voluntarId;
            Role = role;
        }
    }
}