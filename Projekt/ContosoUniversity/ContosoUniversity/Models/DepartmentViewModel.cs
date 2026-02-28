using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models.ViewModels
{
    public class DepartmentViewModel
    {
        // Właściwości Department
        public int DepartmentID { get; set; }
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal Budget { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        public int? InstructorID { get; set; }

        // Właściwości Administratora - wszystkie nullable
        public int? AdministratorID { get; set; }
        public string? AdministratorFullName { get; set; } 
        public DateTime? AdministratorHireDate { get; set; }
    }
}