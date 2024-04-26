using Application.Abstractions;
using DataAccess;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Application.Transactions.Commands;
using Application.Transactions.Querys;
using Acmepay_ltd.Domain.Models;
using Acmepay_ltd.helper;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TransactionDbContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Acmepay_ltd")));
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddMediatR(typeof(PostTransactionAuthorize));

var app = builder.Build();

app.UseHttpsRedirection();

//GET get all transactions
app.MapGet("/api/authorize", async (IMediator mediator) =>
{
    var getTransactions = new GetAllTransactions();
    var transactions = await mediator.Send(getTransactions);

    List<object> response = new List<object>();

    foreach (var transaction in transactions)
    {
        var transactionDetails = new
        {
            Amount = transaction.Amount,
            Currency = transaction.Currency,
            CardHolderNumber = ReplaceCharsWithAsterisks.Replace(transaction.CardHolderNumber),
            HolderName = transaction.HolderName,
            Id = transaction.Id,
            Status = transaction.Status,
        };

        response.Add(transactionDetails);
    }

    return Results.Ok(response);
});

//POST api/authorize
app.MapPost("/api/authorize", async (IMediator mediator, Transaction transaction) =>
{
    var createTransaction = new PostTransactionAuthorize
    {
        Amount = transaction.Amount,
        Currency = transaction.Currency,
        CardHolderNumber = transaction.CardHolderNumber,
        HolderName = transaction.HolderName,
        ExpirationMonth = transaction.ExpirationMonth,
        ExpirationYear = transaction.ExpirationYear,
        CVV = transaction.CVV,
        OrderReference = transaction.OrderReference,
    };

    var tran = await mediator.Send(createTransaction);

    var response = new
    {
        Id = tran.Id,
        Status = transaction.Status
    };

    return Results.Ok(response);
});
// PUT api/authorize/{id}/capture
app.MapPut("/api/authorize/{id}/capture", async (IMediator mediator, PostTransactionCapture request, Guid id) =>
{
    var capture = new PostTransactionCapture
    {
        Id = id,
        OrderReference = request.OrderReference
    };
    var tran = await mediator.Send(capture);

    var response = new
    {
        Id = tran.Id,
        Status = tran.Status
    };

    return Results.Ok(response);
});

// PUT api/authorize/{id}/voids
app.MapPut("/api/authorize/{id}/voids", async (IMediator mediator, PostTransactionCapture request, Guid id) =>
{
    var voided = new PostTransactionVoid
    {
        Id = id,
        OrderReference = request.OrderReference
    };
    var tran = await mediator.Send(voided);

    var response = new
    {
        Id = tran.Id,
        Status = tran.Status
    };

    return Results.Ok(response);
});

app.Run();
