using Blocos.Exceptions.Handler;
using Discount.Grpc;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
// implementar os serviços ao container

builder.Services.AddCarter(new DependencyContextAssemblyCatalog([typeof(Program).Assembly]));
builder.Services.AddMediatR(config => {
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(opt => {
    opt.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

builder.Services.AddExceptionHandler<ExcecaoCustomHandler>();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

builder.Services.AddStackExchangeRedisCache(opt => {
    opt.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options => {
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
})
.ConfigurePrimaryHttpMessageHandler(() => { //NÃO USAR ISSO EM PRODUÇÃO
    var handler = new HttpClientHandler {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
    return handler;
});

var app = builder.Build();
// configurar o pipeline do http request

app.MapCarter();
app.UseExceptionHandler(opt => { });
app.UseHealthChecks("/health",
    new HealthCheckOptions {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
