using System.Collections.Generic;

namespace ContosoUniversity.Models
{
    public class InstructorDataViewModel
    {
        public IEnumerable<InstructorViewModel> Instructors { get; set; }
        public IEnumerable<CourseViewModel> Courses { get; set; }
        public  IEnumerable<EnrollmentViewModel> Enrollments { get; set; }
    }
}