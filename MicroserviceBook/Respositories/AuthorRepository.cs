using AutoMapper;
using MicroserviceBook.Data;
using MicroserviceBook.DTOs.Author;
using MicroserviceBook.Entities;
using MicroserviceBook.Interfaces;
using MicroserviceBook.Service;
using MicroserviceBook.Services;
using MicroserviceBook.ViewModels.AuthorVM;
using MicroserviceBook.ViewModels.BookVM;
using Microsoft.AspNetCore.Authorization;
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
        //[Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
        public async Task<int> CreateAuthor(CreateAuthorDTO model)
        {
            var AuthorEntity = _mapper.Map<Author>(model);
            _context.Authors.Add(AuthorEntity);
            await _context.SaveChangesAsync();
            return AuthorEntity.Id;
        }
        //[Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
        public async Task<int> DeleteAuthor(int id)
        {
            var author = _context.Authors.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);
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
        public async Task<GetAllAuthorsVM> GetAuthor(int id)
        {
            var author = await _context.Authors.Where(a => a.Id == id && a.IsDeleted == false).FirstOrDefaultAsync();
            if (author == null) return default;
            return _mapper.Map<GetAllAuthorsVM>(author);
        }

        public async Task<IEnumerable<GetAllAuthorsVM>> GetAuthorByNameFilter(string? name)
        {

            var res = String.IsNullOrEmpty(name) ?
                await _context.Authors.Where(p => p.IsDeleted == false).ToListAsync()
                : await _context.Authors.Where(s => s.Name.ToLower().Contains(name.Trim().ToLower()) && s.IsDeleted == false).ToListAsync();
            if (res.Count == 0) return new List<GetAllAuthorsVM>();
            return _mapper.Map<IEnumerable<GetAllAuthorsVM>>(res);
        }
        
        public async Task<int> UpdateAuthor(UpdateAuthorDTO model)
        {
            var author = _context.Authors.FirstOrDefault(a => a.Id == model.Id && a.IsDeleted ==false);
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
    

