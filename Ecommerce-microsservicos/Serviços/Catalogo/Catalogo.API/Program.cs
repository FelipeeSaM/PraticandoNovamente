var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao container


var app = builder.Build();


// Configura o pipeline de requisições HTTP


app.Run();
