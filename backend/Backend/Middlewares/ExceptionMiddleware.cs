using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace Backend.Middlewares
{
    public class ExceptionMiddleware : OwinMiddleware
    {
        public ExceptionMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            try
            {
                await Next.Invoke(context);
            }
            catch (Exception)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}