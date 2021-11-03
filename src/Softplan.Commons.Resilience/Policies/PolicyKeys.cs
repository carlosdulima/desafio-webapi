namespace Softplan.Commons.Resilience.Policies
{
    internal static class PolicyKeys
    {
        public const string RetryPolicy = "RetryPolicyAsync.{0}";
        public const string WaitRetryPolicy = "WaitRetryPolicyAsync.{0}";
        public const string CircuitBreakerPolicy = "CircuitBreakerPolicyAsync.{0}";
        public const string FallBackPolicy = "FallBackPolicyAsync.{0}";
        public const string TimeOutPolicy = "TimeOutPolicyAsync.{0}";
    }
}
