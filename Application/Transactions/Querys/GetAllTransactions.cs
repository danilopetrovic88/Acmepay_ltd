using Acmepay_ltd.Domain.Models;
using MediatR;

namespace Application.Transactions.Querys
{
    public class GetAllTransactions : IRequest<ICollection<Transaction>>
    {
    }
}

