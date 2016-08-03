using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContosoUniversity.Models;

namespace ContosoUniversity.Controllers
{
    public class CourseController : BaseController
    {
        // GET: Course
        public ActionResult Index()
        {
            var courses = DbContext.Courses.Include(c => c.Department);
            return View(courses.ProjectTo<CourseViewModel>().ToList());
        }
    }
}