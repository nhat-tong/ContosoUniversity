using System;
using System.Collections.Generic;

namespace Data.Domain
{
    public class Student
    {
        public Guid Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime? EnrollmentDate { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
