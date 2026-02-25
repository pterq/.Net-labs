using System;
using System.Collections.Generic;
using System.Text;

namespace Lab03_1_Core.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
    }
}
