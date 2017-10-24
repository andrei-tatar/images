using System.Threading.Tasks;
using MediatR;

namespace Common
{
    public interface IHandle<in T> where T : IRequest
    {
        Task Handle(T command);
    }
}