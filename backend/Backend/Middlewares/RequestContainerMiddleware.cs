using System.Threading.Tasks;
using Microsoft.Owin;
using Unity;

namespace Backend.Middlewares
{
    public class RequestContainerMiddleware : OwinMiddleware
    {
        private readonly IUnityContainer _rootContainer;
        public const string Key = "request:UnityContainer";

        public RequestContainerMiddleware(OwinMiddleware next, IUnityContainer rootContainer) : base(next)
        {
            _rootContainer = rootContainer;
        }

        public override async Task Invoke(IOwinContext context)
        {
            using (var childContainer = _rootContainer.CreateChildContainer())
            {
                context.Set(Key, childContainer);
                await Next.Invoke(context);
            }
        }
    }
}