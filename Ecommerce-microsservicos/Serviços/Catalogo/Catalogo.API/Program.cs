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

builder.Services.AddExceptionHandler<ExcecaoCustomHandler>();

var app = builder.Build();


// Configura o pipeline de requisições HTTP
app.MapCarter();

app.UseExceptionHandler(opt => { });

//app.ExceptionHandlerPipeline();

app.Run();
