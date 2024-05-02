using SQLite.CodeFirst;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISS.Biblioteca.Commons.Domain
{
    [Table("Bibliotecari")]
    [Serializable]
    public class Bibliotecar : Utilizator {
        public Bibliotecar() {
        }
        public int Oficiu { get; set; }
        [Key, Autoincrement]
        public int CodBibliotecar { get; set; }
    }
}