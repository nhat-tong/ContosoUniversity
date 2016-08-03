using System.Data.Entity;
using Data.Domain;
using Data.Mapping;

namespace Data
{
    [DbConfigurationType(typeof(ContosoConfiguration))]
    public class ContosoDbContext : DbContext
    {
        #region Constructor
        public ContosoDbContext() : base("Contoso")
        {
            // Disable Lazy Loading
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public ContosoDbContext(string connectionString) : base(connectionString)
        {
        }
        #endregion

        #region Initializer
        /// <summary>
        /// Initializer Database for the first time
        /// </summary>
        static ContosoDbContext()
        {
            // Disable Database Initializer
            Database.SetInitializer<ContosoDbContext>(null);
        }
        #endregion

        #region EntitySet
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<Department> Departments { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CourseMap());
            modelBuilder.Configurations.Add(new StudentMap());
            modelBuilder.Configurations.Add(new EnrollmentMap());
            modelBuilder.Configurations.Add(new InstructorMap());
            modelBuilder.Configurations.Add(new OfficeAssignmentMap());
            modelBuilder.Configurations.Add(new DepartmentMap());
        }
    }
}
