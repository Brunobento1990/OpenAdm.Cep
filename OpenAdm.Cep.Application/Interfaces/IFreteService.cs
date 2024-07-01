using OpenAdm.Cep.Application.Dtos.Fretes;
using OpenAdm.Cep.Application.ViewModel;

namespace OpenAdm.Cep.Application.Interfaces;

public interface IFreteService
{
    Task<FreteViewModel> CalcularFreteAsync(CalcularFreteDto calcularFreteDto);
}
