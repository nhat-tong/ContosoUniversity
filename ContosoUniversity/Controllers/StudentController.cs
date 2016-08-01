#region using
using Data;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Data.Domain;
using System.Net;
using System;
using System.Data.Entity.Infrastructure;
using X.PagedList;
#endregion

namespace ContosoUniversity.Controllers
{
    public class StudentController : Controller
    {
        private ContosoDbContext _dbContext;

        public StudentController()
        {
            _dbContext = new ContosoDbContext();
        }

        #region Index
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            // Ref: server pagins using X.PagedList.Mvc: https://github.com/kpi-ua/X.PagedList
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var students = from s in _dbContext.Students
                           select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s =>
                s.LastName.ToUpper().Contains(searchString.ToUpper())
                ||
                s.FirstName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default: // Name ascending
                    students = students.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
        }
        #endregion

        #region Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Student());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "Id")]Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Students.Add(student);
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line
                //here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(student);
        }
        #endregion

        #region Edit
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            return View(_dbContext.Students.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, LastName, FirstName, EnrollmentDate")]Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Entry(student).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to
                //write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(student);
        }
        #endregion

        #region Delete
        public ActionResult Delete(Guid? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Student student = _dbContext.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            // Read: http://stephenwalther.com/blog/archive/2009/01/21/asp.net-mvc-tip-46-ndash-donrsquot-use-delete-links-because.aspx
            try
            {
                //Student student = _dbContext.Students.Find(id);
                //_dbContext.Students.Remove(student);

                var student = new Student { Id = id };
                _dbContext.Entry(student).State = EntityState.Deleted;

                _dbContext.SaveChanges();
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line
                //here to write a log.
                return RedirectToAction("Delete", new
                {
                    id = id,
                    saveChangesError = true
                });
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
            base.Dispose(disposing);
        }
        #endregion
    }
}