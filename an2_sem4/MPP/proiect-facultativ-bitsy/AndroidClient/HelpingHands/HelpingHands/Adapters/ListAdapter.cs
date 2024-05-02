using Android.App;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace HelpingHands.Adapters
{
    internal abstract class ListAdapter<T> : BaseAdapter<T>
    {
        protected List<T> Items { get; }
        protected Activity Context { get; }
        public ListAdapter(Activity context, List<T> items) : base()
        {
            this.Context = context;
            this.Items = items;
        }

        public override T this[int position] => Items[position];

        public override int Count => Items.Count;

        public override long GetItemId(int position) => position;        
    }
}