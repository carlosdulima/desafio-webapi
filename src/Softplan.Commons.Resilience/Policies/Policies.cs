using Microsoft.Extensions.Logging;
using Polly;
using Polly.Timeout;
using Softplan.Commons.Resilience.Logger;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Softplan.Commons.Resilience.Policies
{
    public static class Policies<T> where T : Exception
    {
        private static readonly ILogger _logger = ApplicationLogging.CreateLogger<T>();
        internal const string Message = " Polly {0} : {1} of {2} due to: {3}";

        public static IAsyncPolicy RetryPolicyAsync(int retryCount) =>
           Policy
              .Handle<T>()
              .RetryAsync(retryCount, (exception, onRetry, context) =>
              {
                  _logger.LogTrace(string.Format(Message, PolicyKeys.RetryPolicy, onRetry, context.PolicyKey, exception.Message));
              })
           .WithPolicyKey(string.Format(PolicyKeys.RetryPolicy, typeof(T).Name));

        public static IAsyncPolicy WaitRetryPolicyAsync(int retryCount, Func<int, TimeSpan> rettryAttempt) =>
         Policy
           .Handle<T>()
           .WaitAndRetryAsync(retryCount, rettryAttempt,
            (exception, onRetry, context) =>
            {
                _logger.LogTrace(string.Format(Message, PolicyKeys.RetryPolicy, onRetry, context.PolicyKey, exception.Message));
            })
          .WithPolicyKey(string.Format(PolicyKeys.WaitRetryPolicy, typeof(T).Name));

        public static IAsyncPolicy CircuitBreakerPolicyAsync(int exceptionsAllowedBeforeBreaking, TimeSpan rettryAttempt) =>
         Policy
             .Handle<T>()
             .CircuitBreakerAsync(exceptionsAllowedBeforeBreaking,
             rettryAttempt,
             OnBreak(),
             OnReset(),
             OnHalfOpen())
          .WithPolicyKey(string.Format(PolicyKeys.CircuitBreakerPolicy, typeof(T).Name));

        public static IAsyncPolicy CircuitBreakerPolicyAsync(int exceptionsAllowedBeforeBreaking, TimeSpan rettryAttempt, Action<Exception, TimeSpan> onBreak, Action onReset, Action onHalfOpen) =>
         Policy
           .Handle<T>()
           .CircuitBreakerAsync(exceptionsAllowedBeforeBreaking,
            rettryAttempt,
            onBreak,
            onReset,
            onHalfOpen)
           .WithPolicyKey(string.Format(PolicyKeys.CircuitBreakerPolicy, typeof(T).Name));

        public static IAsyncPolicy FallBackPolicyAsync(Func<CancellationToken, Task> fallbackAction) =>
         Policy
            .Handle<T>()
            .FallbackAsync(fallbackAction,
                ex =>
                {
                    _logger.LogTrace($"Polly FallbackAsync Fallback method used due to: {ex}");
                    return Task.CompletedTask;
                })
               .WithPolicyKey(string.Format(PolicyKeys.FallBackPolicy, typeof(T).Name));


        public static IAsyncPolicy TimeOutPolicyAsync(TimeSpan timeout) =>
         Policy
             .TimeoutAsync(
                 timeout,
                 TimeoutStrategy.Pessimistic)
            .WithPolicyKey(string.Format(PolicyKeys.TimeOutPolicy, typeof(T).Name));


        private static Action<Exception, TimeSpan> OnBreak() =>
          (ex, time) =>
          {
              _logger.LogTrace("Polly CircuitBreakerAsync Circuit breaker opened", ex);
          };


        private static Action OnReset() =>
            () =>
            {
                _logger.LogTrace("Polly CircuitBreakerAsync Circuit breaker reset");
            };

        private static Action OnHalfOpen() =>
            () =>
            {
                _logger.LogTrace("Polly CircuitBreakerAsync Half-open: Next call is a trial");
            };
    }
}
