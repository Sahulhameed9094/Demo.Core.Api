namespace Demo.Core.Api.TenantService
{
    public sealed class TenantProvider
    {
        private const string TenantIdHeaderName = "X-TenantId";

        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetTenantId()
        {
            var tenandidHeader = _httpContextAccessor.HttpContext.Request.Headers[TenantIdHeaderName];

            return Convert.ToInt32(tenandidHeader);
        }
    }
}
