using Backend.Middlewares;
using Backend.WebApi;
using Images.Service;
using MediatR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using Unity;

[assembly: OwinStartup(typeof(Backend.Startup))]

namespace Backend
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = BuildContainer();

            app.UseCors(CorsOptions.AllowAll);
            app.Use<RequestContainerMiddleware>(container);
            app.Use<ExceptionMiddleware>();
            app.UseWebApi(new WebApiConfig(container));
        }

        private static IUnityContainer BuildContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IMediator, Mediator>();
            container.RegisterInstance<SingleInstanceFactory>(
                t => container.IsRegistered(t) ? container.Resolve(t) : null);
            container.RegisterInstance<MultiInstanceFactory>(
                t => container.IsRegistered(t) ? container.ResolveAll(t) : new object[0]);
            ImagesModule.Register(container);
            return container;
        }
    }
}