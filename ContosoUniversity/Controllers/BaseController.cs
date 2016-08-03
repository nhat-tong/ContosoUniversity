using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Data;

namespace ContosoUniversity.Controllers
{
    public class BaseController : Controller
    {
        protected ContosoDbContext DbContext;
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public BaseController()
        {
            DbContext = new ContosoDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            // Release DbContext memory
            DbContext.Dispose();
            base.Dispose(disposing);
        }
    }
}