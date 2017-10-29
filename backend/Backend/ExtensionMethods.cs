using System;
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

        public static TOut Change<TOut>(this object value)
        {
            return (TOut)Enum.Parse(typeof(TOut), value.ToString());
        }
    }
}