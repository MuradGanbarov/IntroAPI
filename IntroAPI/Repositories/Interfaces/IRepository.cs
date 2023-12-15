using System.Linq.Expressions;

namespace IntroAPI.Repositories.Interfaces
{
    public interface IRepository
    {
        Task<IEnumerable<Category>> GetAllAsync(Expression<Func<Category,bool>>expression=null,params string[] includes);
        Task<Category> GetByIdAsync(int id);
        Task AddAsync(Category category);
        void Update(Category category);
        void Delete(Category category);
        Task SaveChangesAsync();
    }
}
