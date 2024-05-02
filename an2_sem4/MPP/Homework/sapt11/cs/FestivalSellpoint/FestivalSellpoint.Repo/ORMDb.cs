using FestivalSellpoint.Domain;
using FestivalSellpoint.Repo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using log4net;

namespace FestivalSellpoint.Repo
{
    public class ORMDb<ID, E, M> : IRepo<ID, E> 
        where E : Entity<ID>
        where M : class
    {
        private static readonly ILog log = LogManager.GetLogger("ORMDb");

        Type DbContextType { get; }
        string TableName { get; }

        public ORMDb(Type dbContextType, string tableName)
        {
            DbContextType = dbContextType;
            TableName = tableName;
        }

        protected DbContext GetContext() => Activator.CreateInstance(DbContextType) as DbContext;

        protected DbSet<M> GetDbSet(DbContext context) =>
            context.GetType().GetProperty(TableName).GetValue(context) as DbSet<M>;

        public virtual void Add(E e)
        {
            using (var db = GetContext())
            {
                GetDbSet(db).Add(ModelAdapter.ToModelEntity((dynamic)e));
                db.SaveChanges();                
            }
        }

        protected IEnumerable<E> GetAllProcessed(Func<DbSet<M>, DbSet<M>> processor)
        {
            log.Info($"{typeof(E)} : Getting All Processed ");
            using (var db = GetContext())
            {
                foreach (dynamic item in processor(GetDbSet(db))) 
                {
                    yield return ModelAdapter.ToDomainEntity(item);
                }                
            }
        }

        protected IEnumerable<E> GetAllProcessed(Func<DbSet<M>, IEnumerable<M>> processor)
        {
            log.Info($"{typeof(E)} : Getting All Processed ");
            using (var db = GetContext())
            {
                foreach (dynamic item in processor(GetDbSet(db)))
                {
                    yield return ModelAdapter.ToDomainEntity(item);
                }
            }
        }

        protected IEnumerable<E> GetAllProcessed(Func<DbSet<M>, IQueryable<M>> processor)
        {
            log.Info($"{typeof(E)} : Getting All Processed Q ");
            using (var db = GetContext())
            {
                foreach (dynamic item in processor(GetDbSet(db))) 
                {
                    yield return ModelAdapter.ToDomainEntity(item);
                }
            }
        }

        public virtual IEnumerable<E> GetAll()
        {
            log.Info($"{typeof(E)} : Getting All");
            return GetAllProcessed(_ => _);
        }

        public E GetById(ID id)
        {
            log.Info($"{typeof(E)} : Getting By id {id}");
            using (var db = GetContext())
            {
                object _id = (id is int) ? Convert.ChangeType(id, typeof(long)) : id;                

                return ModelAdapter.ToDomainEntity((dynamic)GetDbSet(db).Find(_id));
            }
        }

        public void Remove(ID id)
        {
            log.Info($"{typeof(E)} : Removing by {id}");
            using (var db = GetContext())
            {
                var set = GetDbSet(db);
                object _id = (id is int) ? Convert.ChangeType(id, typeof(long)) : id;
                M item = set.Find(_id);
                if (item == null) return;
                set.Remove(item);
                db.SaveChanges();
            }
        }

        public void Update(E e)
        {
            log.Info($"{typeof(E)} : Updating {e}");
            var IdProp = typeof(M).GetProperty("Id");
            using (var db = GetContext())
            {
                var set = GetDbSet(db);
                object _id = (e.Id is int) ? Convert.ChangeType(e.Id, typeof(long)) : e.Id;
                var entity = ModelAdapter.ToModelEntity((dynamic)e);

                if (set.AsNoTracking().AsEnumerable()
                    .Where(x => _id.Equals(IdProp.GetValue(x))).FirstOrDefault() == null)
                    return;

                set.Update(entity);
                db.SaveChanges();
            }
        } 
    }
}
