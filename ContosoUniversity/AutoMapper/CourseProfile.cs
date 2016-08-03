using AutoMapper;
using ContosoUniversity.Models;
using Data.Domain;

namespace ContosoUniversity.AutoMapper
{
    public class CourseProfile : Profile
    {
        public override string ProfileName { get { return "CourseProfile" ; } }

        public CourseProfile()
        {
            CreateMap<Course, CourseViewModel>();
            CreateMap<CourseViewModel, Course>();
        }
    }
}