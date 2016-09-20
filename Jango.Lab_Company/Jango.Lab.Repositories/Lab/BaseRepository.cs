
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jango.Lab.Repositories.Lab
{
    public interface IBaseRepository<T> where T : class, new()
    {
        T Add(T entity);
        bool Update(T entity);


        bool Delete(T entity);

        IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda);

        IQueryable<T> GetAll();
        IEnumerable<T> GetAllList();

        IQueryable<T> LoadPageEntities<S>(int pageIndex, int pageSize, out int total, Expression<Func<T, bool>> whereLambda,
                                          bool isAsc, Expression<Func<T, S>> orderByLambda);

        IQueryable<T> FindBy(Expression<Func<T, bool>> func);

        T GetById(long id);

        Task InsertAsync(T model);
        T Get(Expression<Func<T, bool>> wherExpression);
    }

    public class BaseRepository<T> where T : class
    {

        private readonly DbContext _db = EFContextFactory.GetCurrentDbContext();


        public T Add(T entity)
        {
            _db.Entry<T>(entity).State = EntityState.Added;
            return entity;
        }

        public bool Update(T entity)
        {
            if (!_db.Set<T>().Local.Contains(entity))
            {
                _db.Set<T>().Attach(entity);
            }
            _db.Entry(entity).State = EntityState.Modified;

            return true;
        }

        public Task InsertAsync(T model)
        {
            return Task.FromResult(_db.Set<T>().Add(model));
        }

        public bool Delete(T entity)
        {
            _db.Set<T>().Attach(entity);
            _db.Entry<T>(entity).State = EntityState.Deleted;
            return true;
        }

        public IQueryable<T> GetAll()
        {
            return _db.Set<T>().AsQueryable();
        }

        public T Get(Expression<Func<T, bool>> wherExpression)
        {
            return _db.Set<T>().FirstOrDefault(wherExpression);
        }
        public IEnumerable<T> GetAllList()
        {
            return _db.Set<T>().AsEnumerable();
        }
        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda)
        {
            return _db.Set<T>().Where<T>(whereLambda).AsQueryable();
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> func)
        {
            return _db.Set<T>().Where<T>(func).AsQueryable();
        }

        public T GetById(long id)
        {
            return _db.Set<T>().Find(id);
        }
        public IQueryable<T> LoadPageEntities<S>(int pageIndex, int pageSize, out int total, Expression<Func<T, bool>> whereLambda,
                                                 bool isAsc, Expression<Func<T, S>> orderByLambda)
        {
            var temp = _db.Set<T>().Where<T>(whereLambda);
            total = temp.Count();
            if (isAsc)
            {
                temp = temp.OrderBy<T, S>(orderByLambda)
                    .Skip<T>(pageSize * (pageIndex - 1)) //越过多少条
                    .Take<T>(pageSize).AsQueryable(); //取出多少条
            }
            else
            {
                temp = temp.OrderByDescending<T, S>(orderByLambda)
                    .Skip<T>(pageSize * (pageIndex - 1)) //越过多少条
                    .Take<T>(pageSize).AsQueryable(); //取出多少条
            }
            return temp.AsQueryable();
        }
    }
}
