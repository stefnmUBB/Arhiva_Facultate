using ISS.Biblioteca.Commons.Domain;
using System;

namespace ISS.Biblioteca.Commons.Networking.Responses
{
    [Serializable]
    internal class ReturResponse : IResponse
    {
        public Retur Retur { get; set; }

        public ReturResponse(Retur retur)
        {
            Retur = retur;
        }
    }
}
