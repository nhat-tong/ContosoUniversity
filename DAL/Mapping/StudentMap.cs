using System.Data.Entity.ModelConfiguration;
using Data.Domain;

namespace Data.Mapping
{
    public class StudentMap : EntityTypeConfiguration<Student>
    {
        public StudentMap()
        {
            // Primary key
            HasKey(s => s.Id);

            // Properties
            Property(s => s.FirstName).IsMaxLength().IsRequired();
            Property(s => s.LastName).IsMaxLength().IsRequired();
            Property(s => s.EnrollmentDate).IsOptional();

            // Table & Columns Mapping
            ToTable("Student");
            Property(s => s.Id).HasColumnName("Id");
            Property(s => s.FirstName).HasColumnName("FirstName");
            Property(s => s.LastName).HasColumnName("LastName");
            Property(s => s.EnrollmentDate).HasColumnName("EnrollmentDate");
        }
    }
}
