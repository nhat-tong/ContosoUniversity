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
            Database.SetInitializer<ContosoDbContext>(null);
        }
        #endregion

        #region EntitySet
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CourseMap());
            modelBuilder.Configurations.Add(new StudentMap());
            modelBuilder.Configurations.Add(new EnrollmentMap());
        }
    }
}
