using Auction.Infrastructure.Data;
using Carsties.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Auction.Infrastructure.Middleware;

public sealed class EventualConsistencyMiddleware
{
    private readonly RequestDelegate _next;

    public const string DomainEventsKey = "DomainEventsKey";

    public EventualConsistencyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, IPublisher publisher, AuctionDbContext dbContext)
    {
        var transaction = await dbContext.Database.BeginTransactionAsync();
        httpContext.Response.OnCompleted(async () =>
        {
            try
            {
                if (httpContext.Items.TryGetValue(DomainEventsKey, out var value) &&
                    value is Queue<IDomainEvent> domainEvents)
                {
                    while (domainEvents.TryDequeue(out var nextEvent))
                    {
                        await publisher.Publish(nextEvent);
                    }
                }

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        });

        await _next(httpContext);
    }
}