using System;
using System.Collections.Generic;

namespace Data.Domain
{
    public class Course
    {
        public Guid Id { get; set; }
        public int DepartmentId { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual Department Department { get; set; }
    }
}
