using System;
using System.Security.Claims;
using Backend.Middlewares;
using Backend.WebApi;
using Common;
using Facebook;
using FileBasedStorage;
using Images.Service;
using MediatR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Unity;
using Unity.Injection;

[assembly: OwinStartup(typeof(Backend.Startup))]

namespace Backend
{
    class FacebookTokenFormat : ISecureDataFormat<AuthenticationTicket>
    {
        public string Protect(AuthenticationTicket data)
        {
            throw new System.NotImplementedException();
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            var fb = new FacebookClient(protectedText);
            var result = fb.Get<JsonObject>("/me");
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, result["id"].ToString(), null, "Facebook")
            }, "Bearer");
            return new AuthenticationTicket(identity, new AuthenticationProperties());
        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = BuildContainer();

            app.UseCors(CorsOptions.AllowAll);
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

            var dataPath = @"D:\data";

            container.RegisterType<IImageRepository, FileBasedImageRepository>(new InjectionConstructor(dataPath));
            container.RegisterType(typeof(IStore<>), typeof(FileBasedStore<>), new InjectionConstructor(dataPath));
            return container;
        }
    }
}