using CurrencyExchange.Core.Currencies;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly ICurrencyMarketService _currencyMarketService;
    public CurrencyController(ICurrencyMarketService currencyMarketService)
    {
        _currencyMarketService = currencyMarketService;
    }    

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await _currencyMarketService.GetAsync(x => true, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var result = await _currencyMarketService.GetSingleAsync(x => true, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Currency currency, CancellationToken cancellationToken)
    {
        var result = await _currencyMarketService.AddAsync(currency, cancellationToken);
        return Ok(result);
    }

    [HttpPost("AddExchange")]
    public async Task<IActionResult> AddExchange(int to, int from, decimal cost, CancellationToken cancellationToken)
    {
        await _currencyMarketService.AddCurrencyMarketAsync(to, from, cost, cancellationToken);
        return Ok();
    }
}