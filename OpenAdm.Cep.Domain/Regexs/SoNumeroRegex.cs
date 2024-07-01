using System.Text.RegularExpressions;

namespace OpenAdm.Cep.Domain.Regexs;

public static class SoNumeroRegex
{
    public static bool ValidarSomenteNumero(this string somenteNumero)
    {
        if (string.IsNullOrWhiteSpace(somenteNumero)) return false;
        string numberPattern = @"^\d+$";
        var regex = new Regex(numberPattern);
        return regex.IsMatch(somenteNumero);
    }
}
