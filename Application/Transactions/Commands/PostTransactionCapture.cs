using Acmepay_ltd.Domain.Models;
using MediatR;

namespace Application.Transactions.Commands
{
    public class PostTransactionCapture : IRequest<Transaction>
    {
        public Guid Id { get; set; }
        public string OrderReference { get; set; }
    }
}

