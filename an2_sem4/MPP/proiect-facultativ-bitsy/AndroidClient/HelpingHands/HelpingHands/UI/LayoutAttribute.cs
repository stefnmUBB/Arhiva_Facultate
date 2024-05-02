using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelpingHands.UI
{    
    public class LayoutAttribute : Attribute
    {
        public LayoutAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}