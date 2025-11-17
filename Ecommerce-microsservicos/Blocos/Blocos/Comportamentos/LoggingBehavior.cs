using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Blocos.Comportamentos
{
    public class LoggingBehavior<TRequest, TResponse>
        (ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
            where TRequest : notnull, IRequest<TResponse>
            where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] handle request={Request} - Response={Response} - RequestData={RequestData}",
                typeof(TRequest).Name, typeof(TResponse).Name, request);

            var cronometro = new Stopwatch();
            cronometro.Start();

            var response = await next();

            cronometro.Stop();
            var tempoDecorrido = cronometro.Elapsed;

            if(tempoDecorrido.Seconds > 3)
            {
                logger.LogInformation("[PERFORMANCE] a request {Request} durou {Duracao}",
                    typeof(TRequest).Name, tempoDecorrido.Milliseconds);
            }

            logger.LogInformation("[Final] handler {Request} com a response {Response}", typeof(TRequest).Name, typeof(TResponse).Name);
            return response;
        }
    }
}
