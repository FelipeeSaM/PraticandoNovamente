using Blocos.Comportamentos;
using FluentValidation;
var builder = WebApplication.CreateBuilder(args);

// Adiciona servi�os ao container
builder.Services.AddCarter(new DependencyContextAssemblyCatalog([typeof(Program).Assembly]));
builder.Services.AddMediatR(config => {
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(Validacao<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddMarten(opt => {
    opt.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();


// Configura o pipeline de requisi��es HTTP
app.MapCarter();

app.Run();
