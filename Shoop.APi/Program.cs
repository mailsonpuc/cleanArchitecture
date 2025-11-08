using System.Text.Json.Serialization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;
using Shoop.CrossCutting.IoC;
using System.Reflection;
using System.Text;
using Shoop.APi;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddInfrastructureAPI(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

#region Swagger

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
            Url = new Uri("https://t.me/mailsonssv")
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
#endregion swagger

// Adiciona suporte à compactação de resposta HTTP aplicação
builder.Services.AddResponseCompression(options =>
{
    // Especifica que a compressão Gzip será usada como provedora de compressão
    options.Providers.Add<GzipCompressionProvider>();

    // Define os tipos de MIME que irão passar por compressão
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
});

#region jwt
var key = Encoding.ASCII.GetBytes(Settings.Secret);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,

    };
});
#endregion





string OrigensComAcessoPermitido = "_originsComAcessoPermitido";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: OrigensComAcessoPermitido, policy =>
    {
        policy.WithOrigins("https://apirequest.io", "http://localhost:5149")
        .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
        
        .AllowAnyHeader()
        .AllowCredentials();
        
    });
});


var app = builder.Build();

// Middleware order is important
app.UseResponseCaching(); // Caching de resposta
app.UseHttpsRedirection(); // Redireciona para HTTPS
app.UseStaticFiles(); // Serve arquivos estáticos como custom.css e code.svg
app.UseCors(OrigensComAcessoPermitido); // Aplica políticas de CORS (deve vir antes de Autenticação e Autorização)
app.UseAuthentication();
app.UseAuthorization();



app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Shoop API v1");
    options.RoutePrefix = ""; // <-- Swagger at the root
    options.InjectStylesheet("/custom.css"); // load directly from wwwroot
});


app.Use(async (context, next) =>
{
    context.Response.Headers.Server = "Nao Interessa";
    // context.Response.Headers.Remove("Server"); 

    await next();
});


app.MapControllers();
app.Run();



