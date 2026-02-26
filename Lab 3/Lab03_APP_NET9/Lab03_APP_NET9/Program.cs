
using Lab03_APP_NET9;
using Lab03_APP_NET9.Models;
using Microsoft.EntityFrameworkCore;

using System;
using System.Data;
using System.Data.SqlClient;



public class Program
{
    static void Main(string[] args)
    {
        using (var context = new SampleTestContext())
        {
            Console.Write("Dropping and creating database 'SampleTestEFDB' ... ");
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            Console.WriteLine("Done \n");
        }

        while (true)
        {
            Console.WriteLine("Choose an action:");
            Console.WriteLine(" 1 - Inserting a new row into table...");
            Console.WriteLine(" 2 - Updating rows into table...");
            Console.WriteLine(" 3 - Deleting rows into table...");
            Console.WriteLine(" 4 - Reading data from table...");
            Console.WriteLine(" 5 - Exit");

            Console.WriteLine("Option: ");
            var choice = Console.ReadLine();

            switch(choice)
            {
                case "1":
                    InsertRow();
                    break;
                case "2":
                    UpdateRow();
                    break;
                case "3":
                    DeleteRow();
                    break;
                case "4":
                    ReadData();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;

            }
            Console.WriteLine("--------------------------------------------------------");
        }

        
    }


    static void InsertRow()
    {
        Console.Write("Enter First Name: ");
        string firstName = Console.ReadLine();

        Console.Write("Enter Last Name: ");
        string lastName = Console.ReadLine();

        Console.Write("Enter Age: ");
        if (!int.TryParse(Console.ReadLine(), out int age))
        {
            Console.WriteLine("Invalid age.");
            return;
        }

        using var context = new SampleTestContext();
        var person = new Person { FirstName = firstName, LastName = lastName, Age = age };
        context.People.Add(person);
        context.SaveChanges();
        Console.WriteLine("Row inserted.");

    }

    static void UpdateRow()
    {
        ReadData();
        using var context = new SampleTestContext();

        Console.Write("Enter Id to update: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid Id.");
            return;
        }
        var person = context.People.Find(id);
        if (person == null)
        {
            Console.WriteLine("Person not found.");
            return;
        }

        Console.Write("Enter new First Name(0 if no change): ");
        string firstName = Console.ReadLine();

        Console.Write("Enter new Last Name(0 if no change): ");
        string lastName = Console.ReadLine();

        Console.Write("Enter new Age(0 if no change): ");
        if (!int.TryParse(Console.ReadLine(), out int age))
        {
            Console.WriteLine("Invalid Age.");
            return;
        }

        if (firstName != "0") person.FirstName = firstName;
        if (lastName != "0") person.LastName = lastName;
        if(age != 0) person.Age = age;
        
        context.SaveChanges();

        Console.WriteLine("Row updated.");
        ReadData();
    }

    static void DeleteRow()
    {
        Console.Write("Enter Id to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid Id.");
            return;
        }

        using var context = new SampleTestContext();
        var person = context.People.Find(id);
        if (person == null)
        {
            Console.WriteLine("Person not found.");
            return;
        }

        context.People.Remove(person);
        context.SaveChanges();
        Console.WriteLine("Row deleted.");
    }

    static void ReadData()
    {
        using var context = new SampleTestContext();
        var people = context.People.ToList();
        Console.WriteLine("Id\tFirstName\tLastName\tAge");
        foreach (var p in people)
        {
            Console.WriteLine($"{p.Id}\t{p.FirstName}\t{p.LastName}\t{p.Age}");
        }
    }



}
