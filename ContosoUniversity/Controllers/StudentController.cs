#region using
using Data;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Data.Domain;
using System.Net;
using System;
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

        // GET: Student
        public ActionResult Index()
        {
            return View(_dbContext.Students.ToList());
        }

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
            catch (DataException /* dex */)
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
            catch (DataException /* dex */)
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
            catch (DataException/* dex */)
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