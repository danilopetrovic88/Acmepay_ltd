using Acmepay_ltd.Domain.Models;
using Application.Abstractions;
using Application.Transactions.Commands;
using MediatR;

namespace Application.Transactions.CommandHandlers
{
    public class PostTransactionCaptureHandler : IRequestHandler<PostTransactionCapture, Transaction>
    {
        private readonly ITransactionRepository _transactionRepository;
        public PostTransactionCaptureHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<Transaction> Handle(PostTransactionCapture request, CancellationToken cancellationToken)
        {
            var tran = await _transactionRepository.PostTransactionCapture(request.OrderReference, request.Id);
            return tran;
        }
    }
}

