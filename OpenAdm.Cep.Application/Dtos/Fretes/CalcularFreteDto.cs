using OpenAdm.Cep.Domain.Exceptions;
using OpenAdm.Cep.Domain.Regexs;

namespace OpenAdm.Cep.Application.Dtos.Fretes;

public class CalcularFreteDto
{
    public string CepOrigem { get; set; } = string.Empty;
    public string CepDestino { get; set; } = string.Empty;
    public string Altura { get; set; } = string.Empty;
    public string Largura { get; set; } = string.Empty;
    public string Comprimento { get; set; } = string.Empty;
    public int Peso { get; set; }

    public void Validar()
    {
        if(Peso < 1 || Peso > 30)
        {
            throw new CepException("Peso inválido, informe um peso entre 1 e 30 KG");
        }

        if (!CepOrigem.ValidarCep())
        {
            throw new CepException("Cep de origem inválido!");
        }

        if (!CepDestino.ValidarCep())
        {
            throw new CepException("Cep de destino inválido!");
        }

        if (!Altura.ValidarSomenteNumero())
        {
            throw new CepException("Altura da embalagem inválida!");
        }

        if (!Largura.ValidarSomenteNumero())
        {
            throw new CepException("Largura da embalagem inválida!");
        }

        if (!Comprimento.ValidarSomenteNumero())
        {
            throw new CepException("Comprimento da embalagem inválida!");
        }
    }
}
