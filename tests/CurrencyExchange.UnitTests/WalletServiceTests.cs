using CurrencyExchange.Core.Currencies;
using CurrencyExchange.Core.Wallets;

namespace CurrencyExchange.UnitTests;

public class WalletServiceTests
{
    [Fact]
    public void Increase_WithSimpleData_ShouldBeOk()
    {
        // Arrange
        decimal addend = 10;
        Currency usd = new Currency("USD");
        Wallet wallet = new Wallet(10, usd);
        decimal expected = 20;

        // Act
        wallet.Increase(addend, usd);
        decimal actual = wallet.Capacity;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Increase_WithHardData_ShouldBeOk()
    {
        // Arrange

        Currency rur = new Currency("RUR");
        Currency usd = new Currency("USD");
        Currency eur = new Currency("EUR");
        rur.AddAllowedToConvertCurrencies(new(usd, 30.3654M));
        usd.AddAllowedToConvertCurrencies(new(eur, 1.3476M));
        Wallet wallet = new Wallet(10, rur);

        decimal addend = 10;
        decimal expected = 409.204M + wallet.Capacity;

        // Act
        wallet.Increase(addend, eur);
        decimal actual = wallet.Capacity;

        Assert.Equal(expected, Math.Round(actual, 3));
    }

    [Fact]
    public void Descrease_WithSimpleData_ShouldBeOk()
    {
        // Arrange
        decimal addend = 10;
        Currency usd = new Currency("USD");
        Wallet wallet = new Wallet(10, usd);
        decimal expected = 0;

        // Act
        wallet.Descrease(addend, usd);
        decimal actual = wallet.Capacity;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Descrease_WithHardData_ShouldBeOk()
    {
        // Arrange

        Currency rur = new Currency("RUR");
        Currency usd = new Currency("USD");
        Currency eur = new Currency("EUR");
        rur.AddAllowedToConvertCurrencies(new(usd, 30.3654M));
        usd.AddAllowedToConvertCurrencies(new(eur, 1.3476M));
        Wallet wallet = new Wallet(10, rur);

        decimal deductible = 10;
        decimal expected = 409.204M - wallet.Capacity;

        // Act
        wallet.Descrease(deductible, eur);
        decimal actual = wallet.Capacity;

        Assert.Equal(expected, Math.Round(actual, 3));
    }
}
