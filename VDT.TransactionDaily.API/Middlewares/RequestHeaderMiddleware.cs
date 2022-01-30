using System.Net.Http.Headers;
using System.Text;
using VDT.TransactionDaily.API.Authorize;
using VDT.TransactionDaily.API.BL.Interfaces;
using VDT.TransactionDaily.API.Extensions;
using VDT.TransactionDaily.API.Helper;
using VDT.TransactionDaily.API.Models.Responses;
using static VDT.TransactionDaily.API.Models.Enums.Enumarations;

namespace VDT.TransactionDaily.API.Middlewares
{
    public class RequestHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Ghi đè interface invoke request
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        /// created by vdthang 21.01.2022
        public async Task Invoke(HttpContext context)
        {
            var defaultPath = new List<string>() { "swagger, favicon" };
            var typeAttr = context.GetEndpoint().Metadata.OfType<VDTAuthorizeAttribute>();

            if (!defaultPath.Any(p => context.Request.Path.Value.Contains(p)) && typeAttr != null && typeAttr.Count() > 0)
            {
                try
                {
                    var isContinue = await SetHeader(context);
                    if (!isContinue)
                    {
                        context.Response.ContentType = new MediaTypeHeaderValue("application/json").ToString();
                        await context.Response.WriteAsync(Converter.Serialize(new ServiceResponse()
                        .OnError(Code.Unauthorize, SubCode.ErrorAuthorize)), Encoding.UTF8);
                        return;
                    }
                }
                catch
                {
                    context.Response.ContentType = new MediaTypeHeaderValue("application/json").ToString();
                    await context.Response.WriteAsync(Converter.Serialize(new ServiceResponse()
                    .OnError(Code.Unauthorize, SubCode.ErrorAuthorize)), Encoding.UTF8);
                    return;
                }
            }

            await _next(context);
        }

        /// <summary>
        /// Thực hiện gán header request
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// created by vdthang 21.01.2022
        private async Task<bool> SetHeader(HttpContext context)
        {
            if (context.Request == null)
                return false;

            // Thực hiện đọc session id từ cookie
            if (!context.Request.Headers.ContainsKey(CookieKey.VDT))
                return false;

            var parseHeader = Converter.Deserialize<LoginResponse>(Convert.ToString(context.Request.Headers[CookieKey.VDT]));
            var sessionID = Converter.DecryptAES(parseHeader.SessionID);

            if (string.IsNullOrWhiteSpace(Convert.ToString(sessionID)))
                return false;

            // Thực hiện lấy access token
            var sessionBL = context.RequestServices.GetService<ISessionBL>();
            var findBySessionID = sessionBL.GetByID(Guid.Parse(Convert.ToString(sessionID)));
            if (findBySessionID == null)
                return false;

            var accessToken = findBySessionID.AccessToken;
            context.Request.Headers.Authorization = "Bearer " + accessToken;
            context.Request.Headers[CookieKey.SessionID] = parseHeader.SessionID;
            context.Request.Headers[CookieKey.UserID] = parseHeader.UserID;
            context.Request.Headers[CookieKey.UserName] = parseHeader.UserName;
            context.Request.Headers[CookieKey.Email] = parseHeader.Email;
            context.Request.Headers[CookieKey.DeviceID] = parseHeader.DeviceID;

            return true;
        }
    }
}
