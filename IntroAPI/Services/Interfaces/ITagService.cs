using System.Linq.Expressions;
using IntroAPI.Dtos.TagDtos;

namespace IntroAPI.Services.Interfaces
{
    public interface ITagService
    {
        Task<ICollection<GetTagDto>> GetAllAsync(int page, int take);
        Task<GetTagDto> GetByIdAsync(int id);
        Task<ICollection<GetTagDto>> GetAllOrderByAsync(string OrderBy, bool isDescending, int page, int take, bool isTracking);
        Task CreateAsync(CreateTagDto tagDto);
        Task DeleteAsync(int id);
        Task Update(int id, UpdateTagDto updateTagDto);
    }
}
