using ISS.Biblioteca.Commons.Domain;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace ISS.Biblioteca.Client.DTO
{
    public class WishList : INotifyCollectionChanged, IEnumerable<WishlistItem>
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;        

        public List<WishlistItem> Items = new List<WishlistItem>();

        public void AddExemplar(Carte carte)
        {
            var c = Items.Where(_ => _.Carte.CodCarte == carte.CodCarte).FirstOrDefault();
            if (c == null)
            {
                Items.Add(new WishlistItem(carte, 1, this));
                NotifyChanged();
            }
            else
            {
                c.NrExemplare++;
                NotifyChanged();
            }
        }

        public void Remove(WishlistItem item)
        {
            Items.Remove(item);
            NotifyChanged();
        }

        public void NotifyChanged()
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }


        public void Clear()
        {
            Items.Clear();
            NotifyChanged();
        }
        public IEnumerator<WishlistItem> GetEnumerator() => Items.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int BooksCount => Items.Select(_ => _.NrExemplare).Sum();
    }
}
