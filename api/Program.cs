using api.Database;
using api.Endpoints;
using Microsoft.EntityFrameworkCore;

namespace api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Cria��o da WebApplication
            var builder = WebApplication.CreateBuilder(args);

            // Configura��o do Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("PermitirTodasOrigens",
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
            });

            // Configura��o do Banco de Dados
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ProdsDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.Parse("8.0.36-mysql"))
            );

            // Constru��o da WebApplication
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                // Inicializa��o do Swagger
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Registro dos endpoints de produtos
            app.RegistrarEndpointsProduto();

            // Registro dos endpoints de categorias
            app.RegistrarEndpointsCategoria();

            // Registro dos endpoints de usuarios
            //app.RegistrarEndpointsUsuario();

            app.UseCors("PermitirTodasOrigens");

            // Execu��o da aplica��o
            app.Run();
        }
    }
}
