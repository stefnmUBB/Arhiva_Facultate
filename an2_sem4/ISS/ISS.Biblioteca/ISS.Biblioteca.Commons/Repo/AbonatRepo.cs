using ISS.Biblioteca.Commons.Domain;
using ISS.Biblioteca.Commons.ORM;
using System.Linq;

namespace ISS.Biblioteca.Commons.Repo
{
    public class AbonatRepo : AbstractUtilizatorRepo<Abonat>, IAbonatRepo
    {
        public Abonat GetByCnp(string cnp)
        {
            using (var db = new BibliotecaContext())
            {
                return db.Abonati.Where(a => a.Cnp == cnp).FirstOrDefault();
            }
        }
    }
}
