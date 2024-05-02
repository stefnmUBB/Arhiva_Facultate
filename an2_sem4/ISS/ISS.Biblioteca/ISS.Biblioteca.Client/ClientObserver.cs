using ISS.Biblioteca.Commons.Domain;
using ISS.Biblioteca.Commons.Service;
using System.Diagnostics;

namespace ISS.Biblioteca.Client
{
    public class ClientObserver : IClientObserver
    {
        public delegate void OnImprumutUpdate(Imprumut imprumut);
        public event OnImprumutUpdate ImprumutUpdate;

        public delegate void OnReturUpdate(Retur retur);
        public event OnReturUpdate ReturUpdate;

        public void OnImprumut(Imprumut imprumut)
        {
            //Debug.WriteLine("Carte Imprumutata " + imprumut.CarteImprumutata);
            ImprumutUpdate?.Invoke(imprumut);
        }

        public void OnRetur(Retur retur)
        {
            //Debug.WriteLine("Carte Returnata " + retur.CarteReturnata);
            ReturUpdate?.Invoke(retur);
        }
    }
}
