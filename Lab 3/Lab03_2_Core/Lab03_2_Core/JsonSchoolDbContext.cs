using Lab03_2_Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


namespace Lab03_2_Core
{
    public class JsonSchoolDbContext : DbContext
    {

        IConfiguration _appConfig;

        public JsonSchoolDbContext(IConfiguration config)
        {
            _appConfig = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            optionsBuilder.UseSqlServer(_appConfig.GetConnectionString("SchoolDBLocalConnection"));
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }
    }
}
