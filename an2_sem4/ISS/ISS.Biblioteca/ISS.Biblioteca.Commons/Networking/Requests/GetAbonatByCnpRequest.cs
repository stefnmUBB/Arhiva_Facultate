using System;

namespace ISS.Biblioteca.Commons.Networking.Requests
{
    [Serializable]
    internal class GetAbonatByCnpRequest : IRequest
    {
        public string Cnp { get; set; }

        public GetAbonatByCnpRequest(string cnp)
        {
            Cnp = cnp;
        }
    }
}
