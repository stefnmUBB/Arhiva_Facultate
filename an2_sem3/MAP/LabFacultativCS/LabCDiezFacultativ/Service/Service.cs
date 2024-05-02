using LabCDiezFacultativ.Domain;
using LabCDiezFacultativ.Repo;
using System;
using System.Collections.Generic;

namespace LabCDiezFacultativ.Service
{
    public class Service<ID, E> : AbstractService<ID, E> where E : Entity<ID>
    {
        private Repository<ID, E> Repo;

        public Service(Repository<ID, E> repo)
        {
            Repo = repo;
        }

        public IdGenerator<ID> IdGenerator { get; set; } = null;

        public override E Add(E e)
        {
            // TO DO: validate e           
            e.Id = IdGenerator.Generate();
            return Repo.Add(e);            
        }

        public override IEnumerable<E> GetAll()
        {
            return Repo.GetAll();
        }

        public override E GetById(ID id)
        {
            return Repo.GetById(id);
        }

        public override E RemoveById(ID id)
        {
            return Repo.RemoveById(id);
        }
    }

    public class Service<E> : Service<long, E> where E : Entity<long> 
    {
        public Service(Repository<long, E> repo) : base(repo) { }
    }
}
