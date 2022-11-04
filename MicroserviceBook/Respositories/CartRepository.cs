using AutoMapper;
using MicroserviceBook.Data;
using MicroserviceBook.DTOs.Cart;
using MicroserviceBook.Entities;
using MicroserviceBook.Interfaces;
using MicroserviceBook.Services;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceBook.Respositories
{ 
    public class CartRepository : ICartRepository
    {

        private readonly BookDataContext _context;
        private readonly ICurrentUserService _service;
        private readonly IMapper _mapper;
        public CartRepository(BookDataContext context, ICurrentUserService service,IMapper mapper)
        {
            _context = context;
            _service = service;
            _mapper = mapper;
        }
        //public async Task<int> CreateCart(CreateCartDTO model)
        //{
        //    var mapped_cart = _mapper.Map<Cart>(model);
        //    _context.Carts.Add(mapped_cart);
        //    await _context.SaveChangesAsync();
        //    return mapped_cart.Id;

        public async Task<int> CreateCart()
        {

            var Id = _service.Id;
            var temp_id = await _context.Carts.Where(c => c.IdUser == Id).FirstOrDefaultAsync();
            if(temp_id == null)
            {
                //Them cart
                var cartDTO = new CreateCartDTO
                {
                    IdUser = Id
                };
                var cart = _mapper.Map<Cart>(cartDTO);
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
                return cart.Id;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> GetIdCart(string IdUser)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.IdUser == IdUser);
            if (cart == null)
            {
                return 0;
            }
            else
            {
                return cart.Id;
            }
        }

        //public Task<GetCartVM> GetCartAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<GetCartVM> GetCartByIdUser(int idUser)
        //{
        //    throw new NotImplementedException();
        //}


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
