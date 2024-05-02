using ISS.Biblioteca.Commons.Domain;
using ISS.Biblioteca.Commons.ORM;
using System.Collections.Generic;
using System.Linq;

namespace ISS.Biblioteca.Commons.Repo
{
    public class CarteRepo : AbstractRepo<Carte>, ICarteRepo
    {
        public IEnumerable<Carte> Filter(string titlu, string author, string isbn)
        {
            using(var db=new BibliotecaContext())
            {
                return from carte in db.Carti
                       where carte.Titlu.Contains(titlu)
                          && carte.Autor.Contains(author)
                          && carte.Isbn.Contains(isbn)
                       select carte;
            }
        }

        public IEnumerable<(Carte Carte, int NrExemplare)> GetCartiDisponibile()
        {
            using(var db=new BibliotecaContext())
            {
                return (from carte in db.Carti
                        join exemplar in db.ExemplareCarti                        
                        on carte.CodCarte equals exemplar.Carte.CodCarte
                        into exemplareGroup                                                                        
                        select new { Carte = carte, NrExemplare = (
                            from e2 in exemplareGroup
                            where e2.Status == StatusCarte.Disponibil
                            select e2
                        ).Count() })
                .ToList()
                .Select(_ => (_.Carte, _.NrExemplare))
                .ToList();
            }            
        }

        public IEnumerable<(Carte Carte, int NrExemplare)> GetCarti()
        {
            using (var db = new BibliotecaContext())
            {
                return (from carte in db.Carti
                        join exemplar in db.ExemplareCarti
                        on carte.CodCarte equals exemplar.Carte.CodCarte
                        into exemplareGroup
                        select new { Carte = carte, NrExemplare = exemplareGroup.Count() })
                .ToList()
                .Select(_ => (_.Carte, _.NrExemplare))
                .ToList();
            }
        }
    }
}
