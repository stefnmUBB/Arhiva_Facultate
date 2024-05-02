using FestivalSellpoint.Domain;
using System.Collections.Generic;

namespace FestivalSellpoint.Repo
{
    public interface IRepo<ID, E> where E : Entity<ID>
    {
        void Add(E e);
        void Update(E e);
        void Remove(ID id);
        E GetById(ID id);
        IEnumerable<E> GetAll();
    }
}
