using Lab03_3_Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab03_3_Core.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte[] Photo { get; set; }
        public decimal Height { get; set; }
        public float Weight { get; set; }



        public int GradeId { get; set; }
        public Grade Grade { get; set; }
    }
}
