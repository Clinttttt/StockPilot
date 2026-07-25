namespace StockPilot.Api.RateLimiting
{
    public static class RateLimitPolicies
    {
        public const string General = nameof(General);
        public const string Login = nameof(Login);
        public const string Sensitive = nameof(Sensitive);
        public const string Upload = nameof(Upload);
    }
}
