using Android.App;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using HelpingHands.Data;
using System.Collections.Generic;

namespace HelpingHands.Adapters
{
    internal class EvenimentAdapter : ListAdapter<Eveniment>
    {
        public EvenimentAdapter(Activity context, List<Eveniment> items) : base(context, items)
        {
        }        

        private static string AutoEllipsis(string s, int cnt)
        {
            if (s.Length < cnt) return s;
            return s.Substring(0,cnt-2) + "...";
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = Items[position];
            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = Context.LayoutInflater.Inflate(Resource.Layout.eveniment_row, null);

            view.FindViewById<TextView>(Resource.Id.Text1).Text = item.Name; //AutoEllipsis(item.Name, 25);
            view.FindViewById<TextView>(Resource.Id.Text2).Text = item.Description; //AutoEllipsis(item.Description, 50);   
            view.FindViewById<TextView>(Resource.Id.Text3).Text = item.StartDate.ToShortDateString();
            view.FindViewById<TextView>(Resource.Id.Text4).Text = item.EndDate.ToShortDateString();            

            //var imageBitmap = GetImageBitmapFromUrl(item.ImageURI);
            //view.FindViewById<ImageView>(Resource.Id.Image).SetImageBitmap(imageBitmap);
            return view;
        }
    }
}