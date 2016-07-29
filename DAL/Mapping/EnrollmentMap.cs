using System.Data.Entity.ModelConfiguration;
using Data.Domain;

namespace Data.Mapping
{
    public class EnrollmentMap : EntityTypeConfiguration<Enrollment>
    {
        public EnrollmentMap()
        {
            // Primary Key
            HasKey(e => e.Id);

            // Properties
            Property(e => e.StudentId).IsRequired();
            Property(e => e.CourseId).IsRequired();
            Property(e => e.Grade).IsOptional();

            // Table & Columns Mapping
            ToTable("Enrollment");
            Property(e => e.Id).HasColumnName("Id");
            Property(e => e.StudentId).HasColumnName("StudentId");
            Property(e => e.CourseId).HasColumnName("CourseId");
            Property(e => e.Grade).HasColumnName("Grade");

            // Relationships
            HasRequired(e => e.Course).WithMany(c => c.Enrollments).HasForeignKey(e => e.CourseId);
            HasRequired(e => e.Student).WithMany(s => s.Enrollments).HasForeignKey(e => e.StudentId);
        }
    }
}
