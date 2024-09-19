using AutoMapper;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SQLite;
using Microsoft.Data.Sqlite;
using System.Reflection;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Configuração de AutoMapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // Configuração de SQLite e Dapper (injeção de IDbConnection)
        services.AddTransient<IDbConnection>(sp =>
            new SqliteConnection(Configuration.GetConnectionString("DefaultConnection")));

        // Adicionando Controllers
        services.AddControllers();

        // Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

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

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Habilitar Swagger apenas em desenvolvimento
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
        }

        // Redirecionar HTTPS
        app.UseHttpsRedirection();

        // Autorizações (se necessário)
        app.UseAuthorization();

        // Mapear Controllers
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
