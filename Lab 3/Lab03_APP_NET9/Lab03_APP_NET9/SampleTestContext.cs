using Lab03_APP_NET9.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab03_APP_NET9
{
    public class SampleTestContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SampleTestEFDB;Trusted_Connection=True;");
        }

        public DbSet<Person> People { get; set; }
    }
}
