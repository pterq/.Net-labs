using Microsoft.EntityFrameworkCore;
using Lab03_2_Core.Models;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Lab03_2_Core
{
    public class SchoolContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder); //placeholder
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SchoolDb;Trusted_Connection=True;");

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }
    }
}
