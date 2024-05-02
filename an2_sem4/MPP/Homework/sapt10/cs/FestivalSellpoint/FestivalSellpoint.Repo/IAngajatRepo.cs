using FestivalSellpoint.Domain;
using System;

namespace FestivalSellpoint.Repo
{
    public interface IAngajatRepo : IRepo<int, Angajat>
    {
        Angajat FindByCredentials(string username, string password);
    }
}
