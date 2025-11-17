var builder = WebApplication.CreateBuilder(args);
// implementar os serviços ao container

builder.Services.AddCarter();
builder.Services.AddMediatR(config => {
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

var app = builder.Build();
// configurar o pipeline do http request

app.MapCarter();

app.Run();
