using Microsoft.AspNetCore.Mvc;
using OpenAdm.Cep.Api.Attributes;
using OpenAdm.Cep.Application.Dtos.Error;
using OpenAdm.Cep.Application.Dtos.Fretes;
using OpenAdm.Cep.Application.Interfaces;
using OpenAdm.Cep.Application.ViewModel;

namespace OpenAdm.Cep.Api.Controllers;

[ApiController]
[Route("frete")]
public class FreteController : ControllerBase
{
    private readonly IFreteService _freteService;

    public FreteController(IFreteService freteService)
    {
        _freteService = freteService;
    }

    [HttpPost("calcular")]
    [AuthorizeJwt]
    [ProducesResponseType<ErrorResponse>(400)]
    [ProducesResponseType<ErrorResponse>(401)]
    [ProducesResponseType<FreteViewModel>(200)]
    public async Task<IActionResult> CalcularFrete(CalcularFreteDto calcularFreteDto)
    {
        var response = await _freteService.CalcularFreteAsync(calcularFreteDto);
        return Ok(response);
    }
}
