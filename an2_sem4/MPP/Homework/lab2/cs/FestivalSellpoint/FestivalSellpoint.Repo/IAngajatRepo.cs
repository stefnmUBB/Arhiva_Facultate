using FestivalSellpoint.Domain;
using System;

namespace FestivalSellpoint.Repo
{
    public interface IAngajatRepo : IRepo<int, Angajat>
    {
        Angajat findByCredentials(string username, string password);
    }
}
