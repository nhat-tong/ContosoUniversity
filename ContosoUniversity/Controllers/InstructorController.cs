using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.Models;
using AutoMapper.QueryableExtensions;
using System.Data.Entity;

namespace ContosoUniversity.Controllers
{
    public class InstructorController : BaseController
    {
        public ActionResult Index(int? id, Guid? courseId)
        {
            var viewModel = new InstructorDataViewModel();
            viewModel.Instructors = DbContext.Instructors
                                    .OrderBy(i => i.LastName)
                                    .ProjectTo<InstructorViewModel>() // AutoMapper will be automatically use Include method to load related data while mapping
                                    .ToList();

            if (id != null)
            {
                ViewBag.InstructorID = id.Value;
                viewModel.Courses = viewModel.Instructors.Single(i => i.Id == id.Value).Courses;
            }

            if (courseId != null)
            {
                ViewBag.CourseID = courseId.Value;
                viewModel.Enrollments = viewModel.Courses.Single(x => x.Id == courseId).Enrollments;
                // Explicit loading Enrollments
                // DbContext.Entry(course).Collection(x => x.Enrollments).Load();
                // DbContext.Entry(enrollment).Reference(x => x.Student).Load();
            }

            return View(viewModel);
        }
    }
}