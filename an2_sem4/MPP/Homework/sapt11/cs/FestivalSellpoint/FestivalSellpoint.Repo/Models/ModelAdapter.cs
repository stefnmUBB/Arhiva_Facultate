using FestivalSellpoint.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalSellpoint.Repo.Models
{
    public static class ModelAdapter
    {
        public static Domain.Angajat ToDomainEntity(this Angajat angajat)
            => new Domain.Angajat(angajat.Username, angajat.Password, angajat.Email) { Id = (int)angajat.Id };

        public static Domain.Spectacol ToDomainEntity(this Spectacol spectacol)
            => new Domain.Spectacol(
                spectacol.Artist,
                DateTime.ParseExact(Encoding.ASCII.GetString(spectacol.Data), "yyyy-MM-dd HH:mm:ss", null),
                spectacol.Locatie,
                (int)spectacol.NrLocuriDisponibile,
                (int)spectacol.NrLocuriVandute
                )
            { Id = (int)spectacol.Id };

        public static Domain.Bilet ToDomainEntity(this Bilet bilet)
            => new Domain.Bilet(bilet.NumeCumparator, (int)bilet.NrLocuri, bilet.SpectacolNavigation.ToDomainEntity())
            { Id = (int)bilet.Id };

        public static Angajat ToModelEntity(this Domain.Angajat angajat)
            => new Angajat
            {
                Id = angajat.Id,
                Username = angajat.Username,
                Password = angajat.Password,
                Email = angajat.Email
            };

        public static Spectacol ToModelEntity(this Domain.Spectacol spectacol)
            => new Spectacol
            {
                Id = spectacol.Id,
                Artist = spectacol.Artist,
                Data = Encoding.ASCII.GetBytes(spectacol.Data.ToString("yyyy-MM-dd HH:mm:ss")),
                Locatie = spectacol.Locatie,
                NrLocuriDisponibile = spectacol.NrLocuriDisponibile,
                NrLocuriVandute = spectacol.NrLocuriVandute
            };

        public static Bilet ToModelEntity(this Domain.Bilet bilet)
            => new Bilet
            {
                Id = bilet.Id,
                NumeCumparator = bilet.NumeCumparator,
                Spectacol = bilet.Spectacol.Id,
                SpectacolNavigation = bilet.Spectacol.ToModelEntity()
            };

        public static DateTime DateTimeFromBytes(this byte[] bytes)
            => DateTime.ParseExact(Encoding.ASCII.GetString(bytes), "yyyy-MM-dd HH:mm:ss", null);

    }
}
