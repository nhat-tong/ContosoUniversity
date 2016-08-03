using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Domain;

namespace Data.Mapping
{
    public class OfficeAssignmentMap : EntityTypeConfiguration<OfficeAssignment>
    {
        public OfficeAssignmentMap()
        {
            // Primary Key
            HasKey(off => off.InstructorId);
            Property(off => off.Location).HasMaxLength(50);

            ToTable("OfficeAssignment");           
        }
    }
}
