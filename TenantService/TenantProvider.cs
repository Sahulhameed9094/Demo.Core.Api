using Microsoft.Extensions.Options;

namespace Demo.Core.Api.TenantService
{
    public sealed class TenantProvider
    {
        private const string TenantIdHeaderName = "X-TenantId";

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TenantConnectionString _tenantConnectionString;

        public TenantProvider(IHttpContextAccessor httpContextAccessor,
          IOptions<TenantConnectionString> tenantConnectionString)
        {
            _httpContextAccessor = httpContextAccessor;
            _tenantConnectionString = tenantConnectionString.Value;
        }

        public int GetTenantId()
        {
            // getting tenant id from HTTP header
            var tenandidHeader = _httpContextAccessor.HttpContext?.Request.Headers[TenantIdHeaderName];
            if (!string.IsNullOrEmpty(tenandidHeader))
            {
                return Convert.ToInt32(tenandidHeader);
            }
            // SET 0 IF USING MULTITENANTE
            return 1;
        }


        public string GetTenantConnection()
        {
            return _tenantConnectionString.tenantList[Convert.ToString(GetTenantId())];            
        }


    }
}
