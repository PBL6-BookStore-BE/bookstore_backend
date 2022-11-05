using AutoMapper;
using MicroserviceBook.DTOs.Cart;
using MicroserviceBook.Entities;

namespace MicroserviceBook.Helper
{
    public class MappingCartDetails :Profile
    {
        public MappingCartDetails()
        {
            CreateMap<CartDetailDTO, CartDetail>();
        }
    }
}
