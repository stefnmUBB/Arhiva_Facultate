using Android.App;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using HelpingHands.API;
using HelpingHands.Data;
using System.Collections.Generic;

namespace HelpingHands.Adapters
{
    internal class PostAdapter : ListAdapter<PostDTO>
    {
        public PostAdapter(Activity context, List<PostDTO> items) : base(context, items)
        {
        }

        private static string AutoEllipsis(string s, int cnt)
        {
            if (s.Length < cnt) return s;
            return s.Substring(cnt - 2) + "...";
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = Items[position];
            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = Context.LayoutInflater.Inflate(Resource.Layout.post_row, null);

            view.FindViewById<TextView>(Resource.Id.Text1).Text = AutoEllipsis(item.Username, 25);
            view.FindViewById<TextView>(Resource.Id.Text4).Text = item.Data.ToShortDateString();
            view.FindViewById<TextView>(Resource.Id.Text2).Text = AutoEllipsis(item.Descriere, 150);
            view.FindViewById<TextView>(Resource.Id.Text5).Text = AutoEllipsis(item.Eveniment.Name, 40) + " " + item.Eveniment.StartDate.ToString("dd.MM.yyyy");


            return view;
        }


    }
}