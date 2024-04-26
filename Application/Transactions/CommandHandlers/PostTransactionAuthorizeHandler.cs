using Acmepay_ltd.Domain.Models;
using Application.Abstractions;
using Application.Transactions.Commands;
using MediatR;

namespace Application.Transactions.CommandHandlers
{
    public class PostTransactionAuthorizeHandler : IRequestHandler<PostTransactionAuthorize, Transaction>
    {
        private readonly ITransactionRepository _transactionRepository;

        public PostTransactionAuthorizeHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<Transaction> Handle(PostTransactionAuthorize request, CancellationToken cancellationToken)
        {
            var newTransaction = new Transaction
            {
                Amount = request.Amount,
                Currency = request.Currency,
                CardHolderNumber = request.CardHolderNumber,
                HolderName = request.HolderName,
                ExpirationMonth = request.ExpirationMonth,
                ExpirationYear = request.ExpirationYear,
                CVV = request.CVV,
                OrderReference = request.OrderReference,
            };

            return await _transactionRepository.PostTransactionAuthorize(newTransaction);
        }
    }
}

