using Lab03_1_Core;
using Lab03_1_Core.Models;
using Microsoft.EntityFrameworkCore;

public class Program
{
    static void Main(string[] args)
    {
        using (var context = new BookStore())
        {
            context.Database.EnsureCreated();

            var authors = new List<Author>
        {
            new Author
            {
                FirstName ="Carson",
                LastName ="Alexander",
                BirthDate = DateTime.Parse("1985-09-01"),
                Books = new List<Book>()
                {
                    new Book { Title = "Introduction to Machine Learning"},
                    new Book { Title = "Advanced Topics on Machine Learning"},
                    new Book { Title = "Introduction to Computing"}
                }
            },
            new Author
            {
                FirstName ="Meredith",
                LastName ="Alonso",
                BirthDate = DateTime.Parse("1970-09-01"),
                Books = new List<Book>()
                {
                    new Book { Title = "Introduction to Microeconomics"}
                }
            },
            new Author
            {
                FirstName ="Arturo",
                LastName ="Anand",
                BirthDate = DateTime.Parse("1963-09-01"),
                Books = new List<Book>()
                {
                    new Book { Title = "Calculus I"},
                    new Book { Title = "Calculus II"}
                }
            }
        };

            context.Authors.AddRange(authors);
            context.SaveChanges();
        }

        using (var context = new BookStore())
        {
            var list = context.Authors
                .Include(a => a.Books)
                .ToList();

            foreach (var author in list)
            {
                Console.WriteLine(author.FirstName + " " + author.LastName);

                foreach (var book in author.Books)
                {
                    Console.WriteLine("\t" + book.Title);
                }
            }
        }

        Console.WriteLine("Naciśnij Enter, aby zakończyć...");
        Console.ReadLine();
    }
}

