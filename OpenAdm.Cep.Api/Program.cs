using OpenAdm.Cep.Application.Interfaces;
using OpenAdm.Cep.IoC;
using dotenv.net;
using OpenAdm.Cep.Api.Configurations;
using Microsoft.OpenApi.Models;
using OpenAdm.Cep.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();

var urlViaCep = VariaveisDeAmbiente.GetVariavel("VIA_CEP");
var stringConnection = VariaveisDeAmbiente.GetVariavel("STRING_CONNECTION");
var schema = VariaveisDeAmbiente.GetVariavel("SCHEMA");


builder.Services
    .ConfigureController()
    .ConfigureSwagger()
    .InjectCors()
    .InjectContext(stringConnection)
    .InjectViaCep(urlViaCep)
    .InjectRepositories()
    .InjectServices();

var app = builder.Build();

var basePath = "/api/v1";
app.UsePathBase(new PathString(basePath));

app.UseRouting();

app.UseSwagger(c =>
{
    c.RouteTemplate = "swagger/{documentName}/swagger.json";
    c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
    {
        swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{schema}://{httpReq.Host.Value}{basePath}" } };
    });
});
app.UseSwaggerUI();

app.UseResponseCaching();

app.UseCors("base");

app.UseMiddleware<LogMiddleware>();
app.UseMiddleware<JwtMidlleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
