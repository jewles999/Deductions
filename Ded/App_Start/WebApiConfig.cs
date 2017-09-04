using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DeductionsAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            var corsAttr = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(corsAttr);
        }
    }
}
