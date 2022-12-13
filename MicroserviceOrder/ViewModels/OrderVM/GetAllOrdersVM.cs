using PBL6.BookStore.Models.Entities.Order;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroserviceOrder.ViewModels.OrderVM
{
    public class GetAllOrdersVM
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public string IdUser { get; set; }
        public string Payment { get; set; }
        public string? OrderAddress { get; set; }
    }
}
