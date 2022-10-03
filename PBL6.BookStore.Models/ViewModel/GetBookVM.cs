using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL6.BookStore.Models.ViewModel
{
    public class GetBookVM
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Pulbisher { get; set; }
        public IEnumerable<AuthorVM> Authors { get; set; }
    }
}
