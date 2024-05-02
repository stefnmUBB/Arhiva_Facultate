using ISS.Biblioteca.Commons.Domain;
using ISS.Biblioteca.Commons.ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace ISS.Biblioteca.Commons.Repo
{
    public class AbstractRepo<E> : IRepo<E> where E:class
    {
        public virtual E Add(E entity)
        {
            using (var db = new BibliotecaContext())
            {
                var result = db.Set<E>().Add(entity);
                db.SaveChanges();
                return result;
            }
        }        

        public virtual IEnumerable<E> GetAll()
        {
            using (var db = new BibliotecaContext())             
                return db.Set<E>();                            
        }

        public virtual E GetById(int id)
        {
            using (var db = new BibliotecaContext()) 
                return db.Set<E>().Find(id);
        }

        public virtual E Remove(E entity)
        {
            using (var db = new BibliotecaContext())
            {
                var result= db.Set<E>().Remove(entity);
                db.SaveChanges();
                return result;
            }
        }

        public virtual E Update(E entity)
        {
            using (var db = new BibliotecaContext())
            {
                db.Set<E>().Attach(entity);
                db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                db.Set<E>().AddOrUpdate(entity);
                db.SaveChanges();
            }
            return entity;
        }    
    }
}
