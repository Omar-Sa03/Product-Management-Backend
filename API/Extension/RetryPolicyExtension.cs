using Domain.Common;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace API.Extension
{
    public static class RetryPolicyExtension
    {
        public static void RetryExtension(this IServiceCollection services, IConfiguration configuration)
        {
            var retryPolicyConfig = configuration.Get<RetryPolicyConfig>();
            retryPolicyConfig.MaxRetries = configuration.GetValue<int>("RetryPolicy:maxRetries");
            retryPolicyConfig.RetryDelay = configuration.GetValue<int>("RetryPolicy:retryDelay");
            services.TryAddSingleton(retryPolicyConfig);
        }
    }
}
