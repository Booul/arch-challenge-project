using Microsoft.AspNetCore.Mvc;
using TransactionApi.Models;
using TransactionApi.Data.VO;
using TransactionApi.Business.Interfaces;
using TransactionApi.Models.Enums;
using TransactionApi.Models.Expections;

namespace TransactionApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoreController : ControllerBase
{
    private readonly IBalanceBusiness<Transaction, TransactionVO> business;
    private readonly TransactionApi.Repositories.ILogger logger;

    public CoreController(IBalanceBusiness<Transaction, TransactionVO> business, TransactionApi.Repositories.ILogger logger)
    {
        this.business = business;
        this.logger = logger;
    }

    [HttpPost]
    [Route("send-transaction")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
    [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult SendTransaction(TransactionVO transactionVO)
    { 
        logger.WriteLine(LoggerType.Log, $"Request: Send Transaction ({transactionVO.ToString()})");
        try
        {
            Transaction result = business.Save(transactionVO);
            logger.WriteLine(LoggerType.Log, $"Response: {StatusCodes.Status201Created}");
            
            return StatusCode(StatusCodes.Status201Created, result);
        }
        catch (InvalidUidFormatExpection e)
        {
            logger.WriteLine(LoggerType.Error, $"Response: {StatusCodes.Status422UnprocessableEntity}: {e.Message}");
            return StatusCode(StatusCodes.Status422UnprocessableEntity, e.Message);
        }
        catch (System.Exception e)
        {
            logger.WriteLine(LoggerType.Error, $"Response: {StatusCodes.Status500InternalServerError}: {e}");
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPost]
    [Route("rebuild-balance")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult RebuildBalance()
    {
        logger.WriteLine(LoggerType.Log, $"Request: Rebuild Balance");
        try
        {
            business.RebuildBalance();
            logger.WriteLine(LoggerType.Log, $"Response: {StatusCodes.Status204NoContent}");

            return StatusCode(StatusCodes.Status204NoContent);
        }
        catch (System.Exception e)
        {
            logger.WriteLine(LoggerType.Error, $"Response: {StatusCodes.Status500InternalServerError}: {e}");
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}
