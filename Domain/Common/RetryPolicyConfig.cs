namespace Domain.Common
{
    public class RetryPolicyConfig
    {
        public int MaxRetries { get; set; }
        public int RetryDelay { get; set; }
    }
}
