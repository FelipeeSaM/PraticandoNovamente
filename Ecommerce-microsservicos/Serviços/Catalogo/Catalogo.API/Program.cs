var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao container
builder.Services.AddCarter(new DependencyContextAssemblyCatalog([typeof(Program).Assembly]));
builder.Services.AddMediatR(config => {
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();


// Configura o pipeline de requisições HTTP
app.MapCarter();

app.Run();
