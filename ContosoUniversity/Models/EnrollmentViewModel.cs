using System;
using Data.Domain;

namespace ContosoUniversity.Models
{
    public class EnrollmentViewModel
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public Guid StudentId { get; set; }
        public Grade? Grade { get; set; }

        public StudentViewModel Student { get; set; }
    }
}