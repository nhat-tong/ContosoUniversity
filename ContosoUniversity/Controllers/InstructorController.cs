using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.Models;
using AutoMapper.QueryableExtensions;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;
using AutoMapper;
using Data.Domain;

namespace ContosoUniversity.Controllers
{
    public class InstructorController : BaseController
    {
        #region Index        
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
        #endregion

        #region Edit (Get)
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var instructor = DbContext.Instructors
               .Include(i => i.OfficeAssignment)
               .Include(i => i.Courses)
               .Single(i => i.Id == id);

            var vm = Mapper.Map<InstructorViewModel>(instructor);
            PopulateAssignedCourseData(instructor);

            if (vm == null)
            {
                return HttpNotFound();
            }

            return View(vm);
        }
        #endregion


        #region Edit (Post)
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LastName,FirstName,HireDate,OfficeAssignment")]InstructorViewModel vm)
        {
            if (vm.Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var oldInstructor = DbContext.Instructors
               .Include(i => i.OfficeAssignment)
               .Single(i => i.Id == vm.Id);

            var newInstructor = Mapper.Map<Instructor>(vm);

            try
            {
                if (string.IsNullOrWhiteSpace(newInstructor.OfficeAssignment.Location))
                {
                    newInstructor.OfficeAssignment = null;
                }

                DbContext.Entry(oldInstructor).CurrentValues.SetValues(newInstructor);
                DbContext.Entry(oldInstructor).State = EntityState.Modified;

                DbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (RetryLimitExceededException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return View(newInstructor);
        }
        */

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id, string[] selectedCourses)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var instructorToUpdate = DbContext.Instructors
              .Include(i => i.OfficeAssignment)
              .Include(i => i.Courses)
              .Single(i => i.Id == id);

            // TryUpdateModel: model binder from FormCollection to instructorToUpdate entity
            if (TryUpdateModel(instructorToUpdate, "", new string[] { "LastName", "FirstName", "HireDate", "OfficeAssignment" }))
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(instructorToUpdate.OfficeAssignment.Location))
                    {
                        instructorToUpdate.OfficeAssignment = null;
                    }

                    UpdateInstructorCourses(selectedCourses, instructorToUpdate);

                    DbContext.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            PopulateAssignedCourseData(instructorToUpdate);
            return View(instructorToUpdate);
        }
        #endregion

        #region Course
        private void PopulateAssignedCourseData(Instructor instructor)
        {
            var allCourses = DbContext.Courses;
            var instructorCourses = new HashSet<Guid>(instructor.Courses.Select(c => c.Id));
            var viewModel = new List<AssignedCourseDataViewModel>();
            foreach (var course in allCourses)
            {
                viewModel.Add(new AssignedCourseDataViewModel
                {
                    CourseId = course.Id,
                    Title = course.Title,
                    Assigned = instructorCourses.Contains(course.Id)
                });
            }
            ViewBag.Courses = viewModel;
        }

        private void UpdateInstructorCourses(string[] selectedCourses, Instructor instructorToUpdate)
        {
            if (selectedCourses == null)
            {
                instructorToUpdate.Courses = new List<Course>();
                return;
            }

            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var instructorCourses = new HashSet<Guid>(instructorToUpdate.Courses.Select(c => c.Id));

            foreach (var course in DbContext.Courses)
            {
                if (selectedCoursesHS.Contains(course.Id.ToString()))
                {
                    if (!instructorCourses.Contains(course.Id))
                    {
                        instructorToUpdate.Courses.Add(course);
                    }
                }
                else
                {
                    if (instructorCourses.Contains(course.Id))
                    {
                        instructorToUpdate.Courses.Remove(course);
                    }
                }
            }
        }
        #endregion
    }
}