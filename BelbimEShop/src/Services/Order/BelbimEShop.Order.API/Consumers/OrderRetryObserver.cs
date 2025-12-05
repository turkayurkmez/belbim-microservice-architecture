using MassTransit;

namespace BelbimEShop.Order.API.Consumers
{
    //public class OrderRetryObserver : IRetryObserver
    //{
    //   // private readonly ILogger<OrderRetryObserver> _logger;

    //    public OrderRetryObserver()
    //    {
    //        _logger = logger;
    //    }
    //    Task IRetryObserver.PostCreate<T>(RetryPolicyContext<T> context)
    //    {
    //        _logger.LogInformation("PostCreate: {Context}", context);
    //        return Task.CompletedTask;
    //    }

    //    Task IRetryObserver.PostFault<T>(RetryContext<T> context)
    //    {
    //        _logger.LogError("PostFault: {Context}", context);
    //        return Task.CompletedTask;
    //    }

    //    Task IRetryObserver.PreRetry<T>(RetryContext<T> context)
    //    {
    //        _logger.LogWarning("PreRetry: {Context}", context);
    //        return Task.CompletedTask;
    //    }

    //    Task IRetryObserver.RetryComplete<T>(RetryContext<T> context)
    //    {
    //        _logger.LogInformation("RetryComplete: {Context}", context);
    //        return Task.CompletedTask;
    //    }

    //    Task IRetryObserver.RetryFault<T>(RetryContext<T> context)
    //    {
    //        _logger.LogError("RetryFault: {Context}", context);
    //        return Task.CompletedTask;
    //    }
    //}
}
