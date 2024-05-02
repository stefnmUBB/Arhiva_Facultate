using ISS.Biblioteca.Commons.Domain;
using System;

namespace ISS.Biblioteca.Commons.Networking.Responses
{
    [Serializable]
    public class GetCartiListReponse : IResponse
    {
        public (Carte Carte, int NrExemplare)[] Carti { get; set; }

        public GetCartiListReponse((Carte Carte, int NrExemplare)[] carti)
        {
            Carti = carti;
        }
    }
}
