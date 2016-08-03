using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Data.Domain;

namespace Data.Mapping
{
    public class DepartmentMap : EntityTypeConfiguration<Department>
    {
        public DepartmentMap()
        {
            // Primary Key
            HasKey(d => d.Id);
            Property(d => d.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.Name).HasMaxLength(50);
            Property(d => d.Budget).HasColumnType("money");

            ToTable("Department");

            // Zero-to-Many relationship between Instructor and Department (because of InstructorId is nullable)
            HasOptional(d => d.Administrator)
                .WithMany()
                .HasForeignKey(d => d.InstructorId);
        }
    }
}
