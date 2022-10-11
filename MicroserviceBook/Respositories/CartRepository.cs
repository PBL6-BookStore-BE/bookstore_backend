using AutoMapper;
using MicroserviceBook.Data;
using MicroserviceBook.Entities;
using MicroserviceBook.Interfaces;
using MicroserviceBook.ViewModels.CartVM;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceBook.Respositories
{
    public class CartRepository : ICartRepository
    {

        private readonly BookDataContext _context;   
        private readonly IMapper _mapper;

        public CartRepository(BookDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        //public async Task<int> CreateCart(CreateCartDTO model)
        //{
        //    var mapped_cart = _mapper.Map<Cart>(model);
        //    _context.Carts.Add(mapped_cart);
        //    await _context.SaveChangesAsync();
        //    return mapped_cart.Id;

        //}

        public async Task<int> DeleteCart(int id)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.Id == id);   
            if( cart  == null)
            {
                return 0;
            }
            cart.IsDeleted = true;
            cart.DeletedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return cart.Id;

        }

        public Task<GetCartVM> GetCartAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GetCartVM> GetCartByIdUser(int idUser)
        {
            throw new NotImplementedException();
        }


        //public async Task<GetCartVM> GetCartByIdUser(int idUser)
        //{
        //    var cart = await _context.Carts.FirstOrDefaultAsync(c => c.IdUser == idUser);
        //    if (cart == null)
        //    {
        //        return default;
        //    }
        //    var cart_details = _context.CartDetails.Where(d => d.IdCart == cart.Id)
        //        .Select();





        //var mapped_cart_vm = _mapper.Map<GetCartVM>(cart);
        //return mapped_cart_vm;
        //var cart_details  = _context.CartDetails.Where(d => d.IdCart == cart.Id)
        //    .Select(d=> new GetCartVM
        //    {
        //        IdUse
        //    })      
    }


    //}
}
