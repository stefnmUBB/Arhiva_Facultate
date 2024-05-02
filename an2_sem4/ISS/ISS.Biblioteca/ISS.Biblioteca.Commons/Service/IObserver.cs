using ISS.Biblioteca.Commons.Domain;

namespace ISS.Biblioteca.Commons.Service
{
    public interface IClientObserver
    {
        void OnImprumut(Imprumut imprumut);
        void OnRetur(Retur retur);
    }
}
