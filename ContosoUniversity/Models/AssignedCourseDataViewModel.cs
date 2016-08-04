using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class AssignedCourseDataViewModel
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public bool Assigned { get; set; }
    }
}