using OpenAdm.Cep.Application.Dtos.Usuarios;
using OpenAdm.Cep.Application.ViewModel;

namespace OpenAdm.Cep.Application.Interfaces;

public interface IUsuarioService
{
    Task<UsuarioCreateViewModel> CreateUsuarioAsync(UsuarioCreateDto usuarioCreateDto);
}
