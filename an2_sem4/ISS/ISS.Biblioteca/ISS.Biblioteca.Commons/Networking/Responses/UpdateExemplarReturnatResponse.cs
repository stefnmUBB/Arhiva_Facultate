using ISS.Biblioteca.Commons.Domain;
using System;

namespace ISS.Biblioteca.Commons.Networking.Responses
{
    [Serializable]
    public class UpdateExemplarReturnatResponse : IUpdateResponse
    {
        public Retur Retur { get; set; }

        public UpdateExemplarReturnatResponse(Retur retur)
        {
            Retur = retur;
        }
    }
}
