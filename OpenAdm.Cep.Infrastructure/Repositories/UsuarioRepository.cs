using Microsoft.EntityFrameworkCore;
using OpenAdm.Cep.Domain.Entities;
using OpenAdm.Cep.Domain.Interfaces;
using OpenAdm.Cep.Infrastructure.Context;

namespace OpenAdm.Cep.Infrastructure.Repositories;

public sealed class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _appDbContext;

    public UsuarioRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task AddAsync(Usuario usuario)
    {
        await _appDbContext.AddAsync(usuario);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<Usuario?> GetByIdAsync(Guid id)
    {
        return await _appDbContext
            .Usuarios
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Usuario?> GetByNomeAsync(string nome)
    {
        return await _appDbContext
            .Usuarios
            .FirstOrDefaultAsync(x => x.Nome == nome);
    }
}
