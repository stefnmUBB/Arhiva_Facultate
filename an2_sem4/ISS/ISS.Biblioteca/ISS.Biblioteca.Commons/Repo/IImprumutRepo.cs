﻿using ISS.Biblioteca.Commons.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISS.Biblioteca.Commons.Repo
{
    public interface IImprumutRepo : IRepo<Imprumut>
    {
        IEnumerable<Imprumut> GetByAbonat(Abonat a);
    }
}
