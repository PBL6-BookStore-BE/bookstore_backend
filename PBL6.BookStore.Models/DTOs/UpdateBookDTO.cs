using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL6.BookStore.Models.DTOs
{
    public class UpdateBookDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Pages { get; set; }
        public DateTime PublicationDate { get; set; }
        public int IdCategory { get; set; }
        public int IdPublisher { get; set; }
    }
}
