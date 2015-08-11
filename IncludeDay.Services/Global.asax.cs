using System.Data.Entity;
using System.Web;
using System.Web.Http;
using IncludeDay.Data;
using Newtonsoft.Json;

namespace IncludeDay.Services
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            JsonSerializerSettings jSettings = new Newtonsoft.Json.JsonSerializerSettings();
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = jSettings;

            GlobalConfiguration.Configure(WebApiConfig.Register);
            Database.SetInitializer<IncludeDayContext>(null);
        }
    }
}
