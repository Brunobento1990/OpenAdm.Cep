using Microsoft.AspNetCore.Mvc;
using OpenAdm.Cep.Application.Dtos.Error;
using OpenAdm.Cep.Application.Dtos.Usuarios;
using OpenAdm.Cep.Application.Interfaces;
using OpenAdm.Cep.Application.ViewModel;

namespace OpenAdm.Cep.Api.Controllers;

[ApiController]
[Route("usuario")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost]
    [ProducesResponseType<ErrorResponse>(400)]
    [ProducesResponseType<UsuarioCreateViewModel>(201)]
    public async Task<IActionResult> Create(UsuarioCreateDto usuarioCreateDto)
    {
        var response = await _usuarioService.CreateUsuarioAsync(usuarioCreateDto);
        return Created($"/usuario?id={response.ChaveDeAcesso}", response);
    }
}
