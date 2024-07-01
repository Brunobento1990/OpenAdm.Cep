namespace OpenAdm.Cep.Domain.Exceptions;

public class UnauthorizeCepException : Exception
{
    public UnauthorizeCepException(string message) : base(message)
    {
    }
}
