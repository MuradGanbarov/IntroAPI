using IntroAPI.Dtos.CategoryDtos;
using IntroAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IntroAPI.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }


        public async Task<ICollection<GetCategoryDto>> GetAllAsync(int page, int take)
        {
            ICollection<Category> categories = await _repository.GetAllAsync(skip: (page - 1)*take,take:take,isTracking:false).ToListAsync();
            

            ICollection<GetCategoryDto> categoryDtos = new List<GetCategoryDto>();

            foreach(var category in categories)
            {
                categoryDtos.Add(new GetCategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                });
            }
            return categoryDtos;
        }

        public async Task<GetCategoryDto> GetByIdAsync(int id)
        {
            Category category = await _repository.GetByIdAsync(id)??throw new Exception("Not found");
            return new GetCategoryDto { Id = category.Id, Name = category.Name };
        }
        public async Task CreateAsync(CreateCategoryDto categoryDto)
        {
            await _repository.AddAsync(new Category { Name = categoryDto.Name });
            await _repository.SaveChangesAsync();
        }

        


        public async Task DeleteAsync(int id)
        {
            Category category = await _repository.GetByIdAsync(id)??throw new Exception("Not found");
            _repository.Delete(category);
            await _repository.SaveChangesAsync();
        }

        public async Task Update(int id, UpdateCategoryDto updateCategoryDto)
        {
            Category category = await _repository.GetByIdAsync(id) ?? throw new Exception("Not found");
            category.Name = updateCategoryDto.Name;
            _repository.Update(category);            
            await _repository.SaveChangesAsync();
        }
        
        public async Task<ICollection<GetCategoryDto>> GetAllOrderByAsync(string OrderBy, bool isDescending, int page, int take, bool isTracking)
        {
            Expression<Func<Category, object>> expression = GetOrderExpression(OrderBy);
            var result = await _repository.GetAllAsyncOrderBy(expressionOrder:expression, isDescending:isDescending, skip:(page-1)*take,take: take, isTracking:isTracking).ToListAsync();
            ICollection<GetCategoryDto> resultList = new List<GetCategoryDto>();
            foreach(Category resultitem in result)
            {
                resultList.Add(new GetCategoryDto
                {
                    Id = resultitem.Id,
                    Name = resultitem.Name,
                });
            }
            return resultList;
        }


        public Expression<Func<Category, object>> GetOrderExpression(string orderBy)
        {
            Expression<Func<Category, object>>? expression = null;
            switch (orderBy.ToLower())
            {
                case "name":
                    expression = c => c.Name;
                    break;
                case "id":
                    expression = c => c.Id;
                    break;
            }

            return expression;
        }

    }
}
