using AutoMapper;
using MicroserviceOrder.Data;
using MicroserviceOrder.DTOs.Payment;
using MicroserviceOrder.Interfaces;
using MicroserviceOrder.ViewModels.PaymentVM;
using Microsoft.EntityFrameworkCore;
using PBL6.BookStore.Models.Entities.Order;

namespace MicroserviceOrder.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly OrderDataContext _context;
        private readonly IMapper _mapper;
        public PaymentRepository(OrderDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreatePayment(CreatePaymentDTO model)
        {
            var payment_entity = _mapper.Map<Payment>(model);
            _context.Payments.Add(payment_entity);
            await _context.SaveChangesAsync();
            return payment_entity.Id;
        }

        public async Task<int> DeletePayment(int id)
        {
            var payment_entity = await _context.Payments.FirstOrDefaultAsync(c => c.Id == id);
            if (payment_entity == null)
            {
                return -1;
            }
            else
            {
                payment_entity.IsDeleted = true;
                payment_entity.DeletedDate = DateTime.Now;
                await _context.SaveChangesAsync();
                return payment_entity.Id;
            }
        }

        public async Task<IEnumerable<GetAllPaymentsVM>> GetAllPaymentsAsync()
        {
            var payments = await _context.Payments.Where(c => c.IsDeleted == false).ToListAsync();
            var results = payments.Select(c => _mapper.Map<GetAllPaymentsVM>(c));
            return results;
        }

        public async Task<GetPaymentVM> GetPayment(int id)
        {
            var payment = await _context.Payments.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (payment == null)
            {
                return null;
            }
            var result = _mapper.Map<GetPaymentVM>(payment);
            return result;
        }

        public async Task<int> UpdatePayment(UpdatePaymentDTO model)
        {
            if (model == null)
                return -1;

            var payment = await _context.Payments.FirstOrDefaultAsync(c => c.Id == model.Id);
            if (payment == null)
                return -1;
            payment.Name = model.Name;
            await _context.SaveChangesAsync();
            return payment.Id;
        }
    }
}
