using AutoMapper;
using ContosoUniversity.Models;
using Data.Domain;

namespace ContosoUniversity.AutoMapper
{
    public class StudentProfile : Profile
    {
        public override string ProfileName { get { return "StudentProfile"; } }

        public StudentProfile()
        {
            CreateMap<Student, StudentViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.EnrollmentDate, opt => opt.MapFrom(src => src.EnrollmentDate));

            CreateMap<StudentViewModel, Student>();
        }
    }
}