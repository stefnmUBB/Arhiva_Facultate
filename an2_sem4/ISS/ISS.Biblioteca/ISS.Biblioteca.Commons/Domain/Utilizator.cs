using SQLite.CodeFirst;
using System;

namespace ISS.Biblioteca.Commons.Domain
{
    [Serializable]
    public class Utilizator
    {
        public Utilizator()
        {
        }        
        public string Nume { get; set; }

        [Unique]
        public string Cnp { get; set; }

        public string Adresa { get; set; }

        public string Telefon { get; set; }

        public string TokenLogare { get; set; }
    }
}