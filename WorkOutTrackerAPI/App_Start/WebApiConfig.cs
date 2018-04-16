using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace WorkOutTrackerAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //XmlConfigurator.Configure();
            config.MapHttpAttributeRoutes();
            config.EnableCors();
            config.Routes.MapHttpRoute(
     name: "DefaultApi",
     routeTemplate: "api/{controller}/{action}/{moduleId}",
     defaults: new { moduleId = RouteParameter.Optional }
            );
        }
    }
}
