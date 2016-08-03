#region using
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using ContosoUniversity.AutoMapper;
#endregion

namespace ContosoUniversity
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            // Configure AutoMapper
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<StudentProfile>();
                cfg.AddProfile<DepartmentProfile>();
                cfg.AddProfile<OfficeAssignmentProfile>();
                cfg.AddProfile<EnrollmentProfile>();
                cfg.AddProfile<CourseProfile>();
                cfg.AddProfile<InstructorProfile>();
            });
        }
    }
}
