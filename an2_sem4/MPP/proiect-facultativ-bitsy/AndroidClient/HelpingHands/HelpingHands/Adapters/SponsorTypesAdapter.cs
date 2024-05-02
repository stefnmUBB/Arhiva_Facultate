using Android.App;
using Android.Views;
using Android.Widget;
using HelpingHands.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelpingHands.Adapters
{
    internal class SponsorTypesAdapter : ListAdapter<SponsorType>
    {        
        public SponsorTypesAdapter(Activity context, List<SponsorType> items) : base(context, items)
        {            
        }        

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = Items[position];
            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = Context.LayoutInflater.Inflate(Resource.Layout.interest_row, null);
            view.FindViewById<TextView>(Resource.Id.NumeBox).Text = item.Name;
            return view;
        }
    }
}