using ISS.Biblioteca.Commons.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ISS.Biblioteca.Commons.Networking.Responses
{
    [Serializable]
    internal class ImprumuturiByAbonatResponse : IResponse
    {
        public Imprumut[] Imprumuturi { get; }

        public ImprumuturiByAbonatResponse(IEnumerable<Imprumut> imprumuturi)
        {
            Imprumuturi = imprumuturi.ToArray();
        }
    }
}
