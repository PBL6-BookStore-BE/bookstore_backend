using AutoMapper;
using MicroserviceBook.Data;
using MicroserviceBook.DTOs.Author;
using MicroserviceBook.Entities;
using MicroserviceBook.Interfaces;
using MicroserviceBook.Service;
using MicroserviceBook.Services;
using MicroserviceBook.ViewModels.AuthorVM;
using MicroserviceBook.ViewModels.BookVM;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceBook.Respositories
{
    public class AuthorRepository :IAuthorRepository
    {
        private readonly BookDataContext _context;
        private readonly IMapper _mapper;
        private readonly IGetBookService _service;
        private readonly ICurrentUserService _userService;
        public AuthorRepository(BookDataContext context, IMapper mapper, IGetBookService service, ICurrentUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _service = service;
            _userService = userService;
        }

        public async Task<int> CreateAuthor(CreateAuthorDTO model)
        {
            var AuthorEntity = _mapper.Map<Author>(model);
            _context.Authors.Add(AuthorEntity);
            await _context.SaveChangesAsync();
            return AuthorEntity.Id;
        }

        public async Task<int> DeleteAuthor(int id)
        {
            var author = _context.Authors.FirstOrDefault(x => x.Id == id);
            if (author == null)
            {
                return default;
            }
            else
            {
                author.IsDeleted = true;
                author.DeletedDate = DateTime.Now;
                await _context.SaveChangesAsync();
                return author.Id;
            }
        }
        
        public async Task<IEnumerable<GetAllAuthorsVM>> GetAllAuthors()
        {
            var id = _userService.Id;
            var authors = await (from a in _context.Authors
                                 where a.IsDeleted == false
                                 select new GetAllAuthorsVM
                                 {
                                     Id = a.Id,
                                     Name = a.Name,
                                     Description = a.Description
                                 }).ToListAsync();
            return authors.AsReadOnly();
        }
        public async Task<GetAuthorVM> GetAuthor(int id)
        {
            var author = await _context.Authors.Where(a => a.Id == id).Select(
               a => new GetAuthorVM
               {
                   Id = a.Id,
                   Name = a.Name,
                   Description = a.Description,
                   BookIds = (from author in _context.Authors
                            join
                             ba in _context.BookAuthors
                             on author.Id equals ba.IdAuthor
                            where author.Id == id
                            select ba.IdBook
                            ).ToList()
               }).SingleOrDefaultAsync();
            if (author == null) return default;
            else
            {
                if (author.BookIds == null)
                {
                    author.Books = null;
                }
                else
                {
                    var temp_list = new List<GetBookVM>();
                    foreach (var item in author.BookIds)
                    {
                        temp_list.Add(await _service.GetBookById(item));
                    }
                    author.Books = temp_list;
                }
            }
            return author;
        }
        public async Task<int> UpdateAuthor(UpdateAuthorDTO model)
        {
            var author = _context.Authors.FirstOrDefault(a => a.Id == model.Id);
            if (author == null)
            {
                return default;
            }
            else
            {
                author.Name = model.Name;
                author.Description = model.Description;
                await _context.SaveChangesAsync();
                return author.Id;

            }
        }
    }
}
    

