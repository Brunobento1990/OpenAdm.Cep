namespace OpenAdm.Cep.Application.ViewModel;

public class FreteViewModel
{
    public decimal TotalFrete { get; set; }
    public EnderecoViewModel Endereco { get; set; } = new();
}
