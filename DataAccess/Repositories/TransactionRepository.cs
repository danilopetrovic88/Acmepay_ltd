using Acmepay_ltd.Domain.Models;
using Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TransactionDbContext _dbContext;
        public TransactionRepository(TransactionDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ICollection<Transaction>> GetAllTransactions()
        {
            return await _dbContext.Transactions.ToListAsync();
        }

        public async Task<Transaction> PostTransactionAuthorize(Transaction transaction)
        {
            _dbContext.Transactions.Add(transaction);
            await _dbContext.SaveChangesAsync();
            return transaction;
        }

        public async Task<Transaction> PostTransactionCapture(string orderReference, Guid Id)
        {
            var tran = await _dbContext.Transactions.FirstOrDefaultAsync(x => x.Id == Id);
            tran.OrderReference = orderReference;
            tran.Status = TransactionStatus.Captured.ToString();
            await _dbContext.SaveChangesAsync();
            return tran;
        }

        public async Task<Transaction> PostTransactionVoid(string orderReference, Guid Id)
        {
            var tran = await _dbContext.Transactions.FirstOrDefaultAsync(x => x.Id == Id);
            tran.OrderReference = orderReference;
            tran.Status = TransactionStatus.Voided.ToString();
            await _dbContext.SaveChangesAsync();
            return tran;
        }
    }
}
