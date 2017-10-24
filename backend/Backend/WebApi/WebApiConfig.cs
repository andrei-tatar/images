using System.Web.Http;
using Unity;

namespace Backend.WebApi
{
    public class WebApiConfig : HttpConfiguration
    {
        public WebApiConfig(IUnityContainer container)
        {
            DependencyResolver = new UnityHttpDependencyResolver(container, DependencyResolver);

            Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}