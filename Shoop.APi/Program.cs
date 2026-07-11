using System.Text.Json.Serialization;
using Microsoft.AspNetCore.ResponseCompression;
using Shoop.CrossCutting.IoC;
using NSwag.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddInfrastructureAPI(builder.Configuration);
builder.Services.AddInfrastructureSwagger(builder.Configuration);
builder.Services.AddInfrastructureJWT(builder.Configuration);
builder.Services.AddInfrastructureCors();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);



builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<GzipCompressionProvider>();
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
});



var app = builder.Build();

app.UseResponseCaching();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors(DependencyInjectionCors.GetCorsPolicyName());
app.UseAuthentication();
app.UseAuthorization();



if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();

    app.UseSwaggerUi(options =>
    {
        options.Path = "";
    });
}


app.Use(async (context, next) =>
{
    context.Response.Headers.Server = "Nao Interessa";
    await next();
});

app.MapControllers();
app.Run();