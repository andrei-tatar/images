using System.Web.Http;
using Newtonsoft.Json.Serialization;
using Unity;

namespace Backend.WebApi
{
    public class WebApiConfig : HttpConfiguration
    {
        public WebApiConfig(IUnityContainer container)
        {
            Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            Formatters.JsonFormatter.UseDataContractJsonSerializer = false;

            DependencyResolver = new UnityHttpDependencyResolver(container, DependencyResolver);

            Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            Filters.Add(new WebApiExceptionFilter(Formatters.JsonFormatter.SerializerSettings));
        }
    }
}