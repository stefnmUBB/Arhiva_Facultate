using LabCDiezFacultativ.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabCDiezFacultativ.Repo
{
    public abstract class Repository<ID, E> where E : Entity<ID>
    {
        public abstract IEnumerable<E> GetAll();        
        public abstract E Add(E e);
        public abstract E RemoveById(ID id);
        public abstract E GetById(ID id);
    }
}
