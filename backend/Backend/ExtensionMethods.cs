using Backend.Middlewares;
using Microsoft.Owin;
using Unity;

namespace Backend
{
    public static class ExtensionMethods
    {
        public static IUnityContainer GetRequestContainer(this IOwinContext context)
        {
            return context.Get<IUnityContainer>(RequestContainerMiddleware.Key);
        }
    }
}