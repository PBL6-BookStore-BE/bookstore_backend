using MicroserviceBook.DTOs.Review;
using MicroserviceBook.ViewModels.ReviewVM;

namespace MicroserviceBook.Interfaces
{
    public interface IReviewRepository
    {
        public Task<IEnumerable<GetReviewVM>> GetAllReviewsAsync();
        public Task<GetReviewVM> GetReviewAsync(int Id);

        public Task<int> CreateReview(CreateReviewDTO model);
        public Task<int> UpdateReview(UpdateReviewDTO model);
        public Task<int> DeleteReview(int id);
        public Task<IEnumerable<GetReviewByBookId>> GetReviewsByIdBookAsync(int idBook);
    }
}
