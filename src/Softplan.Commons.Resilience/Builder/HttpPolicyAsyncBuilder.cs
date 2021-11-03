using Polly;
using Softplan.Commons.Resilience.Interfaces;
using Softplan.Commons.Resilience.Policies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace Softplan.Commons.Resilience.Builder
{
    public class HttpPolicyAsyncBuilder : IHttpPolicyAsyncBuilder
    {
        public ICollection<IAsyncPolicy> _policies = new List<IAsyncPolicy>();
        public const int RetryCount = 3;
        private const int ExceptionsAllowedBeforeBreaking = 3;
        public IHttpPolicyAsyncBuilder WithDefaultPolicies()
        {
            _policies.Add(PoliciesHttpClient.TimeOutPolicyAsync(RettryAttempt().Invoke(RetryCount)));
            _policies.Add(PoliciesHttpClient.RetryPolicyAsync(RetryCount));
            _policies.Add(PoliciesHttpClient.WaitRetryPolicyAsync(RetryCount, RettryAttempt()));
            _policies.Add(PoliciesHttpClient.CircuitBreakerPolicyAsync(ExceptionsAllowedBeforeBreaking, RettryAttempt().Invoke(RetryCount)));

            return this;
        }

        public IHttpPolicyAsyncBuilder WithRetry(int retryCount)
        {
            _policies.Add(PoliciesHttpClient.RetryPolicyAsync(retryCount));
            return this;
        }

        public IHttpPolicyAsyncBuilder WithWaitRetry(int retryCount, Func<int, TimeSpan> rettryAttempt)
        {
            _policies.Add(PoliciesHttpClient.WaitRetryPolicyAsync(retryCount, rettryAttempt));
            return this;
        }

        public IHttpPolicyAsyncBuilder WithFallback(Func<CancellationToken, Task> fallbackAction)
        {
            _policies.Add(PoliciesHttpClient.FallBackPolicyAsync(fallbackAction));
            return this;
        }

        public IHttpPolicyAsyncBuilder WithCircuitBreaker(TimeSpan rettryAttempt, int exceptionsAllowedBeforeBreaking = 3)
        {
            _policies.Add(PoliciesHttpClient.CircuitBreakerPolicyAsync(exceptionsAllowedBeforeBreaking, rettryAttempt));
            return this;
        }

        public IHttpPolicyAsyncBuilder WithCircuitBreaker(int exceptionsAllowedBeforeBreaking, TimeSpan rettryAttempt, Action<Exception, TimeSpan> onBreak, Action onReset, Action onHalfOpen)
        {
            _policies.Add(PoliciesHttpClient.CircuitBreakerPolicyAsync(exceptionsAllowedBeforeBreaking, rettryAttempt, onBreak, onReset, onHalfOpen));
            return this;
        }

        public IHttpPolicyAsyncBuilder WithTimeout(TimeSpan timeout)
        {
            _policies.Add(PoliciesHttpClient.TimeOutPolicyAsync(timeout));
            return this;
        }

        public IPolicyExecutor Build()
        {
            if (_policies?.Any() == false)
                throw new InvalidOperationException("There are no policies to execute.");

            VerifyDuplicated();

            return new PolicyExecutor(_policies);
        }

        private static Func<int, TimeSpan> RettryAttempt() =>
           (time) =>
           {
               var retry = 1;
               var delay = TimeSpan.Zero;
               while (retry <= RetryCount)
               {
                   delay += TimeSpan.FromSeconds(Math.Pow(2, retry));
                   retry++;
               }

               return delay + TimeSpan.FromSeconds(16);
           };

        private void VerifyDuplicated()
        {
            var duplicate = _policies.GroupBy(g => g.PolicyKey)
                 .Where(w => w.Count() > 1)
                 .Select(s => s.Key);

            if (duplicate?.Any() == true)
                throw new InvalidOperationException("There are duplicated policies.");
        }
    }
}
