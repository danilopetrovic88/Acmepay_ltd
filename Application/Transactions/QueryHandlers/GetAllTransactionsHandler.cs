using Acmepay_ltd.Domain.Models;
using Application.Abstractions;
using Application.Transactions.Querys;
using MediatR;

namespace Application.Transactions.QueryHandlers
{
    public class GetAllTransactionsHandler : IRequestHandler<GetAllTransactions, ICollection<Transaction>>
    {
        private readonly ITransactionRepository _transactionRepository;
        public GetAllTransactionsHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<ICollection<Transaction>> Handle(GetAllTransactions request, CancellationToken cancellationToken)
        {
            return await _transactionRepository.GetAllTransactions();
        }
    }
}

