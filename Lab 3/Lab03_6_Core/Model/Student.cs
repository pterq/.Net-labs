using System;
using System.Collections.Generic;

namespace Lab03_6_Core.Model;

public partial class Student
{
    public int StudentId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int GradeId { get; set; }

    public virtual Grade Grade { get; set; } = null!;
}
