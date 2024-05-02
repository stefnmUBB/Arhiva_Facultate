using ISS.Biblioteca.Commons.Domain;
using ISS.Biblioteca.Commons.ORM;
using System.Collections.Generic;
using System.Linq;

namespace ISS.Biblioteca.Commons.Repo
{
    public class ImprumutRepo : AbstractRepo<Imprumut>, IImprumutRepo
    {
        public override Imprumut Add(Imprumut imprumut)
        {
            using (var db = new BibliotecaContext())
            {
                db.Abonati.Attach(imprumut.Abonat);
                db.ExemplareCarti.Attach(imprumut.CarteImprumutata);                
                var result = db.Imprumuturi.Add(imprumut);
                db.SaveChanges();
                return result;
            }
        }

        public IEnumerable<Imprumut> GetByAbonat(Abonat a)
        {
            using(var db=new BibliotecaContext())
            {
                return (from imprumut in db.Imprumuturi.Include("Abonat").Include("CarteImprumutata.Carte")
                        where imprumut.Abonat.Cnp == a.Cnp && imprumut.Status == StatusImprumut.Activ
                        select imprumut).ToArray();
            }
        }
    }
}
