using Microsoft.AspNetCore.Http.Features;
using OpenAdm.Cep.Api.Attributes;
using OpenAdm.Cep.Domain.Exceptions;
using OpenAdm.Cep.Domain.Interfaces;

namespace OpenAdm.Cep.Api.Middlewares;

public class JwtMidlleware
{
    private readonly RequestDelegate _next;
    public JwtMidlleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext httpContext, IUsuarioRepository usuarioRepository)
    {
        var token = httpContext.Request.Headers.Authorization.ToString().Split(" ").Last();
        var autenticar = httpContext.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata
                .FirstOrDefault(m => m is AuthorizeJwtAttribute) is AuthorizeJwtAttribute atributoAutorizacao;

        if (!autenticar)
        {
            await _next(httpContext);
            return;
        }

        if(!Guid.TryParse(token, out var id))
        {
            throw new UnauthorizeCepException("Chave de acesso inválida!");
        }

        _ = await usuarioRepository.GetByIdAsync(id)
            ?? throw new UnauthorizeCepException("Não foi possível localizar seu registro!");

        await _next(httpContext);
    }
}
