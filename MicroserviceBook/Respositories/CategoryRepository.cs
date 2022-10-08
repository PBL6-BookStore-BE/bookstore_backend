using AutoMapper;
using MicroserviceBook.Data;
using MicroserviceBook.DTOs.Category;
using MicroserviceBook.Entities;
using MicroserviceBook.Interfaces;
using MicroserviceBook.ViewModels.CategoryVM;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceBook.Respositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookDataContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(BookDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> CreateCategory(CreateCategoryDTO model)
        {
            var CategoryEntity = _mapper.Map<Category>(model);
            _context.Categories.Add(CategoryEntity);
            await _context.SaveChangesAsync();
            return CategoryEntity.Id;
        }

        public async Task<int> DeleteCategory(int id)
        {
            var CategoryEntity = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (CategoryEntity == null)
            {
                return -1;
            }
            else
            {
                CategoryEntity.IsDeleted = true;
                CategoryEntity.DeletedDate = DateTime.Now;
                await _context.SaveChangesAsync();
                return CategoryEntity.Id;
            }
        }

        public async Task<IEnumerable<GetAllCategoriesVM>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories.Where(b => b.IsDeleted == false).ToListAsync();
            var results = categories.Select(i => _mapper.Map<GetAllCategoriesVM>(i));
            return results;
        }

        public async Task<GetCategoryVM> GetCategory(int id)
        {
            var category = await _context.Categories.Where(i => i.Id == id).FirstOrDefaultAsync();
            var result = _mapper.Map<GetCategoryVM>(category);
            return result;
        }

        public async Task<int> UpdateCategory(UpdateCategoryDTO model)
        {
            if (model == null)
                return -1;

            var category = await _context.Categories.Where(i => i.Id == model.Id).FirstOrDefaultAsync();
            if (category == null)
                return -1;
            category.Name = model.Name;
            await _context.SaveChangesAsync();
            return category.Id;
        }
    }
}
