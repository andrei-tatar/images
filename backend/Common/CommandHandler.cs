using System;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace Common
{
    public class CommandHandler<TCommand, TValidator, THandler> : IAsyncRequestHandler<TCommand>
        where TValidator : IValidator<TCommand> where TCommand : IRequest where THandler : IHandle<TCommand>
    {
        private readonly TValidator _validator;
        private readonly Lazy<THandler> _handler;

        public CommandHandler(TValidator validator, Lazy<THandler> handler)
        {
            _validator = validator;
            _handler = handler;
        }

        public async Task Handle(TCommand message)
        {
            await _validator.ValidateAndThrowAsync(message);
            await _handler.Value.Handle(message);
        }
    }
}
