using Blocos.CQRS;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Blocos.Comportamentos
{
    public class Validacao<TRequest, TResponse>
        (IEnumerable<IValidator<TRequest>> validadores)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var contexto = new ValidationContext<TRequest>(request);

            var resultadosValidacoes = await Task.WhenAll(validadores.Select(c => c.ValidateAsync(contexto, cancellationToken)));

            var falhas = resultadosValidacoes
                .Where(c => c.Errors.Any())
                .SelectMany(c => c.Errors)
                .ToList();

            if(falhas.Any())
            {
                throw new ValidationException(falhas);
            }

            return await next();
        }
    }
}
