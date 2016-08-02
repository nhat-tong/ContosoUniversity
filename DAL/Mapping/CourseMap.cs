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

            // Relationships
            HasRequired(c => c.Department)
                .WithMany(d => d.Courses)
                .HasForeignKey(c => c.DepartmentId);
        }
    }
}
