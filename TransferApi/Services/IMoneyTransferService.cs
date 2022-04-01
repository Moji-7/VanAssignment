
using TransferApi.Mapper;
using TransferApi.Models;
using static TransferApi.Mapper.SearchDto;

namespace TransferApi.Services
{

    public interface IMoneyTransferService
    {



        //List<MoneyTransfer> getPendingMoneyTransfers();

        //  List<MoneyTransfer> getDisputedTransfers() throws Exception;

        // void inquiry(String ssn, MoneyTransfer transaction) throws Exception;

        // void transfer(MoneyTransfer transaction, String ssn, MoneyTransferIntegrationService integrationService) throws Exception;

        ///<exception cref="AccountNotFoundException">is thrown if the profile does not exists</exception>
        Task<TransferDto> CreateTransfer(Transfer transfer);
        Task<TransferDto> GetTransferDetails(Guid ownerId);
        Task<IEnumerable<TransferDto>> GetTransfers(TransferSearchDto transferSearchDto);
        Task<TransferDto> SignTransfer(Guid  transferUid);
       // Task<TransactionSearchDto> CreateTranaction(TransferTransaction transferTransaction);
        Task<IEnumerable<TransferTransactionDto>> GetTransactions(TransactionSearchDto transactionSearchDto);
    }

}