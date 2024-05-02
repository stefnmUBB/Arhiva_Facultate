using ISS.Biblioteca.Commons.Domain;

namespace ISS.Biblioteca.Client.DTO
{
    public class ExemplarItem
    {
        public Imprumut Imprumut { get; }
        public ExemplarCarte Exemplar => Imprumut.CarteImprumutata;
        public int Cod => Exemplar.CodExemplar;
        public string Titlu => Exemplar.Carte.Titlu;
        public string Autor => Exemplar.Carte.Autor;

        public ExemplarItem(Imprumut imprumut)
        {
            Imprumut = imprumut;
        }
    }
}
