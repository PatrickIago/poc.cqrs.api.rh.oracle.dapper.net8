using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Poc.DistributedCache.Configuration;
using Poc.RH.API.Models;
using System.Reflection;
using System.Text;

namespace Poc.RH.API.Extensions;

public static class WebApplicationExtensions
{
    //public static async Task MigrateAsync(this WebApplication app)
    //{
    //    await using var serviceScope = app.Services.CreateAsyncScope();
    //    await using var writeDbContext = serviceScope.ServiceProvider.GetRequiredService<EFContext>();
    //    try
    //    {
    //        await app.MigrateDbContextAsync(writeDbContext);
    //    }
    //    catch (Exception ex)
    //    {
    //        app.Logger.LogError(ex, "Ocorreu uma exceção ao iniciar a aplicação: {Message}", ex.Message);
    //        throw;
    //    }
    //}

    //private static async Task MigrateDbContextAsync<TContext>(this WebApplication app, TContext context)
    //    where TContext : DbContext
    //{
    //    var dbName = context.Database.GetDbConnection().Database;

    //    app.Logger.LogInformation("----- {DbName}: {DbConnection}", dbName, context.Database.GetConnectionString());
    //    app.Logger.LogInformation("----- {DbName}: Verificando se existem migrações pendentes...", dbName);

    //    if ((await context.Database.GetPendingMigrationsAsync()).Any())
    //    {
    //        app.Logger.LogInformation("----- {DbName}: Criando e migrando a base de dados...", dbName);

    //        await context.Database.MigrateAsync();

    //        app.Logger.LogInformation("----- {DbName}: Base de dados criada e migrada com sucesso!", dbName);
    //    }
    //    else
    //    {
    //        app.Logger.LogInformation("----- {DbName}: Migrações estão em dia.", dbName);
    //    }
    //}

    public static void AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Poc.RH.API",
                Version = "v1",
                Description = "Api de autenticação de usuários",
                Contact = new OpenApiContact
                {
                    Name = "Guilherme Figueiras Maurila",
                    Email = "gfmaurila@gmail.com"
                },
            });

            // Adicione este bloco para filtrar controladores por versão
            c.DocInclusionPredicate((version, apiDescription) =>
            {
                if (version == "v1" && apiDescription.RelativePath.Contains("api/v1/"))
                    return true;

                return false;
            });


            // Primeira definição de segurança
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header usando o esquema Bearer."
            });

            // Requisito de segurança para "Bearer"
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });


    }

    public static void UseAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = configuration.GetValue<string>(ConfigConsts.Issuer),
                    ValidAudience = configuration.GetValue<string>(ConfigConsts.Audience),
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(configuration.GetValue<string>(ConfigConsts.Key)))
                };
            });
    }

    public static void UseDevelopmentSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Poc.RH.API");
        });
    }

    public static void AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoSettings = configuration.GetSection("MongoDB").Get<MongoDbSettings>();
        var connectionString = configuration.GetConnectionString("CacheConnection");

        var healthCheckBuilder = services
            .AddHealthChecks()
            //.AddDbContextCheck<EFSqlServerContext>(tags: HealthCheckTags.DatabaseTags)
            .AddMongoDb(mongoSettings.ConnectionString, tags: HealthCheckTags.DatabaseTags)
            .AddRedis(connectionString, tags: HealthCheckTags.CacheTags);

        var rabbitMqConfig = configuration.GetSection("RabbitMQ");
        var rabbitMqConnectionString = $"amqp://{rabbitMqConfig["Username"]}:{rabbitMqConfig["Password"]}@{rabbitMqConfig["Hostname"]}:{rabbitMqConfig["Port"]}/{rabbitMqConfig["VirtualHost"]}";

        healthCheckBuilder.AddRabbitMQ(
            rabbitConnectionString: rabbitMqConnectionString,
            tags: HealthCheckTags.RabbitMqTags
        );
    }
}
