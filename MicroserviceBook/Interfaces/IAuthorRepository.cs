using MicroserviceBook.DTOs.Author;
using MicroserviceBook.ViewModels.AuthorVM;

namespace MicroserviceBook.Interfaces
{
    public interface IAuthorRepository
    {
        public Task<IEnumerable<GetAllAuthorsVM>> GetAllAuthors();
        public Task<GetAllAuthorsVM> GetAuthor(int id);

        public Task<int> CreateAuthor(CreateAuthorDTO model);

        public Task<int> UpdateAuthor(UpdateAuthorDTO model);
        public Task<int> DeleteAuthor(int id);
        public Task<IEnumerable<GetAllAuthorsVM>> GetAuthorByNameFilter(string? name);
    }
}
