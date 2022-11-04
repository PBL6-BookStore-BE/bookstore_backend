using AutoMapper;
using MicroserviceBook.Data;
using MicroserviceBook.DTOs.Category;
using MicroserviceBook.Entities;
using MicroserviceBook.Interfaces;
using MicroserviceBook.Service;
using MicroserviceBook.ViewModels.BookVM;
using MicroserviceBook.ViewModels.CategoryVM;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceBook.Respositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookDataContext _context;
        private readonly IMapper _mapper;
        private readonly IGetBookService _service;

        public CategoryRepository(BookDataContext context, IMapper mapper,IGetBookService service)
        {
            _context = context;
            _mapper = mapper;
            _service = service;
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

        public async Task<IEnumerable<GetBookVM>> SearchBookByCategoryFilter(string name)
        {

            if (!String.IsNullOrEmpty(name))
            {
                var res = await _context.Categories.Where(s => s.Name.ToLower().Contains(name.Trim().ToLower())).ToListAsync();
                if (res == null)
                {
                    return default;
                }
                else
                {
                    var list = (from p in res
                                join book in _context.Books
                          on p.Id equals book.IdCategory
                                select book.Id).ToList();
                    if (list == null)
                    {
                        return default;
                    }
                    else
                    {
                        var temp_list = new List<GetBookVM>();
                        foreach (var i in list)
                        {
                            var book = await _service.GetBookById(i);
                            if (book != null)
                            {
                                temp_list.Add(book);
                            }
                        }
                        return temp_list;
                    }

                }

            }
            else
            {
                return default;
            }
        }
    }
}
