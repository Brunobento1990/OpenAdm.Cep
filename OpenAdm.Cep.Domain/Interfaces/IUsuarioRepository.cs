using OpenAdm.Cep.Domain.Entities;

namespace OpenAdm.Cep.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task AddAsync(Usuario usuario);
    Task<Usuario?> GetByIdAsync(Guid id);
    Task<Usuario?> GetByNomeAsync(string nome);
}
