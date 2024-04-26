
using Acmepay_ltd.Domain.Models;

namespace Application.Abstractions
{
    public interface ITransactionRepository
    {
        Task<ICollection<Transaction>> GetAllTransactions();
        Task<Transaction> PostTransactionAuthorize(Transaction transaction);
        Task<Transaction> PostTransactionVoid(string orderReference, Guid Id);
        Task<Transaction> PostTransactionCapture(string orderReference, Guid Id);
    }
}
