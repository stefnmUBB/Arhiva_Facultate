using ISS.Biblioteca.Commons.Domain;
using ISS.Biblioteca.Commons.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISS.Biblioteca.Commons.Repo
{
    public class ExemplarCarteRepo : AbstractRepo<ExemplarCarte>, IExemplarCarteRepo
    {
        public override ExemplarCarte Add(ExemplarCarte exemplar)
        {
            using (var db = new BibliotecaContext())
            {
                db.Set<Carte>().Attach(exemplar.Carte);
                var result = db.Set<ExemplarCarte>().Add(exemplar);
                db.SaveChanges();
                return result;
            }            
        }

        public ExemplarCarte GetDisponibleExemplarOf(Carte carte)
        {
            using (var db = new BibliotecaContext())
                return (from exemplar in db.ExemplareCarti.Include("Carte")
                        where exemplar.Carte.CodCarte == carte.CodCarte && exemplar.Status == StatusCarte.Disponibil
                        select exemplar).FirstOrDefault();
        }
    }
}
