using ISS.Biblioteca.Commons.Domain;
using ISS.Biblioteca.Commons.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISS.Biblioteca.Commons.Repo
{
    public class AbstractUtilizatorRepo<E> : AbstractRepo<E>, IUtilizatorRepo<E>
        where E:Utilizator
    {
        public E FindByCredentials(string cnp, string token)
        {
            using (var db = new BibliotecaContext())
                return (from admin in db.Set<E>()
                        where admin.Cnp == cnp && admin.TokenLogare == token
                        select admin).FirstOrDefault();
        }
    }
}
