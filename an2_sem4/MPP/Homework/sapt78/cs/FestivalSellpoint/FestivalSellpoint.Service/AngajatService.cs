using FestivalSellpoint.Domain;
using FestivalSellpoint.Repo;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace FestivalSellpoint.Service
{
    public class AngajatService : AbstractService<int, Angajat, IAngajatRepo>, IAngajatService
    {
        public AngajatService(IAngajatRepo repo) : base(repo)
        {
        }

        public void Register(Angajat angajat)
        {
            var username = angajat.Username;
            var password = Utils.ComputeSha256Hash(angajat.Password);
            var email = angajat.Email;
            Add(new Angajat(username, password, email));
        }

        public Angajat Login(string username, string password)
            => Repo.FindByCredentials(username, Utils.ComputeSha256Hash(password));
       
    }
}
