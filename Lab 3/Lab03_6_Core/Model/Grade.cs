using System;
using System.Collections.Generic;

namespace Lab03_6_Core.Model;

public partial class Grade
{
    public int GradeId { get; set; }

    public string GradeName { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
