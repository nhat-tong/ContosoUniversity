using AutoMapper;
using ContosoUniversity.Models;
using Data.Domain;

namespace ContosoUniversity.AutoMapper
{
    public class DepartmentProfile : Profile
    {
        public override string ProfileName { get { return "DepartmentProfile"; } }

        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentViewModel>();
            CreateMap<DepartmentViewModel, Department>();
        }
    }
}