using MicroserviceOrder.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL6.BookStore.Models.Entities.Order
{
    public class OrderDetail: BaseEntity
    {
        public int Quantity { get; set; }
        public int IdOrder { get; set; }
        public int IdBook { get; set; }
    }
}
