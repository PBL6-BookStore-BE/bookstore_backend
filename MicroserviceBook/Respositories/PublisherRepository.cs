using AutoMapper;
using MicroserviceBook.DTOs.Publisher;
using MicroserviceBook.Entities;
using MicroserviceBook.Interfaces;
using MicroserviceBook.ViewModels.PublisherVM;
using Microsoft.EntityFrameworkCore;
using MicroserviceBook.Data;
using MicroserviceBook.Service;
using MicroserviceBook.ViewModels.BookVM;



    public class PublisherRepository : IPublisherRepository
    {
        private readonly BookDataContext _context;
        private readonly IMapper _mapper;
        private readonly IGetBookService _service;

        public PublisherRepository(BookDataContext context, IMapper mapper, IGetBookService serivce)
        {
            _context = context;
            _mapper = mapper;
            _service = serivce;
        }
        public async Task<IEnumerable<GetAllPublishersVM>> GetAllPublisherAsync()
        {
            var publishers = await _context.Publishers.Where(p => p.IsDeleted == false).ToListAsync();
            var list = _mapper.Map<IEnumerable<GetAllPublishersVM>>(publishers);
            return list;

        }
        
        public async Task<GetPublisherVM> GetPublisherAsync(int id)
        {
            var publisher = await _context.Publishers.Where(p => p.Id == id  && p.IsDeleted == false).FirstOrDefaultAsync();
            if (publisher != null)
        {
            var mapperPublisher = _mapper.Map<GetPublisherVM>(publisher);
            return mapperPublisher;

        }
        return default;
    }
        public async Task<int> CreatePublisher(CreatePublisherDTO model)
        {
            var _mapperPublisher = _mapper.Map<Publisher>(model);
            _context.Publishers.Add(_mapperPublisher);
            await _context.SaveChangesAsync();
            return _mapperPublisher.Id;


        }

        public async Task<int> DeletePublisher(int id)
        {
            var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);
            if (publisher == null)
            {
                return default;
            }
            else
            {
                publisher.IsDeleted = true;
                publisher.DeletedDate = DateTime.Now;
                await _context.SaveChangesAsync();
                return publisher.Id;
            }
        }

        public async Task<int> UpdatePublisher(UpdatePublisherDTO model)
        {
            var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.Id == model.Id  && p.IsDeleted == false);
            if (publisher == null)
            {
                return default;
            }
            else
            {
                publisher.Name = model.Name;
                await _context.SaveChangesAsync();
                return publisher.Id;
            }
        }

    public async Task<IList<int>> getPublisherByName(string name)
    {
        var res = String.IsNullOrEmpty(name) ?
            await _context.Publishers.Where(p => p.IsDeleted == false).Select(p => p.Id).ToListAsync()
            : await _context.Publishers.Where(s => s.Name.ToLower().Contains(name.Trim().ToLower())  && s.IsDeleted == false).Select(p=>p.Id).ToListAsync();
        return res;
    }

}

