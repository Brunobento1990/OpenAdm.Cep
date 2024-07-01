using OpenAdm.Cep.Application.ViewModel;

namespace OpenAdm.Cep.Application.Interfaces;

public interface ICepService
{
    Task<EnderecoViewModel> ConsultarCepAsync(string cep);
}
