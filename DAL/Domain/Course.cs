using System;
using System.Collections.Generic;

namespace Data.Domain
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
