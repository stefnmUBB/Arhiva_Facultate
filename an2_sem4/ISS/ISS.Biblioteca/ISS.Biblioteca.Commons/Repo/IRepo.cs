using System;
using System.Collections.Generic;
using System.Linq;

namespace ISS.Biblioteca.Commons.Repo
{
    public interface IRepo<E>
    {
        E Add(E entity);
        E Update(E entity);
        E Remove(E entity);
        IEnumerable<E> GetAll();
        E GetById(int id);       
    }
}
