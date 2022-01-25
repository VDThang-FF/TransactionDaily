using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VDT.TransactionDaily.API.Extensions;
using VDT.TransactionDaily.API.JWT;
using VDT.TransactionDaily.API.Models.Responses;
using static VDT.TransactionDaily.API.Models.Enums.Enumarations;

namespace VDT.TransactionDaily.API.Authorize
{
    public class VDTAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public bool Ignore { get; set; } = false;

        public VDTAuthorizeAttribute(bool igNore = false)
        {
            Ignore = igNore;
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }

        /// <summary>
        /// Thực hiện ghi đè hàm override
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Validate user authen identity
            var identityUser = context.HttpContext.User.Identity;
            if (!identityUser.IsAuthenticated)
            {
                context.Result = new ObjectResult(new ServiceResponse()
                    .OnError(Code.Unauthorize, SubCode.ErrorAuthorize));
                return;
            }

            // Validate token
            var jwtSetting = context.HttpContext.RequestServices.GetService<JwtSettings>();
            var token = context.HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", string.Empty);
            var isValid = JwtHelpers.GetPrincipalFromExpiredToken(token, jwtSetting);
            if (!isValid)
            {
                context.Result = new ObjectResult(new ServiceResponse()
                    .OnError(Code.Unauthorize, SubCode.ErrorAuthorize));
                return;
            }

            StartupParameter.AccessToken = token;
        }
    }
}
