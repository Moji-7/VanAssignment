

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TransferApi.Infrastructure.ExceptionHandling;
using TransferApi.Mapper;
using TransferApi.Models;
using TransferApi.Services;

namespace TransferApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransferController : ControllerBase
{
    private readonly ILogger<TransferController> _logger;
    private readonly IMapper _mapper;
    private readonly IMoneyTransferService _moneyTransferService;
    public string userName
    {
        get
        {
            Request.Headers.TryGetValue("user", out var user);
            return user;
        }
    }

    public TransferController(ILogger<TransferController> logger, IMapper mapper, IMoneyTransferService moneyTransferService)
    {
        _logger = logger;
        _mapper = mapper;
        _moneyTransferService = moneyTransferService;
    }

    [SwaggerOperation(Summary = "********************* submit new transfer request **********************")]
    [HttpPost]
    [Route("TransferRequest")]
    public async Task<IActionResult> TransferRequest([FromBody] TransferDto transferDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        Transfer transfer = ValidateTransfer(transferDto);
        await SubmitTransfer(transfer);
        transferDto.ID = transfer.ID;
        return CreatedAtAction(nameof(TransferRequestDetails), new { uid = transferDto.ID }, transferDto);
    }

    [NonAction]
    private async Task<TransferDto> SubmitTransfer(Transfer transfer)
    {

        TransferDto transferDto = await _moneyTransferService.CreateTransfer(transfer);
        // _logger.LogError($"transfer with id: {transfer.ID}, cant save in db");
        // throw new InvalidTransferDescriptionException(transfer.ID);
        _logger.LogInformation($"saved new transfer in db with id: {transfer.ID}");
        return transferDto;
    }


    [SwaggerOperation(Summary = "sign registered transfer requests to activate them (transform to TRANSACTION)")]
    [HttpPut]
    [Route("SignTransferRequest/{uid}")]
    public async Task<IActionResult> SignTransfer(Guid uid)//[FromQuery] Guid? uid,
    {
        try
        {
            var transferDto = await _moneyTransferService.GetTransferDetails(uid);
            if (transferDto is null)
            {
                _logger.LogError($"transfer with id: {uid}, hasn't been found in db.");
                return NotFound();
            }
            else
            {
                transferDto = await _moneyTransferService.SignTransfer(transferDto.ID);
                _logger.LogInformation($"transfer signed successfully with uid: {uid}");
                return Ok(transferDto);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside transferDto action: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }



        return NoContent();
    }


    [SwaggerOperation(Summary = "Get all transfer Requests base on card number)")]
    [HttpPost]
    [Route("GetTransferRequests")]
    public async Task<ActionResult<IEnumerable<TransferDto>>> GetTransfers([FromBody] SearchDto.TransferSearchDto transferSearchDto)
    {
        _logger.LogInformation($"Calling GetTransactions at {DateTime.Now}");
        var transfers = await _moneyTransferService.GetTransfers(transferSearchDto);
        return Ok(transfers);
    }


    [SwaggerOperation(Summary = "get all Transactions with optional search model")]
    [HttpPost]
    [Route("GetTransactions")]
    public async Task<ActionResult<IEnumerable<TransferTransaction>>> GetTransactions([FromBody] SearchDto.TransactionSearchDto transactionSearchDto)//[FromRoute] string cardNumber
    {
        _logger.LogInformation($"Calling GetTransactions at {DateTime.Now}");
        IEnumerable<TransferTransactionDto> transactions;
        transactions = await _moneyTransferService.GetTransactions(transactionSearchDto);
        return Ok(transactions);
    }

    [NonAction]
    private Transfer ValidateTransfer(TransferDto transferDto)
    {
        if (string.IsNullOrEmpty(transferDto.Description))
            throw new InvalidTransferDescriptionException("transfer must have description ");

        #region call integration services     
        //TODO: Account check with integration services
        #endregion

        transferDto.CreationDate = DateTime.Now;
        return _mapper.Map<Transfer>(transferDto);
    }


    [SwaggerOperation(Summary = "get  one Transfer Request with All its History Transactions")]
    [HttpGet()]
    [Route("TransferRequestDetails/{uid}")]
    public async Task<IActionResult> TransferRequestDetails(Guid uid)
    {
        var transferDto = await _moneyTransferService.GetTransferDetails(uid);
        if (transferDto is null)
        {
            _logger.LogError($"transfer with id: {uid}, hasn't been found in db.");
            return NotFound();
        }
        else
        {
            _logger.LogInformation($"Returned owner with id: {uid}");
            return Ok(transferDto);
        }
    }

}











