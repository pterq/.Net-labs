using Lab03_4_Core;
using Lab03_4_Core.Models;

public class Program
{
    static void Main(string[] args)
    {
        //Insert Data
        using (var context = new SchoolDbContext())
        {
            var std = new Student()
            {
                FirstName = "Bill",
                LastName = "Gates"
            };
            context.Students.Add(std);

            // or
            // context.Add<Student>(std);

            context.SaveChanges();
        }


        //Updating Data
        using (var context = new SchoolDbContext())
        {
            var std = context.Students.First<Student>();
            std.FirstName = "Steve";
            context.SaveChanges();
        }


        //Deleting Data
        using (var context = new SchoolDbContext())
        {
            var std = context.Students.First<Student>();
            context.Students.Remove(std);

            // or
            // context.Remove<Student>(std);

            context.SaveChanges();
        }
    }
}