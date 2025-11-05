using System.Text.Json.Serialization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;
using Shoop.CrossCutting.IoC;
using System.Reflection; // ⬅️ Using Adicionado

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddInfrastructureAPI(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Shoop API - Clean Architecture",
        Version = "v1",
        Description = @"O projeto Shoop é uma <b>API RESTful</b> de e-commerce desenvolvida em <b>ASP.NET Core (Net 9.0)</b>, 
        <b>seguindo os princípios de Arquitetura Limpa (Clean Architecture)</b>. 
        <br>
        O sistema é modularizado em <b>camadas claras</b>,
        focando na separação de responsabilidades e na manutenibilidade.",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "License",
            Url = new Uri("https://example.com/license")
        },
    });

    // BLOCO XML 
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    // ----------------------------------------------------

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token.",
    });

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
            Array.Empty<string>()
        }
    });
});

// Adiciona suporte à compactação de resposta HTTP na sua aplicação
builder.Services.AddResponseCompression(options =>
{
    // Especifica que a compressão Gzip será usada como provedora de compressão
    options.Providers.Add<GzipCompressionProvider>();

    // Define os tipos de MIME que irão passar por compressão
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
});


var app = builder.Build();

// Middleware order is important
app.UseResponseCaching(); //cacher
app.UseHttpsRedirection();
app.UseStaticFiles(); // Serve custom.css and code.svg
app.UseAuthorization();

app.UseSwagger();

// Serve Swagger directly at the root (http://localhost:5149)
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Shoop API v1");
    options.RoutePrefix = ""; // <-- Swagger at the root
    options.InjectStylesheet("/custom.css"); // load directly from wwwroot
});

app.MapControllers();
app.Run();