using System.Linq.Expressions;

namespace Domain.Foundation
{
    public interface IRepository<T> where T : class
    {
        T First();
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        List<T> Where(Expression<Func<T, bool>> predicate);
        List<T> Where(Expression<Func<T, bool>> predicate, Expression<Func<T, bool>>[] includePredicats);
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, object>>[] includePredicats);
        IQueryable<T> GetQuery(Expression<Func<T, object>>[] includePredicats);

    }
}
