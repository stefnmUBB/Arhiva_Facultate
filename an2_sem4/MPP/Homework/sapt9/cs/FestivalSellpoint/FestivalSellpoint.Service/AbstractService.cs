using FestivalSellpoint.Domain;
using FestivalSellpoint.Repo;
using System.Collections.Generic;

namespace FestivalSellpoint.Service
{
    public class AbstractService<ID, E, R> : IService<ID, E> 
        where E : Entity<ID>
        where R : IRepo<ID, E>
    {
        protected R Repo { get; }

        public AbstractService(R repo)
        {
            Repo = repo;
        }

        public void Add(E e) => Repo.Add(e);        

        public IEnumerable<E> GetAll() => Repo.GetAll();

        public E GetById(ID id) => Repo.GetById(id);

        public void Remove(ID id) => Repo.Remove(id);

        public void Update(E e) => Repo.Update(e);        
    }
}
