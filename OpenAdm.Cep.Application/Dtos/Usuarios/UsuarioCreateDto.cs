using OpenAdm.Cep.Domain.Exceptions;

namespace OpenAdm.Cep.Application.Dtos.Usuarios;

public class UsuarioCreateDto
{
    public string Nome { get; set; } = string.Empty;

    public void Validar()
    {
        if (string.IsNullOrWhiteSpace(Nome))
        {
            throw new CepException("Informe o nome");
        }

        if(Nome.Length > 255)
        {
            throw new CepException("Informe no máximo 255 caracteres");
        }

        if(Nome.Length < 5)
        {
            throw new CepException("Seu nome deve ser maior que 5 caracteres");
        }
    }
}
