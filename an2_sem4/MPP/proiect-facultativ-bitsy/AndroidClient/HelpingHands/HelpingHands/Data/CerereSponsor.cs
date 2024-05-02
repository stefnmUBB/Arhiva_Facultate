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
    internal class CerereSponsor
    {
        [JsonProperty("id")]
        public int Id { get; set; }


        [JsonProperty("volId")]
        public int VolId { get; set; }

        [JsonProperty("cifFirma")]
        public string CifFirma { get; set; }


        [JsonProperty("telefon")]
        public String Telefon { get; set; }


        [JsonProperty("adresa")]
        public String Adresa { get; set; }


        [JsonProperty("numeFirma")]
        public string NumeFirma { get; set; }


        [JsonProperty("sponsorType")]
        public string SponsorType { get; set; }

    }
}