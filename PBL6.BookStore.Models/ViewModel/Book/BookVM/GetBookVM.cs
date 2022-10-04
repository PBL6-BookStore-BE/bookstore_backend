using PBL6.BookStore.Models.ViewModel.Book.AuthorVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL6.BookStore.Models.ViewModel.Book.BookVM
{
    public class GetBookVM
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Pulbisher { get; set; }
        public IEnumerable<GetAuthorVM> Authors { get; set; }
    }
}
