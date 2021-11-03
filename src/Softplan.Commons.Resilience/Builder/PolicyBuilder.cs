using Softplan.Commons.Resilience.Interfaces;

namespace Softplan.Commons.Resilience.Builder
{
    public class PolicyBuilder
    {
        public IHttpPolicyAsyncBuilder UseExecutorAsync() =>
            new HttpPolicyAsyncBuilder();
    }
}
