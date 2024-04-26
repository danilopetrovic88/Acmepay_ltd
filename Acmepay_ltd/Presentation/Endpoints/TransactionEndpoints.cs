using Dapper;
using Microsoft.Data.SqlClient;
using Acmepay_ltd.Services;
using Acmepay_ltd.helper;
using System.Text.Json;
using Azure.Messaging;
using Acmepay_ltd.Domain.Models;
using Acmepay_ltd.Application.Transactions.Create;
using MediatR;

namespace Acmepay_ltd.Presentation.Endpoints;

public static class TransactionEndpoints
{
    public static void MapTransactionEndpoints(this IEndpointRouteBuilder builder)
    {
        get all transactions
        builder.MapGet("api/authorize", async (SqlConnectionFactory sqlConnectionFactory) =>
        {
            using var connection = sqlConnectionFactory.Create();

            const string sql = "SELECT Amount, Currency, CardHolderNumber, HolderName, Id, Status FROM Transactions";

            var transactions = await connection.QueryAsync<Transaction>(sql);

            List<object> response = new List<object>();

            foreach (var transaction in transactions)
            {
                var transactionDetails = new
                {
                    transaction.Amount,
                    transaction.Currency,
                    CardHolderNumber = ReplaceCharsWithAsterisks.Replace(transaction.CardHolderNumber),
                    transaction.HolderName,
                    transaction.Id,
                    transaction.Status,
                };

                response.Add(transactionDetails);
            }

            return Results.Ok(response);
        });

        //authorize
        builder.MapPost("api/authorize", async (Transaction transaction, SqlConnectionFactory sqlConnectionFactory) =>
        {
            using var connection = sqlConnectionFactory.Create();

            const string sql = """
                    INSERT INTO Transactions (Id, Amount, Currency, CardHolderNumber, HolderName, ExpirationMonth, ExpirationYear, CVV, OrderReference, Status)
                    VALUES (@Id, @Amount, @Currency, @CardHolderNumber, @HolderName, @ExpirationMonth, @ExpirationYear, @CVV, @OrderReference, @Status)
                """;

            await connection.ExecuteAsync(sql, transaction);

            var response = new
            {
                transaction.Id,
                transaction.Status
            };

            return Results.Ok(response);
        });

        //Voids
        builder.MapPost("api/authorize/{id}/voids", async (SqlConnectionFactory sqlConnectionFactory, string id, TransactionRequest request) =>
        {
            using var connection = sqlConnectionFactory.Create();

            var selectedTransaction = await connection.QuerySingleOrDefaultAsync<Transaction>("""
                    SELECT Id, Status, OrderReference
                    FROM Transactions
                    WHERE Id = @Id
                """, new { Id = id });

            if (selectedTransaction == null)
            {
                return Results.NotFound("Transaction not found");
            }

            selectedTransaction.Status = TransactionStatus.Voided.ToString();

            selectedTransaction.OrderReference = request.OrderReference;

            await connection.ExecuteAsync(@"
                        UPDATE Transactions
                        SET Status = @Status, OrderReference = @OrderReference
                        WHERE Id = @Id
                    ", selectedTransaction);

            var response = new
            {
                selectedTransaction.Id,
                selectedTransaction.Status
            };

            return Results.Ok(response);
        });

        //Capture
        builder.MapPost("api/authorize/{id}/capture", async (SqlConnectionFactory sqlConnectionFactory, string id, TransactionRequest request) =>
        {
            using var connection = sqlConnectionFactory.Create();

            var selectedTransaction = await connection.QuerySingleOrDefaultAsync<Transaction>(@"
                    SELECT Id, Status, OrderReference
                    FROM Transactions
                    WHERE Id = @Id
                ", new { Id = id });

            if (selectedTransaction == null)
            {
                return Results.NotFound("Transaction not found");
            }

            selectedTransaction.Status = TransactionStatus.Captured.ToString();

            selectedTransaction.OrderReference = request.OrderReference;

            await connection.ExecuteAsync(@"
                        UPDATE Transactions
                        SET Status = @Status, OrderReference = @OrderReference
                        WHERE Id = @Id
                    ", selectedTransaction);

            var response = new
            {
                selectedTransaction.Id,
                selectedTransaction.Status
            };

            return Results.Ok(response);
        });
    }
}
