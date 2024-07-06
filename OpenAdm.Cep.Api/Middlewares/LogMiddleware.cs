using OpenAdm.Cep.Application.Dtos.Error;
using OpenAdm.Cep.Application.Dtos.Logs;
using OpenAdm.Cep.Application.Interfaces;
using OpenAdm.Cep.Domain.Exceptions;
using System.Text.Json;

namespace OpenAdm.Cep.Api.Middlewares;

public class LogMiddleware
{
    private readonly RequestDelegate _next;
    private CreateAppLog _createAppLog;
    private const string _erroGenerico =
        "Ocorreu um erro interno, tente novamente mais tarde!";
    private int _statusCode = 200;

    public LogMiddleware(RequestDelegate next)
    {
        _createAppLog = new();
        _next = next;
    }

    public async Task Invoke(
        HttpContext httpContext,
        IApiLogService appLogService)
    {
        try
        {
            _createAppLog.Host = (string?)httpContext.Request.Headers.Host ?? "Host não localizado!";
            _createAppLog.Origem = (string?)httpContext.Request.Headers.FirstOrDefault(x => x.Key == "Referer").Value ?? "Referer não lozalizada!";
            _createAppLog.Path = httpContext.Request.Path;
            _createAppLog.Ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "Ip não lozalizado!";
            _createAppLog.LogLevel = 0;
            _createAppLog.StatusCode = _statusCode;

            await _next(httpContext);
        }
        catch (UnauthorizeCepException ex)
        {
            _statusCode = 401;
            await HandleError(httpContext, ex.Message);
            _createAppLog ??= new();
            _createAppLog.StatusCode = _statusCode;
            _createAppLog.LogLevel = 1;
            _createAppLog.Erro = ex.Message;
        }
        catch (CepException ex)
        {
            _statusCode = 400;
            await HandleError(httpContext, ex.Message);
            _createAppLog ??= new();
            _createAppLog.StatusCode = _statusCode;
            _createAppLog.Erro = ex.Message;
            _createAppLog.LogLevel = 2;
        }
        catch (Exception ex)
        {
            _statusCode = 400;

            await HandleError(
                httpContext,
                _erroGenerico);

            _createAppLog ??= new();
            _createAppLog.StatusCode = _statusCode;
            _createAppLog.Erro = ex?.InnerException?.Message ?? ex?.Message;
            _createAppLog.LogLevel = 3;
        }
        finally
        {
            await appLogService.CreateAppLogAsync(_createAppLog);
        }
    }

    public async Task HandleError(HttpContext httpContext, string mensagem)
    {
        httpContext.Response.Headers.Append("Access-Control-Allow-Origin", "*");
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = _statusCode;
        var errorResponse = new ErrorResponse()
        {
            Mensagem = mensagem
        };
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }
}
