using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace FestivalSellpoint.Repo.Models
{
    public partial class Bilet
    {
        public long Id { get; set; }
        public long NrLocuri { get; set; }
        public long Spectacol { get; set; }
        public string NumeCumparator { get; set; }

        public virtual Spectacol SpectacolNavigation { get; set; }
    }
}
