using AutoMapper;
using ContosoUniversity.Models;
using Data.Domain;

namespace ContosoUniversity.AutoMapper
{
    public class EnrollmentProfile : Profile
    {
        public override string ProfileName { get { return "EnrollmentProfile"; } }

        public EnrollmentProfile()
        {
            CreateMap<Enrollment, EnrollmentViewModel>();
            CreateMap<EnrollmentViewModel, Enrollment>();
        }
    }
}