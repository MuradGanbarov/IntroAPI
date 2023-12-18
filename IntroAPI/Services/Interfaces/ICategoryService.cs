using System.Linq.Expressions;

namespace IntroAPI.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ICollection<GetCategoryDto>> GetAllAsync(int page,int take);
        Task<GetCategoryDto> GetByIdAsync(int id);
        Task<ICollection<GetCategoryDto>> GetAllOrderByAsync(string OrderBy, bool isDescending,int page,int take,bool isTracking);
        Task CreateAsync(CreateCategoryDto categoryDto);
        Task DeleteAsync(int id);
        Task Update(int id, UpdateCategoryDto updateCategoryDto);
    
    }

}
