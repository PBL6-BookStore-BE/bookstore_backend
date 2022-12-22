using AutoMapper;
using MicroserviceBook.Data;
using MicroserviceBook.DTOs.Cart;
using MicroserviceBook.Entities;
using MicroserviceBook.Interfaces;
using MicroserviceBook.Service;
using MicroserviceBook.Services;
using MicroserviceBook.ViewModels.CartVM;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceBook.Respositories
{
    public class CartDetailRepository : ICartDetailRepository
    {

        private readonly BookDataContext _context;   
        private readonly IMapper _mapper;
        private readonly IGetBookService _service;
        private readonly ICurrentUserService _userService;
        private readonly ICartRepository _cartRepository;

        public CartDetailRepository(BookDataContext context, IMapper mapper,IGetBookService getBookService, ICurrentUserService service, ICartRepository cartRepository)
        {
            _context = context;
            _mapper = mapper;
            _service = getBookService;
            _userService = service;
            _cartRepository = cartRepository;
        }


        public async Task<int> ChangeQuantity(CartDetailDTO model)
        {
            var id_user = _userService.Id;
            var id_cart = await _context.Carts.Where(c => c.IdUser == id_user).Select(c => c.Id).FirstOrDefaultAsync();
            if (id_cart!= null)
            {
                var item = await _context.CartDetails.Where(i => (i.IdCart == id_cart) && (i.IdBook == model.Id) && (i.IsDeleted == false)).FirstOrDefaultAsync();
                if (item == null)
                {
                    return default;
                }
                else
                {
                    item.Quantity = model.Quantity;
                    await _context.SaveChangesAsync();
                    return item.Id;
                }
            }
            else
            {
                return -1;
            }
        }

        public async Task<int> CreateCartDetail(CartDetailDTO model)
        {

            var id_user = _userService.Id;
            var id_cart = await _context.Carts.Where(c => c.IdUser == id_user).Select(c => c.Id).FirstOrDefaultAsync();
            if (id_cart == 0)
            {
                id_cart = await _cartRepository.CreateCart();

            }
                var item = await _context.CartDetails.Where(i => (i.IdCart == id_cart) && (i.IdBook == model.Id) && (i.IsDeleted == false)).FirstOrDefaultAsync();
                if (item == null)
                {
                    var cart = _mapper.Map<CartDetail>(model);
                    cart.IdCart = id_cart;
                    _context.CartDetails.Add(cart);
                    await _context.SaveChangesAsync();
                    return cart.Id;

                }
                else
                {
                    item.Quantity += model.Quantity;
                    await _context.SaveChangesAsync();
                    return item.Id;
                }


            
          
        }

        public async Task<int> DeleteCartDetail(DeleteCartDTO model)
        {
            var id_user = _userService.Id;
            var id_cart = await _context.Carts.Where(c => c.IdUser == id_user).Select(c => c.Id).FirstOrDefaultAsync();
            if (id_cart!= 0)
            {
                var item = await _context.CartDetails.Where(i => (i.IdCart == id_cart) && (i.IdBook == model.Id) && (i.IsDeleted == false)).FirstOrDefaultAsync();
                if (item == null)
                {
                    return default;
                }
                else
                {
                    item.IsDeleted = true;
                    item.DeletedDate = DateTime.Now;

                    await _context.SaveChangesAsync();
                    return item.Id;
                }
            }
            else
            {
                return -1;
            }

        }

        public async Task<IEnumerable<GetCartDetailVM>> GetCartByUser()
        {
            var id_user = _userService.Id;
            var id_cart = await _context.Carts.Where(c => c.IdUser == id_user).Select(c => c.Id).FirstOrDefaultAsync();
            var items = await _context.CartDetails.Where(cd => cd.IdCart == id_cart && cd.IsDeleted == false).ToListAsync();
            if (items == null)
            {
                return default;
            }
            else
            {
                var result = new List<GetCartDetailVM>();
                foreach (var i in items)
                {
                    var temp = new GetCartDetailVM
                    { 
                        Id = i.Id,
                        Quantity = i.Quantity,
                        bookVM = await _service.GetBookById(i.IdBook)
                    };
                    result.Add(temp);

                }
                return result;
            }
           
        }
        public async Task<int> DeleteAllAsync()
        {
            var id_user = _userService.Id;
            var id_cart = await _context.Carts.Where(c => c.IdUser == id_user).Select(c => c.Id).FirstOrDefaultAsync();
            if (id_cart != 0)
            {
                var items = await _context.CartDetails.Where(i => (i.IdCart == id_cart) && (i.IsDeleted == false)).ToListAsync();
                if (items.Count != 0)
                {
                    foreach (var item in items)
                    {
                        item.IsDeleted = true;
                        item.DeletedDate = DateTime.Now;
                    }

                    await _context.SaveChangesAsync();

                }

            }
            return id_cart;
        }

        public async Task<int> DeleteCartDetailsById(int id)
        {
            var item = await _context.CartDetails.Where(i => (i.Id == id) && (i.IsDeleted == false)).FirstOrDefaultAsync();
            if (item == null) return default;

            item.IsDeleted = true;
            item.DeletedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return item.Id;


        }

        public async Task<int> ChangeQuantityForMobile(CartDetailDTO model)
        {
            var item = await _context.CartDetails.Where(i => (i.Id == model.Id) && (i.IsDeleted == false)).FirstOrDefaultAsync();
            if (item == null) return default;

            item.Quantity = model.Quantity;
            await _context.SaveChangesAsync();
            return item.Id;

        }
    }
}
