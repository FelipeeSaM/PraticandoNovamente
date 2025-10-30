using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao container
builder.Services.AddCarter(new DependencyContextAssemblyCatalog([typeof(Program).Assembly]));
builder.Services.AddMediatR(config => {
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(Validacao<,>));
    config.AddOpenBehavior(typeof(Logging<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddMarten(opt => {
    opt.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

if(builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogoInicialData>();
}

builder.Services.AddExceptionHandler<ExcecaoCustomHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

var app = builder.Build();


// Configura o pipeline de requisições HTTP
app.MapCarter();

app.UseExceptionHandler(opt => { });

app.UseHealthChecks("/health",
    new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse }
);

//app.ExceptionHandlerPipeline();

app.Run();
