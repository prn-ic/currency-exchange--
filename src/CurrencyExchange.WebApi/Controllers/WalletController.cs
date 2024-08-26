using CurrencyExchange.Core.Currencies;
using CurrencyExchange.Core.Wallets;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WalletController : ControllerBase
{
    private readonly IWalletService _walletService;
    private readonly ICurrencyMarketService _currencyMarketService;
    private readonly IMediator _mediator;
    public WalletController(IWalletService walletService, ICurrencyMarketService currencyMarketService, IMediator mediator)
    {
        _walletService = walletService;
        _currencyMarketService = currencyMarketService;
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var result = await _walletService.GetAsync(id, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Wallet wallet, CancellationToken cancellationToken)
    {
        var result = await _walletService.CreateAsync(wallet, cancellationToken);
        return Ok(result);
    }

    [HttpPut("increase/{id}/{currId}")]
    public async Task<IActionResult> Increase(int id, int currId, decimal cost, CancellationToken cancellationToken)
    {
        var wallet = await _walletService.GetAsync(id);
        var currency = await _currencyMarketService.GetSingleAsync(x => x.Id == currId, cancellationToken);

        wallet.Increase(cost, currency);
        foreach (var events in wallet.DomainEvents)
            await _mediator.Publish(events);
        wallet.ClearDomainEvent(); 
        
        return Ok();
    }
    [HttpPut("decrease/{id}/{currId}")]
    public async Task<IActionResult> Decrease(int id, int currId, decimal cost, CancellationToken cancellationToken)
    {
        var wallet = await _walletService.GetAsync(id);
        var currency = await _currencyMarketService.GetSingleAsync(x => x.Id == currId, cancellationToken);

        wallet.Descrease(cost, currency);
        foreach (var events in wallet.DomainEvents)
            await _mediator.Publish(events);

        wallet.ClearDomainEvent(); 
        
        return Ok();
    }
}