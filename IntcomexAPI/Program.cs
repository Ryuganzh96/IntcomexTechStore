using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Disable reference handling to prevent $id properties from appearing in JSON responses
    options.JsonSerializerOptions.ReferenceHandler = null;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Intcomex API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
    {
        new OpenApiSecurityScheme{
            Reference = new OpenApiReference{
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[]{}
    }});
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
    .EnableSensitiveDataLogging() // For debugging purposes only
);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Secret"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Obtener parámetros de rate limit desde la base de datos antes de construir la aplicación
using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    var rateLimitEndpoint = _context.Parametros.FirstOrDefault(p => p.Nombre.ToLower().Contains("rate_limit_endpoint"))?.Valor ?? "*";
    var rateLimitPeriod = _context.Parametros.FirstOrDefault(p => p.Nombre.ToLower().Contains("period"))?.Valor ?? "1m";
    var rateLimit = _context.Parametros.FirstOrDefault(p => p.Nombre.ToLower().Equals("limit"))?.Valor ?? "100";

    var rateLimitOptions = new IpRateLimitOptions
    {
        GeneralRules = new List<RateLimitRule>
        {
            new RateLimitRule
            {
                Endpoint = rateLimitEndpoint,
                Period = rateLimitPeriod,
                Limit = int.Parse(rateLimit)
            }
        }
    };

    builder.Services.AddSingleton(rateLimitOptions);
}

// Configurar Rate Limiting
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>(); // Estrategia de procesamiento
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>(); // Almacenamiento de contadores
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>(); // Almacenamiento de políticas de IP

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Intcomex API v1"));
}

app.UseHttpsRedirection();

// Habilitar CORS
app.UseCors("AllowAllOrigins");

// Aplicar el middleware de Rate Limiting
app.UseIpRateLimiting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
