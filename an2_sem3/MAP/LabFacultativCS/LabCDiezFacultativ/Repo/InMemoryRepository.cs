using LabCDiezFacultativ.Domain;
using System.Collections.Generic;
using System.Linq;

namespace LabCDiezFacultativ.Repo
{
    public class InMemoryRepository<ID, E> : Repository<ID, E> where E : Entity<ID>
    {
        List<E> entities = new List<E>();
        public override E Add(E e)
        {
            entities.Add(e);
            return e;
        }

        public override IEnumerable<E> GetAll()
        {
            return entities;
        }

        public override E GetById(ID id)
        {
            return (from e in entities where e.Id.Equals(id) select e).FirstOrDefault();
        }

        public override E RemoveById(ID id)
        {
            E entity = GetById(id);
            entities.Remove(entity);
            return entity;
        }
    }
}
