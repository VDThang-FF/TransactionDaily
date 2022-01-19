using VDT.TransactionDaily.API.BLCore.Interfaces;

namespace VDT.TransactionDaily.API.Models.Others
{
    public class CoreService
    {
        public VdtContext VdtContext { get; set; }
        public IConfigService ConfigService { get; set; }
        public IHttpContextService HttpContextService { get; set; }
        public IAuthService AuthService { get; set; }

        public CoreService(VdtContext vdtContext, IConfigService configService, IHttpContextService httpContextService, IAuthService authService)
        {
            VdtContext = vdtContext;
            ConfigService = configService;
            HttpContextService = httpContextService;
            AuthService = authService;
        }
    }
}
