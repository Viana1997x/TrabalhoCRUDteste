using AutoMapper;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Reflection;
using Trabalho;
using Trabalho.Repositories;
using Trabalho.Services;

SQLitePCL.Batteries.Init(); // Inicializa a biblioteca SQLite

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(MappingProfile)); // Certifique-se de registrar o perfil aqui

// Configuração de SQLite e Dapper (injeção de IDbConnection)
builder.Services.AddTransient<IDbConnection>(sp =>
    new SqliteConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro dos repositórios injetando IDbConnection
builder.Services.AddTransient<IClienteRepository, ClienteRepository>();
builder.Services.AddTransient<IMedicoRepository, MedicoRepository>();
builder.Services.AddTransient<IConsultaRepository, ConsultaRepository>();

// Registro dos serviços
builder.Services.AddTransient<ClienteService>();
builder.Services.AddTransient<MedicoService>();
builder.Services.AddTransient<ConsultaService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Initialize Database
using (var scope = app.Services.CreateScope())
{
    var dbConnection = scope.ServiceProvider.GetRequiredService<IDbConnection>();
    InitializeDatabase(dbConnection);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

void InitializeDatabase(IDbConnection dbConnection)
{
    var createClientesTable = @"CREATE TABLE IF NOT EXISTS Clientes (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Nome TEXT NOT NULL,
        Idade INTEGER NOT NULL,
        DataNascimento TEXT NOT NULL,
        Telefone TEXT NOT NULL,
        DataRegistro TEXT NOT NULL
    );";

    var createMedicosTable = @"CREATE TABLE IF NOT EXISTS Medicos (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Nome TEXT NOT NULL,
        Idade INTEGER NOT NULL,
        DataNascimento TEXT NOT NULL,
        DataRegistro TEXT NOT NULL
    );";

    var createConsultasTable = @"CREATE TABLE IF NOT EXISTS Consultas (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        ClienteId INTEGER NOT NULL,
        MedicoId INTEGER NOT NULL,
        DataConsulta TEXT NOT NULL,
        DataRegistro TEXT NOT NULL,
        FOREIGN KEY (ClienteId) REFERENCES Clientes(Id),
        FOREIGN KEY (MedicoId) REFERENCES Medicos(Id)
    );";

    dbConnection.Execute(createClientesTable);
    dbConnection.Execute(createMedicosTable);
    dbConnection.Execute(createConsultasTable);
}
