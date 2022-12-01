using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroserviceBook.Entities
{
    public class Book: BaseEntity
    {
        public string Name { get; set; }
        public int Pages { get; set; }
        public decimal Rating { get; set; }

        [Column(TypeName = "Date")]
        public DateTime PublicationDate { get; set; }
        public decimal Price { get; set; }
        public int IdCategory { get; set; }
        public int IdPublisher { get; set; }
        public string UrlImage { get; set; }
        public string Description { get; set; }
    }
}
