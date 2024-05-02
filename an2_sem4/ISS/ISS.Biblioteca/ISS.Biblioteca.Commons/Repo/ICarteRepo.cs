using ISS.Biblioteca.Commons.Domain;
using System.Collections.Generic;

namespace ISS.Biblioteca.Commons.Repo
{
    public interface ICarteRepo : IRepo<Carte>
    {
        IEnumerable<Carte> Filter(string name, string author, string isbn);

        IEnumerable<(Carte Carte, int NrExemplare)> GetCartiDisponibile();        
        IEnumerable<(Carte Carte, int NrExemplare)> GetCarti();        

    }
}
