using Lab03_4_Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab03_4_Core.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int GradeId { get; set; }
        public Grade Grade { get; set; }
    }
}
