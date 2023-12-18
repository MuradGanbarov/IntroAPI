using System.Linq.Expressions;

namespace IntroAPI.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity,new()
    {
        IQueryable<T> GetAllAsync(Expression<Func<T,bool>>? expression=null,int skip=0,int take=0,bool isTracking=false,params string[] includes);
        IQueryable<T> GetAllAsyncOrderBy(Expression<Func<T, object>> expressionOrder, int skip = 0, int take = 0,bool isDescending=false,bool isTracking=false,params string[] includes);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
    }
}
    