using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBL6.BookStore.Models.DTOs.Book.BookDTO
{
    public class CreateBookDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Pages { get; set; } 
        public int Rating { get; set; }

        [Column(TypeName = "Date")]
        public DateTime PublicationDate { get; set; }
        public int IdCategory { get; set; }
        public int IdPublisher { get; set; }
        public string Description { get; set; }

    }
}