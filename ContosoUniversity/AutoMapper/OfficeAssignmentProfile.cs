using AutoMapper;
using ContosoUniversity.Models;
using Data.Domain;

namespace ContosoUniversity.AutoMapper
{
    public class OfficeAssignmentProfile : Profile
    {
        public override string ProfileName { get { return "OfficeAssignmentProfile" ; } }

        public OfficeAssignmentProfile()
        {
            CreateMap<OfficeAssignment, OfficeAssignmentViewModel>();
            CreateMap<OfficeAssignmentViewModel, OfficeAssignment>();
        }
    }
}