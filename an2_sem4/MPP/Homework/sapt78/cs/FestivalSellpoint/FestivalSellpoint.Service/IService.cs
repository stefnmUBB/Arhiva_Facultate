using FestivalSellpoint.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalSellpoint.Service
{
    public interface IService<ID, E> where E : Entity<ID>
    {
        void Add(E e);
        void Update(E e);
        void Remove(ID id);
        E GetById(ID id);
        IEnumerable<E> GetAll();
    }
}
