using IntroAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IntroAPI.Repositories.Implementations
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }




        Task IRepository.AddAsync(Category category)
        {
            throw new NotImplementedException();
        }

        void IRepository.Delete(Category category)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Category>> IRepository.GetAllAsync(Expression<Func<Category, bool>> expression, params string[] includes)
        {
            var query = _context.Categories.AsQueryable();
            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            return (Task<IEnumerable<Category>>)query;
        }

        Task<Category> IRepository.GetByIdAsync(int id)
        {
            Category category = _context.Categories.FirstOrDefault(c => c.Id == id);
            return category;
        }

        Task IRepository.SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        void IRepository.Update(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
