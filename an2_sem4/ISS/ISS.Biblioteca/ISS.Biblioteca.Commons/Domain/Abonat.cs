using SQLite.CodeFirst;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISS.Biblioteca.Commons.Domain
{
    [Table("Abonati")]
    [Serializable]
    public class Abonat : Utilizator {
        public Abonat() { }

        [Key, Autoincrement]
        public int CodAbonat { get; set; }
    }
}