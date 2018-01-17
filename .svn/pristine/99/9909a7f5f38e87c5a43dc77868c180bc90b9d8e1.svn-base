using System.Web.Http;
using System.Web.Http.Cors;

namespace DMSApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var enableCorsAttribute = new EnableCorsAttribute("http://localhost:7110", "*", "*");
            //var enableCorsAttribute = new EnableCorsAttribute("http://59.152.97.62:8010", "*", "*");
            //var enableCorsAttribute = new EnableCorsAttribute("http://45.64.135.140:7110", "*", "*");
            config.EnableCors(enableCorsAttribute);

            config.Routes.MapHttpRoute(
                name: "ControllersWithAction",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}