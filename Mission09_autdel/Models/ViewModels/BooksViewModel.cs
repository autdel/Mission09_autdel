using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission09_autdel.Models.ViewModels
{
    public class BooksViewModel
    {
        // Contains the data from the repository
        public IQueryable<Book> Books { get; set; }

        // Contains the page info
        public PageInfo PageInfo { get; set; }
    }
}
