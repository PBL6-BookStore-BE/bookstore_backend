using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL6.BookStore.Models.DTOs.Book.BookDTO
{
    public class UpdateBookDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Pages { get; set; }

        [Column(TypeName = "Date")]
        public DateTime PublicationDate { get; set; }
        public int IdCategory { get; set; }
        public int IdPublisher { get; set; }
        public string Description { get; set; }
        public IEnumerable<int> IdAuthors { get; set; }
        public List<IFormFile>? list_img { get; set; }
    }
}
