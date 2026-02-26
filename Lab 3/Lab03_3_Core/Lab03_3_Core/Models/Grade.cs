using System;
using System.Collections.Generic;
using System.Text;

namespace Lab03_3_Core.Models
{
    public class Grade
    {
        public Grade()
        {
            Students = new List<Student>();
        }

        public int GradeId { get; set; }
        public string GradeName { get; set; }

        public IList<Student> Students { get; set; }
    }
}
