using FestivalSellpoint.Domain;

namespace FestivalSellpoint.Service.Observer
{
    public interface IObserver
    {
        void UpdateSpectacol(Spectacol s);
    }
}
