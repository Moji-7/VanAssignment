using Moq;
using Xunit;
using TransferApi;
using TransferApi.Controllers;
using TransferApi.Models;
using TransferApi.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using AutoMapper;
using TransferApi.Mapper;
using Status = TransferApi.Mapper.Status;

namespace TransferApi.Tests;

public class TransferTest
{
    #region Property  
    public Mock<IMoneyTransferService> mockService = new Mock<IMoneyTransferService>();

    private Mock<IMapper> mockMapper = new Mock<IMapper>();
    public Mock<ILogger<TransferController>> mockLog = new Mock<ILogger<TransferController>>();

    #endregion
    public enum Status
    {
        Pending, Success, Fail
    }

    [Fact]
    public async void GetEmployeebyId()
    {

        // new card
        var card = new Cart
        {
            Balance = 3300.00m,
            CardNumber = "4001 5900 0000 0001",
            CartInfo = new CartInfo
            {
                CardNumber = "4001 5900 0000 0001",
                CVV2 = 441,
                ExpireDate = "12/22"
            }
        };
        // new Transfers
        var transfer = new Transfer
        {
            ID = new System.Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            CreationDate = System.DateTime.ParseExact("2022-03-31 20:40:50,000", "yyyy-MM-dd HH:mm:ss,fff", null),
            CounterpartyName = "john smith",
            CounterpartyIBAN = "23-4542678-166",

            Amount = 3000.00m,
            Description = "loan transfer",
            IsSigned = false,
            Cart = card
        };

        List<TransferTransaction> transactions = new List<TransferTransaction>{
            new TransferTransaction
            {
                ResultCode = 110,
                ResultDescription = "failed",
                TraceNumber = 87687666,
                SendDate = DateTime.ParseExact("2022-03-31 20:40:52,531", "yyyy-MM-dd HH:mm:ss,fff", null),
                Status = (Models.Status?)Status.Pending,
                ResponseDate = DateTime.ParseExact("2009-03-31 20:40:53,531", "yyyy-MM-dd HH:mm:ss,fff", null),
                Transfer = transfer
             },
             new TransferTransaction
             {
                        ResultCode = 220,
                        ResultDescription = "done successfullt",
                        TraceNumber = 1245397869,
                        SendDate = DateTime.ParseExact("2022-03-31 20:40:52,531", "yyyy-MM-dd HH:mm:ss,fff", null),
                        Status = (Models.Status?)Status.Success,
                        ResponseDate = DateTime.ParseExact("2009-03-31 20:40:54,531", "yyyy-MM-dd HH:mm:ss,fff", null),
                        Transfer = transfer
                        }
};



        IEnumerable<TransferTransactionDto> transactionsDto =
        mockMapper.Object.Map<IEnumerable<TransferTransactionDto>>(transactions);

        SearchDto.TransactionSearchDto transactionSearchDto = new SearchDto.TransactionSearchDto();
        mockService.Setup(p => p.GetTransactions(transactionSearchDto)).ReturnsAsync(transactionsDto);
        TransferController controller = new TransferController(mockLog.Object, mockMapper.Object, mockService.Object);
        var result = await controller.GetTransactions(transactionSearchDto);
        
        //Assert
        Assert.Equal(transactions, result);

    }


}