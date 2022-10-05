using AutoMapper;
using MicroserviceBook.Data;
using MicroserviceBook.Interfaces;
using MicroserviceBook.ViewModels.BookVM;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceBook.Respositories
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public BookRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetAllBooksVM>> GetAllBooks()
        {
            var list = await (from b in _context.Books
                              where b.IsDeleted == false
                              select new GetAllBooksVM
                              {
                                  Name = b.Name,
                                  Price = b.Price
                              }).ToListAsync();
            return list.AsReadOnly();
        }
    }
}
