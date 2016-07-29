using System;

namespace Data.Domain
{
    public enum Grade
    {
        A,
        B,
        C,
        D,
        F
    }
    public class Enrollment
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public Guid StudentId { get; set; }
        public Grade? Grade { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}
