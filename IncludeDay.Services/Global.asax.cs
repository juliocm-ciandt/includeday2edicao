using System.Data.Entity;
using System.Web;
using System.Web.Http;
using IncludeDay.Data;

namespace IncludeDay.Services
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Database.SetInitializer<IncludeDayContext>(null);
        }
    }
}
