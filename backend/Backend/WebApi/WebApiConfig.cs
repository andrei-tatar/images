using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;
using Common;
using FluentValidation;
using Newtonsoft.Json;
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

    public class WebApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly JsonSerializerSettings _jsonSetings;

        public WebApiExceptionFilter(JsonSerializerSettings jsonSetings)
        {
            _jsonSetings = jsonSetings;
        }

        public override async Task OnExceptionAsync(HttpActionExecutedContext context, CancellationToken cancellationToken)
        {
            var validationException = context.Exception as ValidationException;
            if (validationException != null)
            {
                context.Response = new HttpResponseMessage
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new
                    {
                        ValidationErrors = validationException.Errors.Select(s => new
                        {
                            Field = s.PropertyName.ToCamelCase(),
                            Code = s.ErrorCode,
                        }).ToArray()
                    }, _jsonSetings), Encoding.UTF8, "application/json"),
                    StatusCode = HttpStatusCode.BadRequest,

                };
                return;
            }

            await base.OnExceptionAsync(context, cancellationToken);
        }
    }
}