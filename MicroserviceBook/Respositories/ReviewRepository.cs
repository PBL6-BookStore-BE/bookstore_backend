using AutoMapper;
using MicroserviceBook.Data;
using MicroserviceBook.DTOs.Review;
using MicroserviceBook.Entities;
using MicroserviceBook.Interfaces;
using MicroserviceBook.Services;
using MicroserviceBook.ViewModels.ReviewVM;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceBook.Respositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly BookDataContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _service;
        public ReviewRepository(BookDataContext context, IMapper mapper, ICurrentUserService service)
        {
            _context = context;
            _mapper = mapper;
            _service = service;
        }
        public async Task<IEnumerable<GetReviewVM>> GetAllReviewsAsync()
        {
            var IdUser = _service.Id;
            var reviews = await _context.Reviews.Where(r => r.IsDeleted == false && r.IdUser == IdUser).ToListAsync();
            var reviewVM = new List<GetReviewVM>();
            foreach (var i in reviews)
            {
                var k =   _mapper.Map<GetReviewVM>(i);
                k.Username = _service.Username;
                reviewVM.Add(k);
            }
            return reviewVM;


        }

        public async Task<GetReviewVM> GetReviewAsync(int Id)
        {
            var review = await _context.Reviews.Where(r => r.Id == Id && r.IsDeleted == false).FirstOrDefaultAsync();
            var reviewVM  = _mapper.Map<GetReviewVM>(review);
            reviewVM.Username = _service.Username;
            return reviewVM;
        }
        public async Task<int> CreateReview(CreateReviewDTO model)
        {

            var dbContextTransaction = _context.Database.BeginTransaction();
            try
            {
               
                var review = _mapper.Map<Review>(model);
                review.IdUser = _service.Id;
                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();

                var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == review.IdBook);

                decimal sum_rating = _context.Reviews.Where(r => r.IdBook == book.Id).Sum(r => r.Rating);
                int count_review = _context.Reviews.Where(r => r.IdBook == book.Id).Count();
                book.Rating = Convert.ToDecimal(sum_rating / (count_review));
                await _context.SaveChangesAsync();
                await dbContextTransaction.CommitAsync();
                await dbContextTransaction.DisposeAsync();
                return review.Id;

            }
            catch (Exception)
            {

                await dbContextTransaction.RollbackAsync();
                await dbContextTransaction.DisposeAsync();
                throw;
            }


        }

        public async Task<int> DeleteReview(int id)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
            if (review == null)
            {
                return 0;
            }
            else
            {
                review.IsDeleted = true;
                review.DeletedDate = DateTime.Now;
                await _context.SaveChangesAsync();
                return review.Id;

            }
        }

       

        public async Task<int> UpdateReview(UpdateReviewDTO model)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == model.Id && r.IsDeleted == false);
            if (review == null)
            {
                return 0;
            }
            else
            {
                review.Rating = model.Rating;
                review.Comment = model.Comment;
                await _context.SaveChangesAsync();
                return review.Id;
            }
        }

   
    }
}
