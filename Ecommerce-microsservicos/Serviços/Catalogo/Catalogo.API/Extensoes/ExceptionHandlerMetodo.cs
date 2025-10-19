using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.API.Extensoes
{
    public static class ExceptionHandlerMetodo
    {
        public static IApplicationBuilder ExceptionHandlerPipeline(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(exceptionHandlerApp => {
                exceptionHandlerApp.Run(async context => {
                    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                    if(exception is null) { return; }

                    var detalhes = new ProblemDetails {
                        Title = exception.Message,
                        Status = StatusCodes.Status500InternalServerError,
                        Detail = exception.StackTrace
                    };

                    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
                    logger.LogError(exception, exception.Message);

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/problem+json";

                    await context.Response.WriteAsJsonAsync(detalhes);
                });

            });

            return app;
        }
    }
}
