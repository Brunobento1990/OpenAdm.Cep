using OpenAdm.Cep.Application.Dtos.Usuarios;
using OpenAdm.Cep.Application.Interfaces;
using OpenAdm.Cep.Application.ViewModel;
using OpenAdm.Cep.Domain.Entities;
using OpenAdm.Cep.Domain.Exceptions;
using OpenAdm.Cep.Domain.Interfaces;

namespace OpenAdm.Cep.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<UsuarioCreateViewModel> CreateUsuarioAsync(UsuarioCreateDto usuarioCreateDto)
    {
        usuarioCreateDto.Validar();

        var usuario = await _usuarioRepository.GetByNomeAsync(usuarioCreateDto.Nome);

        if(usuario != null)
        {
            throw new CepException("Este nome já está em uso!");
        }

        usuario = new Usuario(
            id: Guid.NewGuid(), 
            dataDeCadastro: DateTime.Now, 
            dataDeAtualizacao: null, 
            nome: usuarioCreateDto.Nome);
    
        await _usuarioRepository.AddAsync(usuario);

        return new UsuarioCreateViewModel()
        {
            ChaveDeAcesso = usuario.Id
        };
    }
}
