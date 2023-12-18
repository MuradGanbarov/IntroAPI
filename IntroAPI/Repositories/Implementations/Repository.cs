using IntroAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IntroAPI.Repositories.Implementations
{
    public class Repository <T> : IRepository<T> where T : BaseEntity,new()
    {

        private readonly DbSet<T> _table;
        private readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _table = context.Set<T>();
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _table.AddAsync(entity);   
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }

        public IQueryable<T> GetAllAsync(Expression<Func<T, bool>>? expression = null,int skip = 0, int take = 0, bool isTracking = false, params string[] includes)
        {
            var query = _table.AsQueryable();

            if(expression is not null) query = query.Where(expression);

            if(skip != 0) query = query.Skip(skip);
            if (take != 0) query = query.Take(take);

            if(includes is not null)
            {
                for(int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            return isTracking?query:query.AsNoTracking();
        }

        public IQueryable<T> GetAllAsyncOrderBy(Expression<Func<T, object>> expressionOrder, int skip = 0, int take = 0, bool isDescending = false, bool isTracking = false, params string[] includes)
        {
            var query = _table.AsQueryable();
            
                if (isDescending) query = query.OrderBy(expressionOrder);
                else query = query.OrderByDescending(expressionOrder);
            

            if(skip != 0) query = query.Skip(skip);
            if (take != 0) query = query.Take(take);

            if(includes is not null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            return isTracking?query:query.AsNoTracking();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            T entity = await _table.FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _table.Update(entity);
        }
    }
}
