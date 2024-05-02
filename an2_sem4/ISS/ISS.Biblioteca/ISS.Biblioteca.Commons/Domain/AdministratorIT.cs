using SQLite.CodeFirst;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISS.Biblioteca.Commons.Domain
{
    [Table("AdministratoriIT")]
    [Serializable]
    public class AdministratorIT : Utilizator
    {
        public AdministratorIT()
        {
        }
        [Key, Autoincrement]
        public int CodAdministrator { get; set; }
        
    }
}