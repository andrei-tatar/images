using System.Web.Http;
using MediatR;
using Unity.Attributes;

namespace Backend.Controllers
{
    public class BaseController : ApiController
    {
        [Dependency]
        protected IMediator Mediator { get; set; }
    }
}