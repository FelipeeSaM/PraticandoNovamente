var builder = WebApplication.CreateBuilder(args);

// Adiciona servi�os ao container


var app = builder.Build();


// Configura o pipeline de requisi��es HTTP


app.Run();
