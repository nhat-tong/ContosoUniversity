using System;

namespace Data.Domain
{
    public class OfficeAssignment
    {
        public int InstructorId { get; set; }
        public string Location { get; set; }

        public virtual Instructor Instructor { get; set; }
    }
}
