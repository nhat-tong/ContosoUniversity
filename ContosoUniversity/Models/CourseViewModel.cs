using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class CourseViewModel
    {
        public Guid Id { get; set; }
        public int DepartmentId { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        public DepartmentViewModel Department { get; set; }
        public ICollection<EnrollmentViewModel> Enrollments { get; set; }
    }
}