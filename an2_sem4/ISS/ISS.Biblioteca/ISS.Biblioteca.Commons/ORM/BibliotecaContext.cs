using ISS.Biblioteca.Commons.Domain;
using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ISS.Biblioteca.Commons.ORM
{
    public class BibliotecaContext:  DbContext
    {        
        public BibliotecaContext() : base(Utils.Config.Connection, false)
        {            
            Database.SetInitializer<BibliotecaContext>(null);
            Configuration.ProxyCreationEnabled = false;            
            ((IObjectContextAdapter)this).ObjectContext.ContextOptions.LazyLoadingEnabled = false;
            ((IObjectContextAdapter)this).ObjectContext.ContextOptions.ProxyCreationEnabled = false;
        }

        public DbSet<Abonat> Abonati { get; set; }        
        public DbSet<AdministratorIT> Administratori { get; set; }        
        public DbSet<Bibliotecar> Bibliotecari { get; set; }        
        public DbSet<Carte> Carti { get; set; }        
        public DbSet<ExemplarCarte> ExemplareCarti { get; set; }        
        public DbSet<Imprumut> Imprumuturi { get; set; }        
        public DbSet<Retur> Retururi { get; set; }                    

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {           
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<BibliotecaContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);                
        }        
    }
}
