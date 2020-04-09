using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Service {

    public static class WebApiConfig {

        public static void Register(HttpConfiguration config) {
            // Web API configuration and services

            config.Formatters.Add(new MyJsonFormatter());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}