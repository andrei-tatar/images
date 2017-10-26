using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using FluentValidation;
using Newtonsoft.Json;

namespace Backend.WebApi
{
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
                            Field = Common.ExtensionMethods.ToCamelCase(s.PropertyName),
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