using LabCDiezFacultativ.Domain;
using LabCDiezFacultativ.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabCDiezFacultativ.Service
{
    public abstract class AbstractService<ID,E> where E : Entity<ID>
    {
        public abstract E Add(E e);
        public abstract E GetById(ID id);
        public abstract E RemoveById(ID id);
        public abstract IEnumerable<E> GetAll();

        public delegate bool FilterPredicate(E e);
        public IEnumerable<E> Filter(FilterPredicate pred) // Func<E, bool> 
        {
            return GetAll().Where(e => pred(e));
        }
    }
}
