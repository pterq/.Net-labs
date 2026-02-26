using Lab03_5_Core;
using Lab03_5_Core.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


public class Program
{
    static void Main(string[] args)
    {
        var context = new SchoolDbContext();

        //context.Database.EnsureCreated();

        //var grd1 = new Grade() { GradeName = "1st Grade" };
        //var std1 = new Student() { FirstName = "Bill", LastName = "Kimono", Grade = grd1 };

        //context.Students.Add(std1);
        //context.SaveChanges();
        //context.Database.EnsureDeleted();

        //raw sql
        var students = context.Students.FromSql($"SELECT * FROM Students WHERE FirstName = 'Bill'").ToList();


        //Parameterized Query
        string name = "Bill";

        var context1 = new SchoolDbContext();
        var students1 = context1.Students
                            .FromSql($"SELECT * FROM Students WHERE FirstName = '{name}'")
                            .ToList();


        //LINQ Operators
        name = "Bill";

        var context2 = new SchoolDbContext();
        var students2 = context2.Students
                            .FromSqlRaw("SELECT * FROM Students WHERE FirstName = @name",
                            new SqlParameter("@name", name))
                            .OrderBy(s => s.StudentId)
                            .ToList();


    }
}