using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TransferApi.Mapper;
using TransferApi.Models;

namespace TransferApi.Services
{
    public class MoneyTransferService : IMoneyTransferService
    {

        private readonly TransferContext _context;
        private readonly IMapper _mapper;
        public MoneyTransferService(TransferContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        public async Task<TransferDto> CreateTransfer(Transfer transfer)
        {
            try
            {
                await _context.AddAsync(transfer);
                await _context.SaveChangesAsync();
                return _mapper.Map<TransferDto>(transfer);

            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<TransferTransactionDto>> GetTransactions()
        {
            return await _context.TransferTransactions
             .ProjectTo<TransferTransactionDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
            //account = await _context.Accounts.Where(x => x.userId == userId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TransferTransactionDto>> GetTransactions(SearchDto.TransactionSearchDto transactionSearchDto)
        {
            var query =
           (from transactions in _context.TransferTransactions
            
            where transactionSearchDto.CardNumber == null || transactions.Transfer.Cart.CartInfo.CardNumber == transactionSearchDto.CardNumber
     
            select transactions
            )
            .ProjectTo<TransferTransactionDto>(_mapper.ConfigurationProvider).ToListAsync();
            return await query;
        }

        public async Task<TransferDto> GetTransferDetails(Guid Id)
        {
            return await _context.Transfers
            .Where(x => x.ID == Id)
              .ProjectTo<TransferDto>(_mapper.ConfigurationProvider)
             .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TransferDto>> GetTransfers(SearchDto.TransferSearchDto transferSearchDto)
        {
            var query =
           (from transfers in _context.Transfers
            where transfers.Cart.CartInfo.CardNumber == transferSearchDto.CardNumber
             && transfers.IsSigned != true
            select transfers).OrderByDescending(x => x.CreationDate)
            .ProjectTo<TransferDto>(_mapper.ConfigurationProvider).ToListAsync();

            var result = await query;
            result.Count();
            return result;
        }

        public async Task<TransferDto> SignTransfer(Guid transferUid)
        {
            Transfer transfer = new Transfer();
            // using (var dbContextTransaction = _context.Database.BeginTransaction())
            //  {
            transfer = await _context.Transfers.FirstAsync(s => s.ID.Equals(transferUid));
            transfer.IsSigned = true;
            transfer.SignedDate = DateTime.Now;
            CreateTranaction(_context, transfer);

            await _context.SaveChangesAsync();
            // await dbContextTransaction.CommitAsync();
            //  }

            return _mapper.Map<TransferDto>(transfer);


            //TODO : create new transaction 

        }

        private async Task<TransferTransaction> CreateTranaction(TransferContext context, Transfer transfer)
        {
            try
            {
                TransferTransaction transferTransaction = new TransferTransaction
                {
                    SendDate = DateTime.Now,
                    Status = TransferApi.Models.Status.Pending,
                    TransferID = transfer.ID,
                    Transfer = transfer
                };



                await _context.TransferTransactions.AddAsync(transferTransaction);
                return transferTransaction;
                //  await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
        }
    }
}

