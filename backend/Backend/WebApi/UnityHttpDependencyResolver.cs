using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http.Dependencies;
using Unity;

namespace Backend.WebApi
{
    public class UnityHttpDependencyResolver : IDependencyResolver
    {
        private readonly IUnityContainer _container;
        private readonly IDependencyResolver _fallbackResolver;

        public UnityHttpDependencyResolver(IUnityContainer container, IDependencyResolver fallbackResolver)
        {
            _container = container;
            _fallbackResolver = fallbackResolver;
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        public object GetService(Type serviceType)
        {
            if (serviceType.Namespace != null && serviceType.Namespace.StartsWith("System"))
                return _fallbackResolver.GetService(serviceType);

            return _container.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (serviceType.Namespace != null && serviceType.Namespace.StartsWith("System"))
                return _fallbackResolver.GetServices(serviceType);

            return _container.ResolveAll(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            var container = GetChildContainer();
            return new UnityHttpDependencyResolver(container, _fallbackResolver);
        }

        private IUnityContainer GetChildContainer()
        {
            var parentContainer =  HttpContext.Current?.GetOwinContext().GetRequestContainer() ?? _container;
            return parentContainer.CreateChildContainer();
        }
    }
}