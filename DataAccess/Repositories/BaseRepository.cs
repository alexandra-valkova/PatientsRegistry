using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public abstract class BaseRepository<TEntity> where TEntity : BaseEntity
    {
        private PatientsRegistryDbContext Context { get; set; }

        public DbSet<TEntity> DbSet { get; set; }

        public BaseRepository()
        {
            Context = new PatientsRegistryDbContext();
            DbSet = Context.Set<TEntity>();
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }

        public TEntity GetFirst(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.FirstOrDefault();
        }

        public TEntity GetByID(int id)
        {
            return DbSet.Find(id);
        }

        public void Save(TEntity entity)
        {
            if (entity.ID > 0)
            {
                Update(entity);
            }

            else
            {
                Insert(entity);
            }
        }

        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
            Context.SaveChanges();
        }

        private void Insert(TEntity entity)
        {
            DbSet.Add(entity);
            Context.SaveChanges();
        }

        private void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}