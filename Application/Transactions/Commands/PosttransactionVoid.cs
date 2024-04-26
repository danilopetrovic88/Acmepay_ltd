using Acmepay_ltd.Domain.Models;
using MediatR;

namespace Application.Transactions.Commands
{
    public class PostTransactionVoid : IRequest<Transaction>
    {
        public Guid Id { get; set; }
        public string OrderReference { get; set; }
    }
}

