using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blocos.Exceptions.Handler
{
    public class ExcecaoCustomHandler 
        (ILogger<ExcecaoCustomHandler> logger)
        : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(
                "Mensagem de erro: {exceptionMessage}, Data da ocorrência {data}",
                exception.Message, DateTime.UtcNow
            );

            (string Detalhe, string Titulo, int StatusCode) detalhes = exception 
                switch {
                    ExcecaoInterna => (
                        exception.Message,
                        exception.GetType().Name,
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError
                        ),

                    ValidationException => (
                    exception.Message,
                    exception.GetType().Name,
                    context.Response.StatusCode = StatusCodes.Status400BadRequest
                        ),

                    RequestBaseExcecao => (
                    exception.Message,
                    exception.GetType().Name,
                    context.Response.StatusCode = StatusCodes.Status400BadRequest
                        ),

                    NaoEncontradoExcecao => (
                    exception.Message,
                    exception.GetType().Name,
                    context.Response.StatusCode = StatusCodes.Status404NotFound
                        ),

                    _ => (
                    exception.Message,
                    exception.GetType().Name,
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError
                        )
                };

            var problemasDetalhes = new ProblemDetails {
                Title = detalhes.Titulo,
                Detail = detalhes.Detalhe,
                Status = detalhes.StatusCode
            };

            problemasDetalhes.Extensions.Add("traceId", context.TraceIdentifier);

            if(exception is ValidationException validationException)
            {
                problemasDetalhes.Extensions.Add("ValidacaoDeErros", validationException.Errors);
            }

            await context.Response.WriteAsJsonAsync(problemasDetalhes, cancellationToken: cancellationToken);
            return true;
        }
    }
}
