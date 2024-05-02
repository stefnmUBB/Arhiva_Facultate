using ISS.Biblioteca.Commons.Domain;

namespace ISS.Biblioteca.Client.DTO
{
    public class WishlistItem
    {
        public Carte Carte { get; }

        private int _NrExemplare = 0;
        public int NrExemplare 
        {
            get => _NrExemplare;
            set
            {
                _NrExemplare = value;
                if (_NrExemplare <= 0) 
                {
                    WishList.Remove(this);
                    WishList.NotifyChanged();
                }                
            }
        }

        private WishList WishList;
        public string Titlu => Carte.Titlu;
        public string Autor => Carte.Autor;

        public WishlistItem(Carte carte, int nrExemplare, WishList wishList)
        {
            Carte = carte;
            NrExemplare = nrExemplare;
            WishList = wishList;
        }     
    }
}
