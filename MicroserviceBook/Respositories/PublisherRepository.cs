using AutoMapper;
using MicroserviceBook.Data;
using MicroserviceBook.DTOs.Publisher;
using MicroserviceBook.Entities;
using MicroserviceBook.Interfaces;
using MicroserviceBook.ViewModels.PublisherVM;
using Microsoft.EntityFrameworkCore;

public class PublisherRepository : IPublisherRepository
{
    private readonly BookDataContext _context;
    private readonly IMapper _mapper;

    public PublisherRepository(BookDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<IEnumerable<GetAllPublishersVM>> GetAllPublisherAsync()
    {
        var publishers = await _context.Publishers.Where(p => p.IsDeleted == false).ToListAsync();
        var list = _mapper.Map<IEnumerable<GetAllPublishersVM>>(publishers);
        return list;

    }

    public async Task<GetPublisherVM> GetPublisherAsync(int id)
    {
        var publisher = await _context.Publishers.Where(p => p.Id == id).FirstOrDefaultAsync();

        var mapperPublisher = _mapper.Map<GetPublisherVM>(publisher);
        return mapperPublisher;

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
        var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.Id == id);
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
        var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.Id == model.Id);
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


}