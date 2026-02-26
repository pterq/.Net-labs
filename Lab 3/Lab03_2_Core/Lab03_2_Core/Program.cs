using Lab03_2_Core;
using Lab03_2_Core.Models;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;

public class Program
{
    static void Main(string[] args)
    {

        //Db context with json

        var configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .Build();

        //appsetings.json visibilty test
        //Console.WriteLine(configuration.GetConnectionString("SchoolDBLocalConnection"));

        using (var context = new JsonSchoolDbContext(configuration))
        {
            //creates db if not exists 
            context.Database.EnsureCreated();

            //create entity objects
            var grd1 = new Grade() { GradeName = "1st Grade" };
            var std1 = new Student() { FirstName = "Json", LastName = "Derulo", Grade = grd1 };

            //add entity to the context
            context.Students.Add(std1);

            //save data to the database tables
            context.SaveChanges();

            //retrieve all the students from the database
            foreach (var s in context.Students)
            {
                Console.WriteLine($"First Name: {s.FirstName}, Last Name: {s.LastName}");
            }

            //context.Database.EnsureDeleted();
        }




        //=================================================================
        using (var context = new SchoolDbContext())
        {
            //creates db if not exists 
            context.Database.EnsureCreated();

            //create entity objects
            var grd1 = new Grade() { GradeName = "1st Grade" };
            var std1 = new Student() { FirstName = "Yash", LastName = "Malhotra", Grade = grd1 };

            //add entity to the context
            context.Students.Add(std1);

            //save data to the database tables
            context.SaveChanges();

            //retrieve all the students from the database
            foreach (var s in context.Students)
            {
                Console.WriteLine($"First Name: {s.FirstName}, Last Name: {s.LastName}");
            }

            //context.Database.EnsureDeleted();
        }


        // state tracking
        //================================================================
        //status unchanged
        using (var context1 = new SchoolDbContext())
        {

            // retrieve entity 
            var student = context1.Students.FirstOrDefault();
            DisplayStates(context1.ChangeTracker.Entries());
        }

        //display function for state
        static void DisplayStates(IEnumerable<EntityEntry> entries)
        {
            foreach (var entry in entries)
            {
                Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State.ToString()} ");
            }
        }

        //added state
        using (var context = new SchoolDbContext())
        {
            context.Students.Add(new Student() { FirstName = "Bill", LastName = "Gates" });

            DisplayStates(context.ChangeTracker.Entries());
        }

        //modified state
        using (var context = new SchoolDbContext())
        {
            var student = context.Students.FirstOrDefault();
            student.LastName = "Friss";

            DisplayStates(context.ChangeTracker.Entries());
        }

        //deleted state
        using (var context = new SchoolDbContext())
        {
            var student = context.Students.FirstOrDefault();
            context.Students.Remove(student);

            DisplayStates(context.ChangeTracker.Entries());
        }

        //detached state
        var disconnectedEntity = new Student() { StudentId = 1, FirstName = "Bill"};
        using (var context = new SchoolDbContext())
        {
            Console.Write(context.Entry(disconnectedEntity).State);
            Console.WriteLine("\n");
        }


        //delete database
        using (var context = new SchoolDbContext())
        {
            context.Database.EnsureDeleted();
            Console.WriteLine("Database deleted");
        }

            Console.WriteLine("Naciśnij Enter, aby zakończyć...");
        Console.ReadLine();
    }
    
}


