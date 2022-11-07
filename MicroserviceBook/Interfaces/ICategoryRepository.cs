using MicroserviceBook.DTOs.Author;
using MicroserviceBook.DTOs.Category;
using MicroserviceBook.ViewModels.BookVM;
using MicroserviceBook.ViewModels.CategoryVM;

namespace MicroserviceBook.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable<GetAllCategoriesVM>> GetAllCategoriesAsync();
        public Task<GetCategoryVM> GetCategory(int id);

        public Task<int> CreateCategory(CreateCategoryDTO model);

        public Task<int> UpdateCategory(UpdateCategoryDTO model);
        public Task<int> DeleteCategory(int id);

        public Task<IList<int>> getCategoryByName(string name);
    }
}
