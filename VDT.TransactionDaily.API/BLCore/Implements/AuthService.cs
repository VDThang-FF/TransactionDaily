using VDT.TransactionDaily.API.BLCore.Interfaces;
using VDT.TransactionDaily.API.Helper;
using VDT.TransactionDaily.API.Models;
using VDT.TransactionDaily.API.Models.Enums;

namespace VDT.TransactionDaily.API.BLCore.Implements
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextService _httpContextService;

        public AuthService(IHttpContextService httpContextService)
        {
            _httpContextService = httpContextService;
        }

        /// <summary>
        /// Lấy thông tin người dùng
        /// </summary>
        /// <returns></returns>
        /// created by vdthang 17.01.2022
        public VdtUser GetUserInfo()
        {
            var userID = Converter.DecryptAES(_httpContextService.GetCookieByName(Enumarations.CookieKey.UserID));
            var userName = Converter.DecryptAES(_httpContextService.GetCookieByName(Enumarations.CookieKey.UserName));
            var email = Converter.DecryptAES(_httpContextService.GetCookieByName(Enumarations.CookieKey.Email));

            return new VdtUser()
            {
                Id = Convert.ToUInt32(userID),
                Email = email,
                UserName = userName,
            };
        }
    }
}
