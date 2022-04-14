using Microsoft.AspNetCore.Mvc;
using BalanceApi.Business.Interfaces;
using BalanceApi.Models.Enums;
using BalanceApi.Models.Exceptions;

namespace BalanceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BalanceController : ControllerBase
{
    private readonly IBalanceBusiness business;
    private readonly BalanceApi.Repositories.ILogger logger;

    public BalanceController(IBalanceBusiness business, BalanceApi.Repositories.ILogger logger)
    {
        this.business = business;
        this.logger = logger;
    }

    [HttpGet]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
    [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Get(string uidAccount)
    { 
        logger.WriteLine(LoggerType.Log, $"Request: Get Balance");
        try
        {
            double result = business.Get(uidAccount);
            logger.WriteLine(LoggerType.Log, $"Response: {StatusCodes.Status201Created}");
            
            return StatusCode(StatusCodes.Status200OK, result);
        }
        catch (NoTransactionsException e) {
            logger.WriteLine(LoggerType.Warning, $"Response: {StatusCodes.Status404NotFound}: {e.Message}");
            return StatusCode(StatusCodes.Status404NotFound, e.Message);
        }
        catch (System.Exception e)
        {
            logger.WriteLine(LoggerType.Error, $"Response: {StatusCodes.Status500InternalServerError}: {e}");
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}
