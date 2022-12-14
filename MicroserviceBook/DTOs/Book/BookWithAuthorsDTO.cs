using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroserviceBook.DTOs.Book
{
    public class BookWithAuthorsDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Pages { get; set; }

        [Column(TypeName = "Date")]
        public DateTime PublicationDate { get; set; }
        public int IdCategory { get; set; }
        public int IdPublisher { get; set; }
        public IEnumerable<int> IdAuthors { get; set; } 
        public List<IFormFile> list_img { get; set; }
        public string Description { get; set; }
    }
}

