using System;
using System.Collections.Generic;
using FluentValidation;
using MediatR;
using Unity;

namespace Common
{
    public static class UnityExtensionMethods
    {
        private const string ContainerRegisteredCommands = "unity:RegisteredCommands";

        public static void RegisterCommandHandler<TCommand, THandler, TValidator>(this IUnityContainer container)
            where THandler : IHandle<TCommand> where TCommand : IRequest where TValidator : IValidator<TCommand>
        {
            Dictionary<string, Type> registeredCommands;
            if (!container.IsRegistered<Dictionary<string, Type>>(ContainerRegisteredCommands))
            {
                registeredCommands = new Dictionary<string, Type>();
                container.RegisterInstance(ContainerRegisteredCommands, registeredCommands);
            }
            else
            {
                registeredCommands = container.Resolve<Dictionary<string, Type>>(ContainerRegisteredCommands);
            }
            registeredCommands.Add(typeof(TCommand).Name, typeof(TCommand));
            container.RegisterType<IAsyncRequestHandler<TCommand>, CommandHandler<TCommand, TValidator, THandler>>();
        }

        public static void RegisterQueryHandler<TRequest, TResponse, THandler>(this IUnityContainer container)
            where THandler : IAsyncRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
        {
            container.RegisterType<IAsyncRequestHandler<TRequest, TResponse>, THandler>();
        }

        public static Type GetCommandType(this IUnityContainer container, string commandName)
        {
            if (!container.IsRegistered<Dictionary<string, Type>>(ContainerRegisteredCommands)) return null;
            var registeredCommands = container.Resolve<Dictionary<string, Type>>(ContainerRegisteredCommands);
            Type found;
            return registeredCommands.TryGetValue(commandName, out found) ? found : null;
        }
    }
}
