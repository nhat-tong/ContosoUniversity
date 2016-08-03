using AutoMapper;
using ContosoUniversity.Models;
using Data.Domain;

namespace ContosoUniversity.AutoMapper
{
    public class InstructorProfile : Profile
    {
        public override string ProfileName { get { return "InstructorProfile"; } }

        public InstructorProfile()
        {
            CreateMap<Instructor, InstructorViewModel>();
            CreateMap<InstructorViewModel, Instructor>();
        }
    }
}