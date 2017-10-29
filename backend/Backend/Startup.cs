using System.Configuration;
using System.IO;
using Backend.Middlewares;
using Backend.WebApi;
using Common;
using FileBasedStorage;
using Images.Service;
using MediatR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.StaticFiles;
using Owin;
using Unity;
using Unity.Injection;

[assembly: OwinStartup(typeof(Backend.Startup))]

namespace Backend
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Utils.CreateDirectoryRecursive(ImageRepositoryPath);

            var container = BuildContainer();

            app.UseCors(CorsOptions.AllowAll);
            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true,
                RequestPath = new PathString("/image"),
                FileSystem = new PhysicalFileSystem(ImageRepositoryPath),
            });
            app.Use<RequestContainerMiddleware>(container);
            app.Use<ExceptionMiddleware>();
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
                AccessTokenFormat = new FacebookTokenFormat()
            });

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

            container.RegisterType<IImageRepository, FileBasedImageRepository>(new InjectionConstructor(ImageRepositoryPath));
            container.RegisterType(typeof(IStore<,>), typeof(FileBasedStore<,>), new InjectionConstructor(DataPath));
            return container;
        }

        private static readonly string DataPath = ConfigurationManager.AppSettings.Get("fs:storagePath");
        private static readonly string ImageRepositoryPath = Path.Combine(DataPath, "Images");
    }
}