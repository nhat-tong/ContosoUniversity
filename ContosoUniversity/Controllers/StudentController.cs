using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public class StudentController : Controller
    {
        private ContosoDbContext _dbContext;

        public StudentController()
        {
            _dbContext = new ContosoDbContext();
        }

        // GET: Student
        public ActionResult Index()
        {
            return View(_dbContext.Students.ToList());
        }
    }
}