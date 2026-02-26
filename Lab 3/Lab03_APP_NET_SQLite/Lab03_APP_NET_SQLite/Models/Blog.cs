using Lab03_APP_NET_SQLite;
using Microsoft.EntityFrameworkCore;


using System;
using System.Collections.Generic;
using System.Text;

namespace Lab03_APP_NET_SQLite.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
        public List<Post> Posts { get; } = new();
    }
}
