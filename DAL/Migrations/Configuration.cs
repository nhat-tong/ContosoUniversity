using System.Collections.Generic;
using Data.Domain;

namespace Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.ContosoDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data.ContosoDbContext context)
        {
            var students = new List<Student>
            {
                new Student { Id = Guid.NewGuid(), FirstName = "Carson",   LastName = "Alexander",
                    EnrollmentDate = DateTime.Parse("2010-09-01") },
                new Student { Id = Guid.NewGuid(), FirstName = "Meredith", LastName = "Alonso",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { Id = Guid.NewGuid(), FirstName = "Arturo",   LastName = "Anand",
                    EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { Id = Guid.NewGuid(), FirstName = "Gytis",    LastName = "Barzdukas",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { Id = Guid.NewGuid(), FirstName = "Yan",      LastName = "Li",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { Id = Guid.NewGuid(), FirstName = "Peggy",    LastName = "Justice",
                    EnrollmentDate = DateTime.Parse("2011-09-01") },
                new Student { Id = Guid.NewGuid(),FirstName = "Laura",    LastName = "Norman",
                    EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { Id = Guid.NewGuid(), FirstName = "Nino",     LastName = "Olivetto",
                    EnrollmentDate = DateTime.Parse("2005-08-11") }
            };
            students.ForEach(s => context.Students.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var courses = new List<Course>
            {
                new Course {Id = Guid.NewGuid(), Title = "Chemistry",      Credits = 3, },
                new Course {Id = Guid.NewGuid(), Title = "Microeconomics", Credits = 3, },
                new Course {Id = Guid.NewGuid(), Title = "Macroeconomics", Credits = 3, },
                new Course {Id = Guid.NewGuid(), Title = "Calculus",       Credits = 4, },
                new Course {Id = Guid.NewGuid(), Title = "Trigonometry",   Credits = 4, },
                new Course {Id = Guid.NewGuid(), Title = "Composition",    Credits = 3, },
                new Course {Id = Guid.NewGuid(), Title = "Literature",     Credits = 4, }
            };
            courses.ForEach(s => context.Courses.AddOrUpdate(p => p.Title, s));
            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                new Enrollment {
                    Id = Guid.NewGuid(),
                    StudentId = students.Single(s => s.LastName == "Alexander").Id,
                    CourseId = courses.Single(c => c.Title == "Chemistry" ).Id,
                    Grade = Grade.A
                },
                 new Enrollment {
                     Id = Guid.NewGuid(),
                    StudentId = students.Single(s => s.LastName == "Alexander").Id,
                    CourseId = courses.Single(c => c.Title == "Microeconomics" ).Id,
                    Grade = Grade.C
                 },
                 new Enrollment {
                     Id = Guid.NewGuid(),
                    StudentId = students.Single(s => s.LastName == "Alexander").Id,
                    CourseId = courses.Single(c => c.Title == "Macroeconomics" ).Id,
                    Grade = Grade.B
                 },
                 new Enrollment {
                     Id = Guid.NewGuid(),
                     StudentId = students.Single(s => s.LastName == "Alonso").Id,
                    CourseId = courses.Single(c => c.Title == "Calculus" ).Id,
                    Grade = Grade.B
                 },
                 new Enrollment {
                     Id = Guid.NewGuid(),
                     StudentId = students.Single(s => s.LastName == "Alonso").Id,
                    CourseId = courses.Single(c => c.Title == "Trigonometry" ).Id,
                    Grade = Grade.B
                 },
                 new Enrollment {
                     Id = Guid.NewGuid(),
                    StudentId = students.Single(s => s.LastName == "Alonso").Id,
                    CourseId = courses.Single(c => c.Title == "Composition" ).Id,
                    Grade = Grade.B
                 },
                 new Enrollment {
                     Id = Guid.NewGuid(),
                    StudentId = students.Single(s => s.LastName == "Anand").Id,
                    CourseId = courses.Single(c => c.Title == "Chemistry" ).Id
                 },
                 new Enrollment {
                     Id = Guid.NewGuid(),
                    StudentId = students.Single(s => s.LastName == "Anand").Id,
                    CourseId = courses.Single(c => c.Title == "Microeconomics").Id,
                    Grade = Grade.B
                 },
                new Enrollment {
                    Id = Guid.NewGuid(),
                    StudentId = students.Single(s => s.LastName == "Barzdukas").Id,
                    CourseId = courses.Single(c => c.Title == "Chemistry").Id,
                    Grade = Grade.B
                 },
                 new Enrollment {
                     Id = Guid.NewGuid(),
                    StudentId = students.Single(s => s.LastName == "Li").Id,
                    CourseId = courses.Single(c => c.Title == "Composition").Id,
                    Grade = Grade.B
                 },
                 new Enrollment {
                     Id = Guid.NewGuid(),
                    StudentId = students.Single(s => s.LastName == "Justice").Id,
                    CourseId = courses.Single(c => c.Title == "Literature").Id,
                    Grade = Grade.B
                 }
            };

            foreach (Enrollment e in enrollments)
            {
                var enrollmentInDataBase = context.Enrollments.Where(
                    s =>
                         s.Student.Id == e.StudentId &&
                         s.Course.Id == e.CourseId).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    context.Enrollments.Add(e);
                }
            }
            context.SaveChanges();
        }
    }
}
