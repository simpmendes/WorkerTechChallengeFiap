
using TechChallengeFiap.Application.Interfaces;
using TechChallengeFiap.Application.Services;
using TechChallengeFiap.Domain.Interfaces;
using TechChallengeFiap.Infra.Data;
using TechChallengeFiap.Infra.Data.Context;
using TechChallengeFiap.Infra.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Azure.Messaging.ServiceBus;
using WorkerInclusaoConsulta;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("TechChallengeConection");

builder.Services.AddDbContext<ConsultaAcoesDBContext>(
    o => o.UseSqlServer(connectionString)
    );

builder.Services.AddHttpClient();
builder.Services.AddScoped<IApiExternaFinanceIntegration, ApiExternaFinanceIntegration>();
builder.Services.AddScoped<ICotacoesAcoesService, CotacoesAcoesService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IConsultaAcoesRepository, ConsultaAcoesRepository>();
builder.Services.AddScoped<IAcaoRepository, AcaoRepository>();
builder.Services.AddScoped<IPedidoAcaoRepository, PedidoAcaoRepository>();
builder.Services.AddScoped<IAcoesUsuarioRepository, AcoesUsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<Worker>();
builder.Services.AddSingleton<IHostedService, WorkerManager>();

//builder.Services.AddDbContext<JobsDbContext>(
//    o => o.UseInMemoryDatabase("JobsDb")
//    );
// Configurar o AutoMapper
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TechChallengeFiap", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header usando Bearer.
                                    Entre com 'Bearer [espaço] então coloque seu token.
                                    Exemplo: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                        },
                        Array.Empty<string>()
                    }
                });
    //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //var xmlpath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //c.IncludeXmlComments(xmlpath);
});

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("Secret"));

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Register Azure Service Bus Client and Worker
//var serviceBusConnectionString = builder.Configuration.GetConnectionString("AzureServiceBus");
//builder.Services.AddSingleton<ServiceBusClient>(_ => new ServiceBusClient(serviceBusConnectionString));
//builder.Services.AddHostedService<Worker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
