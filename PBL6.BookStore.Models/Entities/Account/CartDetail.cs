using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL6.BookStore.Models.Entities.Account
{
    public class CartDetail: BaseEntity
    {
        public int Quantity { get; set; }
        public int IdCart { get; set; }
        public string IdBook { get; set; }
    }
}
