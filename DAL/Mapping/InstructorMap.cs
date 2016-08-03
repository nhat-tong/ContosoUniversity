using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Data.Domain;

namespace Data.Mapping
{
    public class InstructorMap : EntityTypeConfiguration<Instructor>
    {
        public InstructorMap()
        {
            // Primary Key
            HasKey(ins => ins.Id);
            Property(ins => ins.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Properties
            Property(ins => ins.LastName).HasMaxLength(50);
            Property(ins => ins.FirstName).HasMaxLength(50);

            // Table & Columns Mapping
            ToTable("Instructor");

            // Zero-to-One relationship between Instructor and OfficeAssignment
            HasOptional(ins => ins.OfficeAssignment)
                .WithRequired(off => off.Instructor);
        }
    }
}
