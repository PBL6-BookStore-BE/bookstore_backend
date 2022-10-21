using AutoMapper;
using MicroserviceBook.DTOs.Publisher;
using MicroserviceBook.Entities;
using MicroserviceBook.ViewModels.PublisherVM;

namespace MicroserviceBook.Helper
{
    public class MappingPublishers : Profile
    {
        public MappingPublishers()
        {
            CreateMap<Publisher, GetAllPublishersVM>();
            CreateMap<Publisher, GetPublisherVM>();
            CreateMap<CreatePublisherDTO, Publisher>();
            CreateMap<CreatePublisherDTO, Publisher>();
        }
    }
}
