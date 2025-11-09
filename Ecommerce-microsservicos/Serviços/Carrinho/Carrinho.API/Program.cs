var builder = WebApplication.CreateBuilder(args);

// implementar os serviços ao container

var app = builder.Build();

// configurar o pipeline do http request

app.Run();
