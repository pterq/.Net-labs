using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;

namespace ContosoUniversity.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly SchoolContext _context;

        public InstructorsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Instructors
        public async Task<IActionResult> Index(int? id, int? courseID)
        {
            var viewModel = new InstructorIndexData();
            viewModel.Instructors = await _context.Instructors
                  .Include(i => i.OfficeAssignment)
                  .Include(i => i.CourseAssignments)
                    .ThenInclude(i => i.Course)
                        .ThenInclude(i => i.Enrollments)
                            .ThenInclude(i => i.Student)
                  .Include(i => i.CourseAssignments)
                    .ThenInclude(i => i.Course)
                        .ThenInclude(i => i.Department)
                  .AsNoTracking()
                  .OrderBy(i => i.LastName)
                  .ToListAsync();

            if (id != null)
            {
                ViewData["InstructorID"] = id.Value;
                Instructor instructor = viewModel.Instructors.Where(
                    i => i.ID == id.Value).Single();
                viewModel.Courses = instructor.CourseAssignments.Select(s => s.Course);
            }

            if (courseID != null)
            {
                ViewData["CourseID"] = courseID.Value;
                var selectedCourse = viewModel.Courses.Where(x => x.CourseID == courseID).Single();
                await _context.Entry(selectedCourse).Collection(x => x.Enrollments).LoadAsync();
                foreach (Enrollment enrollment in selectedCourse.Enrollments)
                {
                    await _context.Entry(enrollment).Reference(x => x.Student).LoadAsync();
                }
                viewModel.Enrollments = selectedCourse.Enrollments;
            }

            return View(viewModel);
        }

        // GET: Instructors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors
                .FirstOrDefaultAsync(m => m.ID == id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // GET: Instructors/Create
        public IActionResult Create()
        {
            var instructor = new Instructor();
            instructor.CourseAssignments = new List<CourseAssignment>();
            PopulateAssignedCourseData(instructor);
            return View();
        }

        // POST: Instructors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstMidName,HireDate,LastName")] Instructor instructor,
            [Bind(Prefix = "OfficeAssignment.Location")] string officeLocation, 
            string[] selectedCourses)
        {
            // Usuń błędy walidacji dla właściwości nawigacyjnych
            ModelState.Remove("OfficeAssignment");
            ModelState.Remove("CourseAssignments");
            ModelState.Remove("FullName");
            ModelState.Remove("OfficeAssignment.Location");

            if (ModelState.IsValid)
            {
                try
                {
                    //zapisanie noweg instruktora żeby dostał ID
                    _context.Add(instructor);
                    await _context.SaveChangesAsync();

                    // dodaj OfficeAssignment jeśli podano lokalizację
                    if (!string.IsNullOrWhiteSpace(officeLocation))
                    {
                        var officeAssignment = new OfficeAssignment
                        {
                            InstructorID = instructor.ID,
                            Location = officeLocation
                        };
                        _context.OfficeAssignments.Add(officeAssignment);
                    }

                    // dodanie CourseAssignments
                    if (selectedCourses != null)
                    {
                        instructor.CourseAssignments = new List<CourseAssignment>();
                        foreach (var course in selectedCourses)
                        {
                            if (int.TryParse(course, out int courseId))
                            {
                                instructor.CourseAssignments.Add(new CourseAssignment
                                {
                                    InstructorID = instructor.ID,
                                    CourseID = courseId
                                });
                            }
                        }
                        
                    }
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator. " + ex.Message);
                }
            }
            
            PopulateAssignedCourseData(instructor);
            return View(instructor);
        }

        // GET: Instructors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.CourseAssignments).ThenInclude(i => i.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (instructor == null)
            {
                return NotFound();
            }
            PopulateAssignedCourseData(instructor);
            return View(instructor);
        }

        private void PopulateAssignedCourseData(Instructor instructor)
        {
            var allCourses = _context.Courses.ToList();

            
            var instructorCourses = new HashSet<int>();
            if (instructor.CourseAssignments != null)
            {
                instructorCourses = new HashSet<int>(instructor.CourseAssignments.Select(c => c.CourseID));
            }

            var viewModel = new List<AssignedCourseData>();
            foreach (var course in allCourses)
            {
                viewModel.Add(new AssignedCourseData
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    Assigned = instructorCourses.Contains(course.CourseID)
                });
            }
            ViewBag.Courses = viewModel;
        }


        // POST: Instructors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            // sprawdzenie co przychodzi w formularzu
            var form = Request.Form;


            ModelState.Remove("OfficeAssignment.Location");

            // 1. Znajdź instruktora
            var instructorToUpdate = await _context.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.CourseAssignments)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (instructorToUpdate == null)
            {
                return NotFound();
            }
        
            instructorToUpdate.LastName = form["LastName"];
            instructorToUpdate.FirstMidName = form["FirstMidName"];

            if (DateTime.TryParse(form["HireDate"], out DateTime hireDate))
            {
                instructorToUpdate.HireDate = hireDate;
            }

            // osbługa office asignment
            var officeLocation = form["OfficeAssignment.Location"].ToString();
            //Console.WriteLine($"Office location from form: '{officeLocation}'");

            if (string.IsNullOrWhiteSpace(officeLocation))
            {
                if (instructorToUpdate.OfficeAssignment != null)
                {
                    //Console.WriteLine("Removing office assignment");
                    _context.OfficeAssignments.Remove(instructorToUpdate.OfficeAssignment);
                    instructorToUpdate.OfficeAssignment = null;
                }
            }
            else
            {
                if (instructorToUpdate.OfficeAssignment == null)
                {
                    //Console.WriteLine($"Adding new office assignment: {officeLocation}");
                    instructorToUpdate.OfficeAssignment = new OfficeAssignment
                    {
                        Location = officeLocation,
                        InstructorID = instructorToUpdate.ID
                    };
                }
                else
                {
                    //Console.WriteLine($"Updating office assignment to: {officeLocation}");
                    instructorToUpdate.OfficeAssignment.Location = officeLocation;
                }
            }

            // obsługa kursów
            var selectedCourses = form["selectedCourses"].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries);
            //Console.WriteLine($"Selected courses: {string.Join(", ", selectedCourses)}");

            // usuwanie wszystkich istniejących przypisan
            if (instructorToUpdate.CourseAssignments != null)
            {
                var assignmentsToRemove = instructorToUpdate.CourseAssignments.ToList();
                foreach (var assignment in assignmentsToRemove)
                {
                    _context.CourseAssignments.Remove(assignment);
                }
            }
            instructorToUpdate.CourseAssignments = new List<CourseAssignment>();

            // dodanie nowych przypisań
            foreach (var courseId in selectedCourses)
            {
                if (int.TryParse(courseId, out int cid))
                {
                    //Console.WriteLine($"Adding course assignment: {cid}");
                    instructorToUpdate.CourseAssignments.Add(new CourseAssignment
                    {
                        InstructorID = instructorToUpdate.ID,
                        CourseID = cid
                    });
                }
            }
            
            try
            {
                var changes = await _context.SaveChangesAsync();
                //Console.WriteLine($"Saved {changes} changes");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error: {ex.Message}");
                ModelState.AddModelError("", "Unable to save changes. " + ex.Message);
            }
            
            PopulateAssignedCourseData(instructorToUpdate);
            return View(instructorToUpdate);
        }

        
        // GET: Instructors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors
                .FirstOrDefaultAsync(m => m.ID == id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Instructor instructor = await _context.Instructors
                .Include(i => i.CourseAssignments)
                .FirstOrDefaultAsync(i => i.ID == id);

            if (instructor == null)
            {
                return NotFound();
            }

            var departments = await _context.Departments
                .Where(d => d.InstructorID == id)
                .ToListAsync();

            foreach (var department in departments)
            {
                department.InstructorID = null;
            }

            // Alternatywna metoda usuwania
            _context.Entry(instructor).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstructorExists(int id)
        {
            return _context.Instructors.Any(e => e.ID == id);
        }
    }
}
