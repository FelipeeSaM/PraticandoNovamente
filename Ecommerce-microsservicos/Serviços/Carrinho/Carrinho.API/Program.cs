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


builder.Services.AddScoped<IBasketRepository, BasketRepository>();

var app = builder.Build();
// configurar o pipeline do http request

app.MapCarter();

app.Run();
