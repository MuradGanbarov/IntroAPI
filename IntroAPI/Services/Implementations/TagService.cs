using IntroAPI.Dtos.CategoryDtos;
using IntroAPI.Dtos.TagDtos;
using IntroAPI.Dtos.TagDtos;
using IntroAPI.Entities;
using IntroAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IntroAPI.Services.Implementations
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _repository;

        public TagService(ITagRepository repository)
        {
            _repository = repository;
        }
        public async Task CreateAsync(CreateTagDto tagDto)
        {
            await _repository.AddAsync(new Tag { Name = tagDto.Name});
        }

        public async Task DeleteAsync(int id)
        {
            Tag tag = await _repository.GetByIdAsync(id)??throw new Exception("Not found");
            _repository.Delete(tag);
            await _repository.SaveChangesAsync();

        }

        public async Task<ICollection<GetTagDto>> GetAllAsync(int page, int take)
        {
            ICollection<Tag> tags = await _repository.GetAllAsync(skip: (page - 1) * take, take: take, isTracking: false).ToListAsync();


            ICollection<GetTagDto> tagDtos = new List<GetTagDto>();

            foreach (var tag in tags)
            {
                tagDtos.Add(new GetTagDto
                {
                    Id = tag.Id,
                    Name = tag.Name,
                });
            }
            return tagDtos;
        }

        public async Task<ICollection<GetTagDto>> GetAllOrderByAsync(string OrderBy, bool isDescending, int page, int take, bool isTracking)
        {
            Expression<Func<Tag, object>> expression = GetOrderExpression(OrderBy);
            var result = await _repository.GetAllAsyncOrderBy(expressionOrder: expression, isDescending: isDescending, skip: (page - 1) * take, take: take, isTracking: isTracking).ToListAsync();
            ICollection<GetTagDto> resultList = new List<GetTagDto>();
            foreach (Tag resultitem in result)
            {
                resultList.Add(new GetTagDto
                {
                    Id = resultitem.Id,
                    Name = resultitem.Name,
                });
            }
            return resultList;
        }

        public async Task<GetTagDto> GetByIdAsync(int id)
        {
            Tag tag = await _repository.GetByIdAsync(id) ?? throw new Exception("Not found");
            return new GetTagDto { Id = tag.Id, Name = tag.Name };
        }

        public async Task Update(int id, UpdateTagDto updateTagDto)
        {
            Tag tag = await _repository.GetByIdAsync(id) ?? throw new Exception("Not found");
            tag.Name = updateTagDto.Name;
            _repository.Update(tag);
            await _repository.SaveChangesAsync();

        }

        public Expression<Func<Tag, object>> GetOrderExpression(string orderBy)
        {
            Expression<Func<Tag, object>>? expression = null;
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
