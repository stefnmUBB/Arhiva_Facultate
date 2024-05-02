using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HelpingHands.UI
{
    public class AutoLoadActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            var layoutAttr = GetLayoutAttribute();
            if(layoutAttr!=null)
            {
                int vwid = GetIdFromName(typeof(Resource.Layout), layoutAttr.Name);                    
                SetContentView(vwid);
            }

            var controls = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(f => f.GetCustomAttribute(typeof(ControlAttribute)) != null)
                .ToList();            
            
            foreach (var control in controls)
            {                
                var val = GetType().GetMethods().Where(m=>m.Name=="FindViewById" && m.GetGenericArguments().Length==1)
                    .First()
                    .MakeGenericMethod(control.FieldType).Invoke(this,
                    new object[] { GetIdFromName(typeof(Resource.Id), control.Name) }
                    );
                control.SetValue(this, val);
            }
        }

        private int GetIdFromName(Type type, string name)
        {
            return (int)type.GetField(name, BindingFlags.Public | BindingFlags.Static).GetValue(null);
        }

        private LayoutAttribute GetLayoutAttribute()       
            => Attribute.GetCustomAttribute(GetType(), typeof(LayoutAttribute)) as LayoutAttribute;                    
    }
}