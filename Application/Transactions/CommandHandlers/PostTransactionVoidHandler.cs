using Acmepay_ltd.Domain.Models;
using Application.Abstractions;
using Application.Transactions.Commands;
using MediatR;

namespace Application.Transactions.CommandHandlers
{
    public class PostTransactionVoidHandler : IRequestHandler<PostTransactionVoid, Transaction>
    {
        private readonly ITransactionRepository _transactionRepository;
        public PostTransactionVoidHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<Transaction> Handle(PostTransactionVoid request, CancellationToken cancellationToken)
        {
            var tran = await _transactionRepository.PostTransactionVoid(request.OrderReference, request.Id);
            return tran;
        }
    }
}

