using MicroserviceBook.DTOs.Publisher;
using MicroserviceBook.ViewModels.PublisherVM;

namespace MicroserviceBook.Interfaces
{
    public interface IPublisherRepository
    {
        public Task<IEnumerable<GetAllPublishersVM>> GetAllPublisherAsync();
        public Task<GetPublisherVM> GetPublisherAsync(int id);
        public Task<int> CreatePublisher(CreatePublisherDTO model);
        public Task<int> UpdatePublisher(UpdatePublisherDTO model);

        public Task<int> DeletePublisher(int id);
    }
}
