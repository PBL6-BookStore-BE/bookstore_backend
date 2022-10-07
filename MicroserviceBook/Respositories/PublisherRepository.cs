using AutoMapper;
using MicroserviceBook.Data;

namespace MicroserviceBook.Respositories
{
    public class PublisherRepository
    {
        private readonly BookDataContext _context;
        private readonly IMapper _mapper;

        public PublisherRepository(BookDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


    }
}
