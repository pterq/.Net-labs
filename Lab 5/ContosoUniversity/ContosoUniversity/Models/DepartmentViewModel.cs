namespace ContosoUniversity.Models.ViewModels
{
    public class DepartmentViewModel
    {
        // Właściwości Department
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }
        public int? InstructorID { get; set; }

        // Właściwości Administratora - wszystkie nullable
        public int? AdministratorID { get; set; }
        public string? AdministratorFullName { get; set; } 
        public DateTime? AdministratorHireDate { get; set; }
    }
}