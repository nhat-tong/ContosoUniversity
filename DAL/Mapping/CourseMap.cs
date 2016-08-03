using System.Data.Entity.ModelConfiguration;
using Data.Domain;

namespace Data.Mapping
{
    public class CourseMap : EntityTypeConfiguration<Course>
    {
        public CourseMap()
        {
            // Primary Key
            HasKey(c => c.Id);

            // Properties
            Property(c => c.Title).IsMaxLength().IsRequired();
            Property(c => c.Credits).IsRequired();

            // Table & Columns Mapping
            ToTable("Course");
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.Title).HasColumnName("Title");
            Property(c => c.Credits).HasColumnName("Credits");

            // One-to-Many relationship between Course and Department
            HasRequired(c => c.Department)
                .WithMany(d => d.Courses)
                .HasForeignKey(c => c.DepartmentId);

            // Customization many-to-many relationship between Course and Instructor
            HasMany(c => c.Instructors)
                .WithMany(ins => ins.Courses)
                .Map(t => t.MapLeftKey("CourseId")
                           .MapRightKey("InstructorId")
                           .ToTable("CourseInstructor"));
        }
    }
}
