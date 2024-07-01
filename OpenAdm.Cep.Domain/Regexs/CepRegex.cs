using System.Text.RegularExpressions;

namespace OpenAdm.Cep.Domain.Regexs;

public static class CepRegex
{
    public static bool ValidarCep(this string cep)
    {
        if(string.IsNullOrWhiteSpace(cep)) return false;
        string cepPattern = @"^\d{8}$";
        var regex = new Regex(cepPattern);
        return regex.IsMatch(cep);
    }
}
