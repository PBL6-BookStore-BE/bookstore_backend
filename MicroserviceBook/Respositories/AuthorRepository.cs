using AutoMapper;
using MicroserviceBook.Data;
using MicroserviceBook.DTOs.Author;
using MicroserviceBook.Entities;
using MicroserviceBook.Interfaces;
using MicroserviceBook.ViewModels.AuthorVM;
using MicroserviceBook.ViewModels.BookVM;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceBook.Respositories
{
    public class AuthorRepository :IAuthorRepository
    {
        private readonly BookDataContext _context;
        private readonly IMapper _mapper;
        public AuthorRepository(BookDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            var authors = await (from a in _context.Authors
                                 where a.IsDeleted == false
                                 select new GetAllAuthorsVM
                                 {
                                     Name = a.Name
                                 }).ToListAsync();
            return authors.AsReadOnly();
        }

        public async Task<GetAuthorVM> GetAuthor(int id)
        {
            var author = await _context.Authors.Where(a => a.Id == id).Select(
               a => new GetAuthorVM
               {
                   Name = a.Name,
                   Description = a.Description,
                   ImageUrl = a.ImageUrl,
                   Books = (from author in _context.Authors
                            join
                             ba in _context.BookAuthors
                             on author.Id equals ba.IdAuthor
                            join b in _context.Books
                            on ba.IdBook equals b.Id
                            where author.Id == id
                            select new GetAllBooksVM
                            {
                                Name = b.Name,
                                Price = b.Price
                            }).ToList()

               }).SingleOrDefaultAsync();
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
                author.ImageUrl = model.ImageUrl;


                await _context.SaveChangesAsync();
                return author.Id;

            }
        }
    }
}
    

