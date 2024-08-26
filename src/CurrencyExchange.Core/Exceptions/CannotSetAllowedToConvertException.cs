namespace CurrencyExchange.Core.Exceptions;

public class CannotSetAllowedToConvertException : DomainException
{
    public override string Message =>
        """Невозможно добавить доступные для конвертации валюты, так как валюта свободно конвертируема""";
}
