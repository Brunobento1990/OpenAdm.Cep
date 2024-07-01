using OpenAdm.Cep.Application.Dtos.Fretes;

namespace OpenAdm.Cep.Application.Interfaces;

public interface IChromeService
{
    Task<decimal> CalcularFreteAsync(CalcularFreteDto calcularFreteDto);
    void Quit();
}
