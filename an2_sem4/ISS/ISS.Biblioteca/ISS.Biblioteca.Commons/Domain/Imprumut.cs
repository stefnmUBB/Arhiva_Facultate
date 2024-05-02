using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISS.Biblioteca.Commons.Domain
{
    [Table("Imprumuturi")]
    [Serializable]
    public class Imprumut
    {
        public Imprumut()
        {
        }

        [Key, Autoincrement]
        public int CodImprumut { get; set; }
        public DateTime Data { get; set; }
        public StatusImprumut Status { get; set; }
        public Abonat Abonat { get; set; }
        public ExemplarCarte CarteImprumutata { get; set; }
    }
}