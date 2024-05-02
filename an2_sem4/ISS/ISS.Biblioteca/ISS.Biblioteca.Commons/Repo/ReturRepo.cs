using ISS.Biblioteca.Commons.Domain;
using ISS.Biblioteca.Commons.ORM;

namespace ISS.Biblioteca.Commons.Repo
{
    public class ReturRepo : AbstractRepo<Retur>, IReturRepo
    {
        public override Retur Add(Retur retur)
        {
            using (var db = new BibliotecaContext())
            {
                db.Abonati.Attach(retur.Abonat);
                db.ExemplareCarti.Attach(retur.CarteReturnata);
                db.Bibliotecari.Attach(retur.Bibliotecar);                
                var result = db.Retururi.Add(retur);
                db.SaveChanges();
                return result;
            }
        }
    }
}
