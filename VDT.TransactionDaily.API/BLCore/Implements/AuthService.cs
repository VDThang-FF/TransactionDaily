using System.Security.Claims;
using VDT.TransactionDaily.API.BLCore.Interfaces;
using VDT.TransactionDaily.API.Extensions;
using VDT.TransactionDaily.API.Helper;
using VDT.TransactionDaily.API.JWT;
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
            if (StartupParameter.IsDevelopment())
            {
                // Đọc cấu hình từ token
                var config = JwtHelpers.ReadToken(StartupParameter.AccessToken);
                var payload = config.Payload;
                return new VdtUser()
                {
                    Id = Convert.ToUInt32(payload[ClaimTypes.NameIdentifier]),
                    Email = Convert.ToString(payload[ClaimTypes.Email]),
                    UserName = Convert.ToString(payload[ClaimTypes.Name]),
                };
            }
            else
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

        /// <summary>
        /// Lấy ID người dùng
        /// </summary>
        /// <returns></returns>
        /// created by vdthang 24.01.2022
        public uint? GetUserId()
        {
            if (StartupParameter.IsDevelopment())
            {
                // Đọc cấu hình từ token
                if (string.IsNullOrWhiteSpace(StartupParameter.AccessToken))
                    return null;

                var config = JwtHelpers.ReadToken(StartupParameter.AccessToken);
                var payload = config.Payload;
                return Convert.ToUInt32(payload[ClaimTypes.NameIdentifier]);
            }
            else
            {
                var userID = Converter.DecryptAES(_httpContextService.GetCookieByName(Enumarations.CookieKey.UserID));
                return Convert.ToUInt32(userID);
            }
        }

        /// <summary>
        /// Lấy tên đăng nhập người dùng
        /// </summary>
        /// <returns></returns>
        /// created by vdthang 24.01.2022
        public string GetUserName()
        {
            if (StartupParameter.IsDevelopment())
            {
                // Đọc cấu hình từ token
                if (string.IsNullOrWhiteSpace(StartupParameter.AccessToken))
                    return null;

                var config = JwtHelpers.ReadToken(StartupParameter.AccessToken);
                var payload = config.Payload;
                return Convert.ToString(payload[ClaimTypes.Name]);
            }
            else
            {
                return Converter.DecryptAES(_httpContextService.GetCookieByName(Enumarations.CookieKey.UserName));
            }
        }

        /// <summary>
        /// Lấy email người dùng
        /// </summary>
        /// <returns></returns>
        /// created by vdthang 24.01.2022
        public string GetUserEmail()
        {
            if (StartupParameter.IsDevelopment())
            {
                // Đọc cấu hình từ token
                if (string.IsNullOrWhiteSpace(StartupParameter.AccessToken))
                    return null;

                var config = JwtHelpers.ReadToken(StartupParameter.AccessToken);
                var payload = config.Payload;
                return Convert.ToString(payload[ClaimTypes.Email]);
            }
            else
            {
                return Converter.DecryptAES(_httpContextService.GetCookieByName(Enumarations.CookieKey.Email));
            }
        }
    }
}
