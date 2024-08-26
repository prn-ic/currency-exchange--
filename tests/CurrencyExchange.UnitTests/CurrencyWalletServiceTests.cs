using CurrencyExchange.Core.Currencies;

namespace CurrencyExchange.UnitTests;

public class CurrencyWalletServiceTests
{
    [Fact]
    public void Convert_WithSimpleData_ShouldBeOk()
    {
        // Arrange
        Currency usd = new Currency("USD");
        Currency rub = new Currency("RUB");
        usd.AddAllowedToConvertCurrencies(new(rub, 91.6M));
        decimal expected = 916;

        // Act
        decimal actual = usd.Convert(rub, 10);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Convert_WithHardData_ShouldBeOk()
    {
        // Arrange
        // CNY -> USD
        Currency rur = new Currency("RUR");
        Currency usd = new Currency("USD");
        Currency eur = new Currency("EUR");
        rur.AddAllowedToConvertCurrencies(new(usd, 30.3654M));
        usd.AddAllowedToConvertCurrencies(new(eur, 1.3476M));
        
        decimal expected = 409.204M;

        // Act
        decimal actual = rur.Convert(eur, 10);

        Assert.Equal(expected, Math.Round(actual, 3));
    }
}
