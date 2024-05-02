using FestivalSellpoint.Domain;
using System;

namespace FestivalSellpoint.Network.DTO
{
    [Serializable]
    public class BiletDTO : EntityDTO
    {
        public string NumeCumparator { get; }
        public int NrLocuri { get; }
        public SpectacolDTO Spectacol { get; }

        public BiletDTO(string numeCumparator, int nrLocuri, Spectacol spectacol)
        {
            NumeCumparator = numeCumparator;
            NrLocuri = nrLocuri;
            Spectacol = SpectacolDTO.FromSpectacol(spectacol);
        }
    }
}
