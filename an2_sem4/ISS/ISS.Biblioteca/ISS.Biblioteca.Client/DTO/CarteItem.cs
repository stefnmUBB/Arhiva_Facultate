using ISS.Biblioteca.Commons.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISS.Biblioteca.Client.DTO
{
    public class CarteItem : INotifyPropertyChanged
    {
        public Carte Carte { get; set; }
        public int Cod => Carte.CodCarte;
        public string Titlu => Carte.Titlu;
        public string Autor => Carte.Autor;
        public string Isbn => Carte.Isbn;
        private int _NrExemplare = 0;
        public int NrExemplare 
        { 
            get=>_NrExemplare; 
            set
            {
                _NrExemplare = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NrExemplare"));
            }
        }

        public CarteItem(Carte carte, int nrExemplare)
        {
            Carte = carte;                        
            NrExemplare = nrExemplare;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
