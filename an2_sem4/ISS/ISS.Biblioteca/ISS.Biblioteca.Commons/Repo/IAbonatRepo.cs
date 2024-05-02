using ISS.Biblioteca.Commons.Domain;

namespace ISS.Biblioteca.Commons.Repo
{
    public interface IAbonatRepo : IUtilizatorRepo<Abonat>
    {
        Abonat GetByCnp(string cnp);
    }
}
