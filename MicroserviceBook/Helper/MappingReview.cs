using AutoMapper;
using MicroserviceBook.DTOs.Review;
using MicroserviceBook.Entities;
using MicroserviceBook.ViewModels.ReviewVM;

namespace MicroserviceBook.Helper
{
    public class MappingReview :Profile
    {
        public MappingReview()
        {
            CreateMap<Review, GetReviewVM>();
            CreateMap<CreateReviewDTO, Review>();
            
        }
    }
}
