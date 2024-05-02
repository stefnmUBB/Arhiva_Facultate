using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISS.Biblioteca.Commons.Domain
{
    [Table("Retururi")]
    [Serializable]
    public class Retur
    {
        public Retur()
        {
        }

        [Key, Autoincrement]
        public int CodRetur { get; set; }

        public DateTime Data { get; set; }

        public ExemplarCarte CarteReturnata { get; set; }
        public Abonat Abonat { get; set; }
        public Bibliotecar Bibliotecar { get; set; }

    }
}