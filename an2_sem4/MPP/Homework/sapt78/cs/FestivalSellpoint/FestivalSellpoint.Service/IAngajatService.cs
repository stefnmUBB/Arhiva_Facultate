using FestivalSellpoint.Domain;

namespace FestivalSellpoint.Service
{
    public interface IAngajatService : IService<int, Angajat>
    {
        void Register(Angajat angajat);
        Angajat Login(string username, string password);
    }
}
