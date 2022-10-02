using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL6.BookStore.Models.Entities.Book
{
    public class Review: BaseEntity
    {
        public int Rating { get; set; }
        public string Comment { get; set; }

        public int IdBook { get; set; }
        public int IdUser { get; set; }
    }
}
