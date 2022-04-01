
using TransferApi.Mapper;
using TransferApi.Models;
using static TransferApi.Mapper.SearchDto;

namespace TransferApi.Services
{

    public interface IMoneyTransferService
    {

        ///<exception cref="AccountNotFoundException">is thrown if the account is not accessible</exception>
        Task<TransferDto> CreateTransfer(Transfer transfer);
        Task<TransferDto> GetTransferDetails(Guid ownerId);
        Task<IEnumerable<TransferDto>> GetTransfers(TransferSearchDto transferSearchDto);
        Task<TransferDto> SignTransfer(Guid transferUid);
        // Task<TransactionSearchDto> CreateTranaction(TransferTransaction transferTransaction);
        Task<IEnumerable<TransferTransactionDto>> GetTransactions(TransactionSearchDto transactionSearchDto);
    }

}