using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lab04_MVC.Models;

namespace Lab04_MVC.Data
{
    public class Lab04_MVCContext : DbContext
    {
        public Lab04_MVCContext (DbContextOptions<Lab04_MVCContext> options)
            : base(options)
        {
        }

        public DbSet<Lab04_MVC.Models.Movie> Movie { get; set; } = default!;
    }
}
