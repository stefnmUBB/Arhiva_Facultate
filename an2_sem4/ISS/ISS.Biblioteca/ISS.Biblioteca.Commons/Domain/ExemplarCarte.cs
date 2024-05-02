using SQLite.CodeFirst;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISS.Biblioteca.Commons.Domain
{
    [Table("ExemplareCarti")]
    [Serializable]
    public class ExemplarCarte
    {

        public ExemplarCarte()
        {
        }
        [Key, Autoincrement]
        public int CodExemplar { get; set; }

        public Carte Carte { get; set; }        
        public StatusCarte Status { get; set; }

        // not used
        // those exists to map the many-to-many relationships
        public ICollection<Imprumut> Imprumuturi { get; set; }
        public ICollection<Retur> Retururi { get; set; }

        public override string ToString() =>
            $"Exemplar(Id={CodExemplar},Carte={Carte.CodCarte},Status={Status})";      
    }
}