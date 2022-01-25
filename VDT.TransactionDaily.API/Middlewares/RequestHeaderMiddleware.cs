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

            // Nếu là develop thì config session id mặc định
            object sessionID = null;
            if (StartupParameter.IsDevelopment())
            {
                sessionID = new Guid("00000000-0000-0000-0000-000000000001");
            }
            else
            {
                // Thực hiện đọc session id từ cookie
                if (!context.Request.Cookies.ContainsKey(CookieKey.SessionID))
                    return false;

                sessionID = Converter.DecryptAES(context.Request.Cookies[CookieKey.SessionID].ToString());
            }

            if (string.IsNullOrWhiteSpace(Convert.ToString(sessionID)))
                return false;

            // Thực hiện lấy access token
            var sessionBL = context.RequestServices.GetService<ISessionBL>();
            var findBySessionID = sessionBL.GetByID(Guid.Parse(Convert.ToString(sessionID)));
            if (findBySessionID == null)
                return false;

            var accessToken = findBySessionID.AccessToken;
            context.Request.Headers.Authorization = "Bearer " + accessToken;

            return true;
        }
    }
}
