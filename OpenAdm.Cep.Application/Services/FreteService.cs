using OpenAdm.Cep.Application.Dtos.Fretes;
using OpenAdm.Cep.Application.Interfaces;
using OpenAdm.Cep.Application.ViewModel;

namespace OpenAdm.Cep.Application.Services;

public class FreteService : IFreteService
{
    private readonly ICepService _cepService;
    private readonly IChromeService _chromeService;

    public FreteService(ICepService cepService, IChromeService chromeService)
    {
        _cepService = cepService;
        _chromeService = chromeService;
    }

    public async Task<FreteViewModel> CalcularFreteAsync(CalcularFreteDto calcularFreteDto)
    {
        calcularFreteDto.Validar();

        var endereco = await _cepService.ConsultarCepAsync(calcularFreteDto.CepDestino);
        var totalFrete = await _chromeService.CalcularFreteAsync(calcularFreteDto);

        return new FreteViewModel()
        {
            Endereco = endereco,
            TotalFrete = totalFrete,
        };
    }
}
