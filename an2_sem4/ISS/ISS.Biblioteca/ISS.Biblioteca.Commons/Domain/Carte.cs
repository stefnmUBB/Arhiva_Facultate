using SQLite.CodeFirst;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISS.Biblioteca.Commons.Domain
{
    [Table("Carti")]
    [Serializable]
    public class Carte
    {
        public Carte()
        {
        }
        [Key, Autoincrement]
        public int CodCarte { get; set; }

        public string Titlu { get; set; }

        public string Isbn { get; set; }

        public string Autor { get; set; }        

        public override string ToString()
        => $"Carte(Id={CodCarte},Title={Titlu},Autor={Autor})";

    }
}