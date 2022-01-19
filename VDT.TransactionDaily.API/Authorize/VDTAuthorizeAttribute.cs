using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
            var identityUser = context.HttpContext.User.Identity;
            if (!identityUser.IsAuthenticated)
            {
                context.Result = new ObjectResult(new ServiceResponse()
                    .OnError(Code.Unauthorize, SubCode.ErrorAuthorize));
                return;
            }
        }
    }
}
