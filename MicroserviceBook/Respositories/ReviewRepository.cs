using AutoMapper;
using MicroserviceBook.Data;
using MicroserviceBook.DTOs.Review;
using MicroserviceBook.Entities;
using MicroserviceBook.Interfaces;
using MicroserviceBook.ViewModels.ReviewVM;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceBook.Respositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly BookDataContext _context;
        private readonly IMapper _mapper;

        public ReviewRepository(BookDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetReviewVM>> GetAllReviewsAsync()
        {
            var reviews = await _context.Reviews.Where(r => r.IsDeleted == false).ToListAsync();
            var reviewVM = _mapper.Map<IEnumerable<GetReviewVM>>(reviews);
            return reviewVM;


        }

        public async Task<GetReviewVM> GetReviewAsync(int Id)
        {
            var review = await _context.Reviews.Where(r => r.Id == Id).FirstOrDefaultAsync();
            var reviewVM  = _mapper.Map<GetReviewVM>(review);
            return reviewVM;
        }
        public async Task<int> CreateReview(CreateReviewDTO model)
        {

            var dbContextTransaction = _context.Database.BeginTransaction();
            try
            {
               
                var review = _mapper.Map<Review>(model);
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
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == model.Id);
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
